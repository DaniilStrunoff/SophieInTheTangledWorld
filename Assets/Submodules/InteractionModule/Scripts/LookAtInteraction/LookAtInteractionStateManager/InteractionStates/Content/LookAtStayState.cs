using UnityEngine;


[CreateAssetMenu(fileName = "LookAtStayState", menuName = "Look At States/Look At Stay State")]
public class LookAtStayState : LookAtBaseState {
    override public void UpdateState(ILookAtManager manager) {}

    override public void OnStateEnter(ILookAtManager manager) {
        manager.ControllerFactory.LookAtController.TriggerLookAtEnterEvent += ((LookAtManager)manager).SwitchToLookAtChangeState;
        manager.ControllerFactory.LookAtController.TriggerLookAtExitEvent += ((LookAtManager)manager).SwitchToLookAtExitState;
    }

    override public void OnStateExit(ILookAtManager manager) {
        manager.ControllerFactory.LookAtController.TriggerLookAtEnterEvent -= ((LookAtManager)manager).SwitchToLookAtChangeState;
        manager.ControllerFactory.LookAtController.TriggerLookAtExitEvent -= ((LookAtManager)manager).SwitchToLookAtExitState;
    }

    override public void Act(ILookAtManager manager) {}
}
