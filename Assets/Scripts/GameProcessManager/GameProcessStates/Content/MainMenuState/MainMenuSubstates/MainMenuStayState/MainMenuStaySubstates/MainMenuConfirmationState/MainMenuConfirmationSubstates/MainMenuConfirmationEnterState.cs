using UnityEngine;


[CreateAssetMenu(fileName = "MainMenuConfirmationEnterState", menuName = "Game Process States/Main Menu SubStates/Main Menu Stay SubStates/Main Menu Confirmation SubStates/Main Menu Confirmation Enter State")]
public class MainMenuConfirmationEnterState : GameProcessBaseState {

    override public void UpdateState(IGameProcessManager manager) {
        if (manager.ControllerFactory.MainMenuConfirmationFadeController.IsFadeComplited &&
            manager.ControllerFactory.MainMenuDefaultFadeController.IsUnfadeComplited) {
            manager.SwitchState(((MainMenuConfirmationStateFactory)manager.StateFactory).MainMenuConfirmationStayState);
        }
    }

    override public void OnStateEnter(IGameProcessManager manager) {}

    override public void OnStateExit(IGameProcessManager manager) {}

    override public void Act(IGameProcessManager manager) {
        manager.ControllerFactory.MainMenuConfirmationFadeController.FadeIn();
        manager.ControllerFactory.MainMenuDefaultFadeController.FadeOut();
    }
}
