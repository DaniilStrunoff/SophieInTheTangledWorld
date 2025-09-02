using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Collider))]
public class UIElementController : MonoBehaviour, IInteractionController {
    public event Action TriggerEnterEvent;
    public event Action TriggerExitEvent;

    public event Action InteractionEvent;
    public event Action TryInteractionEvent;

    public float FadeTime = 1;

    [HideInInspector]
    public bool IsFadeComplited = false;

    [HideInInspector]
    public bool IsUnfadeComplited = false;

    public CanvasGroup canvasGroup;

    private float time = 0f;

    void Start() {
        if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.gameObject.SetActive(false);
        IsFadeComplited = false;
        IsUnfadeComplited = true;
        canvasGroup.alpha = 0;
    }

    public void HideUIElement() {
        canvasGroup.alpha = 0;
        canvasGroup.gameObject.SetActive(false);
    }

    public void FadeOut() {
        if (IsUnfadeComplited) return;
        if (time <= FadeTime) {
            canvasGroup.alpha = 1 - (time/FadeTime);
            time += Time.unscaledDeltaTime > 0.1 ? 0 : Time.unscaledDeltaTime;
            IsFadeComplited = false;
            IsUnfadeComplited = false;
        } else {
            canvasGroup.alpha = 0;
            canvasGroup.gameObject.SetActive(false);
            time = 0f;
            IsUnfadeComplited = true;   
        }
    }

    public void FadeIn() {
        if (IsFadeComplited) return;
        canvasGroup.gameObject.SetActive(true);
        if (time <= FadeTime) {
            canvasGroup.alpha = time/FadeTime;
            time += Time.unscaledDeltaTime;
            IsFadeComplited = false;
            IsUnfadeComplited = false;
        } else {
            canvasGroup.alpha = 1;
            time = 0f;
            IsFadeComplited = true;
        }
    }

    public void InvokeTriggerEnterEvent() {
        TriggerEnterEvent?.Invoke();
    }

    public void EnableInteraction() {
        TryInteractionEvent += InvokeInteractionEvent;
    }

    public void DisableInteraction() {
        TryInteractionEvent -= InvokeInteractionEvent;
    }

    public void TryInvokeInteractionEvent() {
        TryInteractionEvent?.Invoke();
    }

    private void InvokeInteractionEvent() {
        InteractionEvent?.Invoke();
    }

    public void OnTriggerEnter(Collider collider) {
        Interactor interactor = collider.GetComponentInParent<Interactor>();
        if (interactor != null) {
            TriggerEnterEvent.Invoke();
        }
    }

    public void OnTriggerExit(Collider collider) {
        Interactor interactor = collider.GetComponentInParent<Interactor>();
        if (interactor != null) {
            TriggerExitEvent.Invoke();
        }
    }
}
