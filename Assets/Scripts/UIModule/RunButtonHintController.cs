using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;


[RequireComponent(typeof(Animator))]
public class RunButtonHintController : MonoBehaviour {
    public PlayerInputController PlayerInputController;

    private Animator animator;

    public enum InputDeviceEnum {
        KeyboardAndMouse,
        Gamepad
    }

    [SerializeField]
    private InputDeviceEnum inputDevice;

    private HashSet<InputDevice> InputDevices {
        get {
            return mapping[inputDevice];
        }
    }

    private Dictionary<InputDeviceEnum, HashSet<InputDevice>> mapping = new() {
        { InputDeviceEnum.KeyboardAndMouse, new() {Keyboard.current, Mouse.current} },
        { InputDeviceEnum.Gamepad, new() { Gamepad.current } },
    };

    void OnEnable() {
        if (SaveAndLoadController.GetInstance().SaveData.RunButtonHint[InputDevices.First()]) return;
        animator = GetComponent<Animator>();
        PlayerInputController.DeviceChangeEvent += OnDeviceChange;
        PlayerInputController.PlayerInputActions.FindAction("Run").performed += OnRunEvent;
        OnDeviceChange(PlayerInputController.LastUsedDevice);
    }

    private void OnDeviceChange(InputDevice device) {
        if (SaveAndLoadController.GetInstance().SaveData.RunButtonHint[InputDevices.First()]) {
            animator.ResetTrigger("Show");
            animator.SetTrigger("Hide");
            return;
        }
        if (InputDevices.Contains(device)) {
            animator.ResetTrigger("Hide");
            animator.SetTrigger("Show");
        } else {
            animator.ResetTrigger("Show");
            animator.SetTrigger("Hide");
        }
    }

    private void OnRunEvent(CallbackContext callback) {
        if (!InputDevices.Contains(PlayerInputController.LastUsedDevice)) return;
        PlayerInputController.DeviceChangeEvent -= OnDeviceChange;
        PlayerInputController.PlayerInputActions.FindAction("Run").performed -= OnRunEvent;
        SaveAndLoadController.GetInstance().SaveData.RunButtonHint[InputDevices.First()] = true;
        SaveAndLoadController.GetInstance().Save();
        animator.ResetTrigger("Show");
        animator.SetTrigger("Hide");
    }

    void OnDisable() {
        PlayerInputController.DeviceChangeEvent -= OnDeviceChange;
        PlayerInputController.PlayerInputActions.FindAction("Run").performed -= OnRunEvent;
    }
}
