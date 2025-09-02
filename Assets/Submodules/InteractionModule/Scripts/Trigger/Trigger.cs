using UnityEngine;
using UnityEngine.Events;


[RequireComponent(typeof(Collider))]
public class Trigger : MonoBehaviour {
    public TriggerType triggerType;
    public UnityEvent OnTriggerEvents;

    public enum TriggerType {
        OneShot,
        MultiShot
    }

    private bool isNotTriggered = true;

    public void OnTriggerEnter(Collider collider) {
        Triggerer triggerer = collider.GetComponentInParent<Triggerer>();
        if (triggerer != null) {
            TriggerInvoke();
        }
    }

    private void TriggerInvoke() {
        switch (triggerType) {
            case TriggerType.OneShot:
                if (isNotTriggered) {
                    OnTriggerEvents.Invoke();
                    isNotTriggered = false;
                }
            break;
            case TriggerType.MultiShot:
                OnTriggerEvents.Invoke();
            break;
        }
    }
}
