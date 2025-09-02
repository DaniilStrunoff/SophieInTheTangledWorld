using UnityEngine;


[CreateAssetMenu(fileName = "MainMenuDefaultEnterState", menuName = "Game Process States/Main Menu SubStates/Main Menu Stay SubStates/Main Menu Default SubStates/Main Menu Default Enter State")]
public class MainMenuDefaultEnterState : GameProcessBaseState {

    override public void UpdateState(IGameProcessManager manager) {
        if (manager.ControllerFactory.MainMenuDefaultFadeController.IsFadeComplited &&
            manager.ControllerFactory.MainMenuConfirmationFadeController.IsUnfadeComplited) {
            manager.SwitchState(((MainMenuDefaultStateFactory)manager.StateFactory).MainMenuDefaultStayState);
        }
    }

    override public void OnStateEnter(IGameProcessManager manager) {}

    override public void OnStateExit(IGameProcessManager manager) {}

    override public void Act(IGameProcessManager manager) {
        manager.ControllerFactory.MainMenuDefaultFadeController.FadeIn();
        manager.ControllerFactory.MainMenuConfirmationFadeController.FadeOut();
    }
}
