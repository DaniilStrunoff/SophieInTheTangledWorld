using System;
using Unity.Collections;
using UnityEngine;
using UnityEngine.InputSystem;


public class MainMenuController : MonoBehaviour, IController {
    public event Action InputActionBackEvent;
    public event Action BackEvent;
    public event Action ConfirmationMenuEvent;

    public GameProcessBaseState LastState {get; private set;}

    public GameObject DefaultMenuSelectedObject;
    public GameObject ConfirmationMenuSelectedObject;

    [HideInInspector]
    public GameObject FirstDefaultMenuSelectedObject;

    public void Awake() {
        FirstDefaultMenuSelectedObject = DefaultMenuSelectedObject;
    }

    public void InvokeInputActionBackEvent(InputAction.CallbackContext context) {
        if (context.performed) InputActionBackEvent?.Invoke();
    }

    public void InvokeBackEvent() {
        BackEvent?.Invoke();
    }

    public void InvokeConfirmationMenuEvent() {
        ConfirmationMenuEvent?.Invoke();
    }

    public void SetDefaultMenuSelectedObject(GameObject gameObject) {
        DefaultMenuSelectedObject = gameObject;
    }
}
