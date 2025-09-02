using UnityEngine;

[CreateAssetMenu(fileName = "InteractionEnterState", menuName = "Interaction States/Interaction Enter State")]
public class InteractionEnterState : InteractionBaseState {
    override public void UpdateState(IInteractionManager manager) {
        if (manager.ControllerFactory.UIElementController.IsFadeComplited) {
            var nextState = ((InteractionManagerStateFactory)manager.StateFactory).InteractionStayState;
            manager.SwitchState(nextState);
        }
    }

    override public void OnStateEnter(IInteractionManager manager) {
        manager.ControllerFactory.UIElementController.EnableInteraction();
        manager.ControllerFactory.InteractableMarkController.MakeInteractable();
        manager.ControllerFactory.UIElementController.InteractionEvent += ((InteractionManager)manager).SwitchToInteractionExitState;
        manager.ControllerFactory.UIElementController.InteractionEvent += manager.ControllerFactory.InteractableMarkController.MakeInteraction;
    }

    override public void OnStateExit(IInteractionManager manager) {
        manager.ControllerFactory.UIElementController.DisableInteraction();
        manager.ControllerFactory.UIElementController.InteractionEvent -= ((InteractionManager)manager).SwitchToInteractionExitState;
        manager.ControllerFactory.UIElementController.InteractionEvent -= manager.ControllerFactory.InteractableMarkController.MakeInteraction;
    }

    override public void Act(IInteractionManager manager) {
        manager.ControllerFactory.UIElementController.FadeIn();
    }
}
