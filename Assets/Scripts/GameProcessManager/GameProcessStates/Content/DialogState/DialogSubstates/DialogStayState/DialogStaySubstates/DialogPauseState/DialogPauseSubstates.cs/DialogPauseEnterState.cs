using UnityEngine;


[CreateAssetMenu(fileName = "DialogPauseEnterState", menuName = "Game Process States/Dialog SubStates/Dialog Stay SubStates/Dialog Pause SubStates/Dialog Pause Enter State")]
public class DialogPauseEnterState : GameProcessBaseState {

    override public void UpdateState(IGameProcessManager manager) {
        if (manager.ControllerFactory.DialogContinueButtonFadeController.IsFadeComplited) {
            manager.SwitchState(((DialogPauseStateFactory)manager.StateFactory).DialogPauseStayState);
        }
    }

    override public void OnStateEnter(IGameProcessManager manager) {
        // Debug.Log("Pause State");
    }

    override public void OnStateExit(IGameProcessManager manager) {}

    override public void Act(IGameProcessManager manager) {
        manager.ControllerFactory.DialogContinueButtonFadeController.FadeIn();
    }
}
