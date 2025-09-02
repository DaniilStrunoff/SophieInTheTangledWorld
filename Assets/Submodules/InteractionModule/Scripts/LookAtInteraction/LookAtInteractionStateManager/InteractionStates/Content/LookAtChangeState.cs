using UnityEngine;


[CreateAssetMenu(fileName = "LookAtChangeState", menuName = "Look At States/Look At Change State")]
public class LookAtChangeState : LookAtBaseState {
    override public void UpdateState(ILookAtManager manager) {
        if (manager.ControllerFactory.LookAtController.IsFadeComplited &&
            manager.ControllerFactory.LookAtController.IsLookComplited) {
            var nextState = ((LookAtManagerStateFactory)manager.StateFactory).LookAtStayState;
            manager.SwitchState(nextState);
        }
    }

    override public void OnStateEnter(ILookAtManager manager) {
        manager.ControllerFactory.LookAtController.TriggerLookAtEnterEvent += ((LookAtManager)manager).SwitchToLookAtChangeState;
        manager.ControllerFactory.LookAtController.TriggerLookAtExitEvent += ((LookAtManager)manager).SwitchToLookAtExitState;
    }

    override public void OnStateExit(ILookAtManager manager) {
        manager.ControllerFactory.LookAtController.TriggerLookAtEnterEvent -= ((LookAtManager)manager).SwitchToLookAtChangeState;
        manager.ControllerFactory.LookAtController.TriggerLookAtExitEvent -= ((LookAtManager)manager).SwitchToLookAtExitState;
    }

    override public void Act(ILookAtManager manager) {
        manager.ControllerFactory.LookAtController.FadeIn();
        manager.ControllerFactory.LookAtController.LookAt();
    }
}
