using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.Rendering;
using System.Collections;
using Unity.VisualScripting;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.UI;


[RequireComponent(typeof(EventSystem), typeof(PlayerInput))]
public class PlayerInputController : MonoBehaviour, IController {

    public InputActionAsset controller;

    public GameObject CurrentSelectedObject {
        get {
            return eventSystem.currentSelectedGameObject;
        }
        set {
            eventSystem.SetSelectedGameObject(value);
        }
    }

    public InputActionMap UiInputActions {get; private set;}
    public InputActionMap PlayerInputActions {get; private set;}

    private InputAction DialogContinueAction {get; set;}
    private InputAction DialogAutoAction {get; set;}

    public Action<InputDevice> DeviceChangeEvent;

    public InputDevice LastUsedDevice { get; private set; }
    public PlayerInput PlayerInput;
    private EventSystem eventSystem;

    public GraphicRaycaster GraphicRaycaster;

    private static PlayerInputController instance;

    void Awake() {
        instance = this;
        UiInputActions = controller.FindActionMap("UI");
        PlayerInputActions = controller.FindActionMap("Player");
        DialogContinueAction = UiInputActions.FindAction("DialogContinue");
        DialogAutoAction = UiInputActions.FindAction("DialogAuto");
        eventSystem = GetComponent<EventSystem>();
        InputSystem.onEvent += OnInputEvent;
        LastUsedDevice = InputSystem.GetDevice(SaveAndLoadController.GetInstance().SaveData.InputDevice);
    }

    private readonly Dictionary<InputActionMap, bool> _requests = new();

    private void LateUpdate() {
        foreach (var (actionMap, enable) in _requests) {
            foreach (var action in actionMap) {
                if (enable) action.Enable();
                else action.Disable();
            }
        }
        _requests.Clear();
    }

    public void SetEnabled(InputActionMap action) {
        _requests[action] = true;
    }

    public void SetDisabled(InputActionMap action) {
        _requests[action] = false;
    }

    public void ManageCursorRaycasting() {
        if (Cursor.visible) GraphicRaycaster.enabled = true;
        else GraphicRaycaster.enabled = false;
    }

    private static void OnInputEvent(InputEventPtr eventPtr, InputDevice device) {
        if (eventPtr.IsA<StateEvent>() || eventPtr.IsA<DeltaStateEvent>()) {
            if (!ReferenceEquals(instance.LastUsedDevice, null) && instance.LastUsedDevice is Touchscreen) return;
            if (device is Gamepad gamepad) {
                if (eventPtr.IsA<StateEvent>() || eventPtr.IsA<DeltaStateEvent>()) {
                    var left = gamepad.leftStick.ReadValue();
                    var right = gamepad.rightStick.ReadValue();
                    if (left.sqrMagnitude < 0.01f && right.sqrMagnitude < 0.01f
                        && !AnyButtonPressed(gamepad)) {
                        return;
                    }
                }
            }
            if (device != instance.LastUsedDevice) {
                SaveAndLoadController.GetInstance().SaveData.InputDevice = device.layout;
                SaveAndLoadController.GetInstance().Save();
                instance.DeviceChangeEvent?.Invoke(device);
            }
            instance.LastUsedDevice = device;
        }
    }

    private static bool AnyButtonPressed(Gamepad gamepad) {
        return gamepad.allControls.Any(c => c is ButtonControl b && b.isPressed);
    }

    public void DisableAll() {
        if (UiInputActions == null) Awake();
        SetDisabled(UiInputActions);
        if (PlayerInputActions == null) Awake();
        SetDisabled(PlayerInputActions);
    }

    public void EnableAll() {
        if (UiInputActions == null) Awake();
        SetEnabled(UiInputActions);
        if (PlayerInputActions == null) Awake();
        SetEnabled(PlayerInputActions);
    }

    public void EnablePlayerSchemaOnly() {
        if (UiInputActions == null) Awake();
        SetDisabled(UiInputActions);
        if (PlayerInputActions == null) Awake();
        SetEnabled(PlayerInputActions);
    }

    public void EnableUISchemaOnly() {
        if (UiInputActions == null) Awake();
        SetEnabled(UiInputActions);
        if (PlayerInputActions == null) Awake();
        SetDisabled(PlayerInputActions);
    }

    public void SetSelectedGameObject(GameObject gameObject) {
        CurrentSelectedObject = gameObject;
    }

    public void SetSelectedGameObjectIfNone(GameObject gameObject) {
        if (CurrentSelectedObject == null) SetSelectedGameObject(gameObject);
    }

    public void SetSelectedGameObjectToNull() {
        CurrentSelectedObject = null;
    }

    private IEnumerator SetSelectedGameObjectAtEndOfFrame(GameObject gameObject, int numOfFramesToSkip) {
        for (int i = 0; i < numOfFramesToSkip; i++) 
            yield return null;
        SetSelectedGameObject(gameObject);
    }

    private static readonly List<RaycastResult> _hits = new();

    public void SetSelectedGameObjectOnDeviceChange(InputDevice inputDevice, GameObject gameObject) {
        switch (inputDevice) {
            case Mouse:
                var es = EventSystem.current;
                if (es == null) return;

                // Собираем все попадания из всех Raycaster'ов (GraphicRaycaster/PhysicsRaycaster)
                _hits.Clear();
                var data = new PointerEventData(es) { position = Mouse.current.position.ReadValue() };
                es.RaycastAll(data, _hits);
                if (_hits.Count == 0) return;

                for (int i = 0; i < _hits.Count; i++) {
                    var go = _hits[i].gameObject;
                    var selectable = go.GetComponentInParent<Selectable>();
                    if (selectable != null && selectable.IsActive() && selectable.IsInteractable()) {
                        es.SetSelectedGameObject(selectable.gameObject);
                    }
                }
                break;
            case Touchscreen:
                break;
            case Gamepad:
            case Keyboard:
                SetSelectedGameObjectIfNone(gameObject);
                break;
        }
    }

    private void OnDestroy() {
        InputSystem.onEvent -= OnInputEvent;
    }
}
