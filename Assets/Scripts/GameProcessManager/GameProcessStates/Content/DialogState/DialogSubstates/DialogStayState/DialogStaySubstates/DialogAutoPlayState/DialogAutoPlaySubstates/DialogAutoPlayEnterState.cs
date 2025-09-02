using UnityEngine;


[CreateAssetMenu(fileName = "DialogAutoPlayEnterState", menuName = "Game Process States/Dialog SubStates/Dialog Stay SubStates/Dialog Auto Play SubStates/Dialog Auto Play Enter State")]
public class DialogAutoPlayEnterState : GameProcessBaseState {

    override public void UpdateState(IGameProcessManager manager) {
        if (manager.ControllerFactory.DialogContinueButtonFadeController.IsUnfadeComplited) {
            manager.SwitchState(((DialogAutoPlayStateFactory)manager.StateFactory).DialogAutoPlayStayState);
        }
    }

    override public void OnStateEnter(IGameProcessManager manager) {}

    override public void OnStateExit(IGameProcessManager manager) {}

    override public void Act(IGameProcessManager manager) {
        manager.ControllerFactory.DialogContinueButtonFadeController.FadeOut();
    }
}
