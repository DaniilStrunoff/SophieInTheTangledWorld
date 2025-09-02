using UnityEngine;

[CreateAssetMenu(fileName = "InteractionStayState", menuName = "Interaction States/Interaction Stay State")]
public class InteractionStayState : InteractionBaseState {
    override public void UpdateState(IInteractionManager manager) {}

    override public void OnStateEnter(IInteractionManager manager) {
        manager.ControllerFactory.UIElementController.EnableInteraction();
        manager.ControllerFactory.UIElementController.InteractionEvent += ((InteractionManager)manager).SwitchToInteractionExitState;
        manager.ControllerFactory.UIElementController.InteractionEvent += manager.ControllerFactory.InteractableMarkController.MakeInteraction;
    }

    override public void OnStateExit(IInteractionManager manager) {
        manager.ControllerFactory.UIElementController.DisableInteraction();
        manager.ControllerFactory.UIElementController.InteractionEvent -= ((InteractionManager)manager).SwitchToInteractionExitState;
        manager.ControllerFactory.UIElementController.InteractionEvent -= manager.ControllerFactory.InteractableMarkController.MakeInteractable;
    }

    override public void Act(IInteractionManager manager) {}
}
