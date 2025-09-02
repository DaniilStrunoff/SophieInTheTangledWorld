using UnityEngine;

[CreateAssetMenu(fileName = "InteractionExitState", menuName = "Interaction States/Interaction Exit State")]
public class InteractionExitState : InteractionBaseState {
    override public void UpdateState(IInteractionManager manager) {
        if (manager.ControllerFactory.UIElementController.IsUnfadeComplited) {
            var nextState = ((InteractionManagerStateFactory)manager.StateFactory).NoInteractionState;
            manager.SwitchState(nextState);
        }
    }

    override public void OnStateEnter(IInteractionManager manager) {}

    override public void OnStateExit(IInteractionManager manager) {}

    override public void Act(IInteractionManager manager) {
        manager.ControllerFactory.UIElementController.FadeOut();
    }
}
