using UnityEngine;


[CreateAssetMenu(fileName = "NoLookAtState", menuName = "Look At States/No Look At State")]
public class NoLookAtState : LookAtBaseState {
    override public void UpdateState(ILookAtManager manager) {}

    override public void OnStateEnter(ILookAtManager manager) {
        manager.ControllerFactory.LookAtController.TriggerLookAtEnterEvent += ((LookAtManager)manager).SwitchToLookAtEnterState;
    }

    override public void OnStateExit(ILookAtManager manager) {
        manager.ControllerFactory.LookAtController.TriggerLookAtEnterEvent -= ((LookAtManager)manager).SwitchToLookAtEnterState;
    }

    override public void Act(ILookAtManager manager) {}
}
