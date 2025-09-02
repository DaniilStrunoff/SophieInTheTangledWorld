using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.InputSystem.InputAction;


[AddComponentMenu("Utility/Event Invoker")]
public class EventInvoker : MonoBehaviour {
    public UnityEvent events;

    public void InvokeAll() {
        if (!gameObject.activeInHierarchy || !gameObject.activeSelf) return;
        events?.Invoke();
    }

    public void InvokeAll(CallbackContext callback) {
        if (callback.performed) InvokeAll();
    }
}
