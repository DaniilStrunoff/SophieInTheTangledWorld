using UnityEngine;


[CreateAssetMenu(fileName = "LookAtExitState", menuName = "Look At States/Look At Exit State")]
public class LookAtExitState : LookAtBaseState {
    override public void UpdateState(ILookAtManager manager) {
        if (manager.ControllerFactory.LookAtController.IsUnfadeComplited) {
            var nextState = ((LookAtManagerStateFactory)manager.StateFactory).NoLookAtState;
            manager.SwitchState(nextState);
        }
    }

    override public void OnStateEnter(ILookAtManager manager) {
        manager.ControllerFactory.LookAtController.TriggerLookAtEnterEvent += ((LookAtManager)manager).SwitchToLookAtChangeState;
    }

    override public void OnStateExit(ILookAtManager manager) {
        manager.ControllerFactory.LookAtController.TriggerLookAtEnterEvent -= ((LookAtManager)manager).SwitchToLookAtChangeState;
    }

    override public void Act(ILookAtManager manager) {
        manager.ControllerFactory.LookAtController.FadeOut();
    }
}
