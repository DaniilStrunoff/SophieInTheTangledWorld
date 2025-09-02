using UnityEngine;


[CreateAssetMenu(fileName = "DialogPlayEnterState", menuName = "Game Process States/Dialog SubStates/Dialog Stay SubStates/Dialog Play SubStates/Dialog Play Enter State")]
public class DialogPlayEnterState : GameProcessBaseState {

    override public void UpdateState(IGameProcessManager manager) {
        if (manager.ControllerFactory.DialogContinueButtonFadeController.IsUnfadeComplited) {
            manager.SwitchState(((DialogPlayStateFactory)manager.StateFactory).DialogPlayStayState);
        }
    }

    override public void OnStateEnter(IGameProcessManager manager) {
        // Debug.Log("Play State");
    }

    override public void OnStateExit(IGameProcessManager manager) {}

    override public void Act(IGameProcessManager manager) {
        manager.ControllerFactory.DialogContinueButtonFadeController.FadeOut();
    }
}
