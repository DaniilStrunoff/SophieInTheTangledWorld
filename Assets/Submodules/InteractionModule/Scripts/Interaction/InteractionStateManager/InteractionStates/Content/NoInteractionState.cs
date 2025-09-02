using UnityEngine;

[CreateAssetMenu(fileName = "NoInteractionState", menuName = "Interaction States/No Interaction State")]
public class NoInteractionState : InteractionBaseState {
    override public void UpdateState(IInteractionManager manager) {}

    override public void OnStateEnter(IInteractionManager manager) {
        manager.ControllerFactory.UIElementController.HideUIElement();
        manager.ControllerFactory.InteractableMarkController.MakeNonInteractable();
    }

    override public void OnStateExit(IInteractionManager manager) {}

    override public void Act(IInteractionManager manager) {}
}
