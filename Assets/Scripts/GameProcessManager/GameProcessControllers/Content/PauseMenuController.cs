using System;
using UnityEngine;
using UnityEngine.InputSystem;


public class PauseMenuController : MonoBehaviour, IController {
    public event Action PauseEvent;
    public event Action InputActionPauseEvent;
    public event Action InputActionBackEvent;
    public event Action BackEvent;
    public event Action PauseLocalizationMenuEvent;
    public event Action PauseScreenMenuEvent;
    public event Action PauseDialogMenuEvent;
    public event Action PauseAudioMenuEvent;

    public GameProcessBaseState LastState {get; private set;}

    public GameObject DefaultMenuSelectedObject;
    public GameObject LocalizationMenuSelectedObject;
    public GameObject ScreenMenuSelectedObject;
    public GameObject DialogsMenuSelectedObject;
    public GameObject AudioMenuSelectedObject;

    [HideInInspector]
    public GameObject FirstDefaultMenuSelectedObject;

    public void Awake() {
        FirstDefaultMenuSelectedObject = DefaultMenuSelectedObject;
    }

    public void Pause(InputAction.CallbackContext context) {
        if (context.performed) InputActionPauseEvent?.Invoke();
    }

    public void Pause() {
        PauseEvent?.Invoke();
    }

    public void InvokeInputActionBackEvent(InputAction.CallbackContext context) {
        if (context.performed) InputActionBackEvent?.Invoke();
    }

    public void InvokeBackEvent() {
        BackEvent?.Invoke();
    }

    public void InvokePauseLocalizationMenuEvent(InputAction.CallbackContext context) {
        if (context.performed) PauseLocalizationMenuEvent?.Invoke();
    }

    public void InvokePauseLocalizationMenuEvent() {
        PauseLocalizationMenuEvent?.Invoke();
    }

    public void InvokePauseScreenMenuEvent(InputAction.CallbackContext context) {
        if (context.performed) PauseScreenMenuEvent?.Invoke();
    }

    public void InvokePauseScreenMenuEvent() {
        PauseScreenMenuEvent?.Invoke();
    }

    public void InvokePauseDialogMenuEvent(InputAction.CallbackContext context) {
        if (context.performed) PauseDialogMenuEvent?.Invoke();
    }

    public void InvokePauseDialogMenuEvent() {
        PauseDialogMenuEvent?.Invoke();
    }

    public void InvokePauseAudioMenuEvent(InputAction.CallbackContext context) {
        if (context.performed) PauseAudioMenuEvent?.Invoke();
    }

    public void InvokePauseAudioMenuEvent() {
        PauseAudioMenuEvent?.Invoke();
    }

    public void SetLastState(GameProcessBaseState state) {
        LastState = state;
    }

    public void SetDefaultMenuSelectedObject(GameObject gameObject) {
        DefaultMenuSelectedObject = gameObject;
    }
}
