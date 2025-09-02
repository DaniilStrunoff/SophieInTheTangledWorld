using UnityEngine;


[CreateAssetMenu(fileName = "PauseMenuEnterState", menuName = "Game Process States/Pause Menu SubStates/Pause Menu Enter State")]
public class PauseMenuEnterState : GameProcessBaseState {
    override public void UpdateState(IGameProcessManager manager) {
        if (manager.ControllerFactory.PauseMenuFadeController.IsFadeComplited &&
            manager.ControllerFactory.MainMenuFadeController.IsUnfadeComplited &&
            manager.ControllerFactory.MainMenuConfirmationFadeController.IsUnfadeComplited &&
            manager.ControllerFactory.MainMenuDefaultFadeController.IsUnfadeComplited &&
            manager.ControllerFactory.GameplayFadeController.IsUnfadeComplited &&
            manager.ControllerFactory.DialogFadeController.IsUnfadeComplited) {
            manager.SwitchState(((PauseMenuStateFactory)manager.StateFactory).PauseMenuStayState);
        }
    }

    override public void OnStateEnter(IGameProcessManager manager) {
        manager.ControllerFactory.TimeScaleController.SetTimeScaleToZero();
    }

    override public void OnStateExit(IGameProcessManager manager) {}

    override public void Act(IGameProcessManager manager) {
        manager.ControllerFactory.PauseMenuFadeController.FadeIn();
        manager.ControllerFactory.MainMenuFadeController.FadeOut();
        manager.ControllerFactory.MainMenuConfirmationFadeController.FadeOut();
        manager.ControllerFactory.MainMenuDefaultFadeController.FadeOut();
        manager.ControllerFactory.GameplayFadeController.FadeOut();
        manager.ControllerFactory.DialogFadeController.FadeOut();
    }
}
