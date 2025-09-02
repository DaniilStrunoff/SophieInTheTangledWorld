using System;
using UnityEngine;


public class InteractableMarkController : MonoBehaviour, IInteractionController {
    public Animator animator;

    public void MakeNonInteractable() {
        animator.SetTrigger("MakeNonInteractable");
    }

    public void MakeInteractable() {
        animator.SetTrigger("MakeInteractable");
    }

    public void MakeInteraction() {
        animator.SetTrigger("MakeInteraction");
    }
}
