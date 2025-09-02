using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Collider), typeof(InteractionManager))]
public class Interactable : MonoBehaviour {
    public PlayerInputController PlayerInputController;

    public UnityEvent OnInteractionEvents;

    private UIElementController UIElementController;

    private MultiGraphicButton multiGraphicButton;

    void Start() {
        UIElementController = GetComponent<UIElementController>();
        UIElementController.InteractionEvent += OnInteractionEvents.Invoke;
        if (PlayerInputController == null) PlayerInputController = FindAnyObjectByType<PlayerInputController>();
        PlayerInputController.PlayerInputActions.FindAction("Interact").performed += TryInvokeInteractionEvent;
        multiGraphicButton = UIElementController.canvasGroup.GetComponentInChildren<MultiGraphicButton>();
        if (multiGraphicButton == null) Debug.LogError($"{name} has no button!");
        else multiGraphicButton.onClick.AddListener(TryInvokeInteractionEvent); 
    }

    private void TryInvokeInteractionEvent(InputAction.CallbackContext context) {
        if (context.performed)
            TryInvokeInteractionEvent();
    }

    private void TryInvokeInteractionEvent() {
        UIElementController.TryInvokeInteractionEvent();
    }

    private void OnDestroy() {
        UIElementController.InteractionEvent -= OnInteractionEvents.Invoke;
        PlayerInputController.PlayerInputActions.FindAction("Interact").performed -= TryInvokeInteractionEvent;
        multiGraphicButton.onClick.RemoveListener(TryInvokeInteractionEvent);
    }
}
