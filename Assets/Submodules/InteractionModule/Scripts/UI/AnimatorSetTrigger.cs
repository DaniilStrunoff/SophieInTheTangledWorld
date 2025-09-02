
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Animator))]
public class AnimatorSetTrigger : MonoBehaviour {

    public string TriggetName; 
    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    public void SetTrigger(InputAction.CallbackContext context) {
        if (context.performed) animator.SetTrigger(TriggetName);
    }
}
