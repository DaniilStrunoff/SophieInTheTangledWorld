using UnityEngine;


[CreateAssetMenu(fileName = "MainMenuEnterState", menuName = "Game Process States/Main Menu SubStates/Main Menu Enter State")]
public class MainMenuEnterState : GameProcessBaseState {
    override public void UpdateState(IGameProcessManager manager) {
        if (manager.ControllerFactory.MainMenuFadeController.IsFadeComplited &&
            manager.ControllerFactory.PauseMenuFadeController.IsUnfadeComplited &&
            manager.ControllerFactory.PauseMenuDefaultFadeController.IsUnfadeComplited &&
            manager.ControllerFactory.PauseMenuLocalizationFadeController.IsUnfadeComplited &&
            manager.ControllerFactory.PauseMenuScreenFadeController.IsUnfadeComplited &&
            manager.ControllerFactory.PauseMenuDialogsFadeController.IsUnfadeComplited &&
            manager.ControllerFactory.PauseMenuAudioFadeController.IsUnfadeComplited) {
            manager.SwitchState(((MainMenuStateFactory)manager.StateFactory).MainMenuStayState);
        }
    }

    override public void OnStateEnter(IGameProcessManager manager) {
        manager.ControllerFactory.TimeScaleController.SetTimeScaleToZero();
    }

    override public void OnStateExit(IGameProcessManager manager) {}

    override public void Act(IGameProcessManager manager) {
        manager.ControllerFactory.MainMenuFadeController.FadeIn();
        manager.ControllerFactory.PauseMenuFadeController.FadeOut();
        manager.ControllerFactory.PauseMenuDefaultFadeController.FadeOut();
        manager.ControllerFactory.PauseMenuLocalizationFadeController.FadeOut();
        manager.ControllerFactory.PauseMenuScreenFadeController.FadeOut();
        manager.ControllerFactory.PauseMenuDialogsFadeController.FadeOut();
        manager.ControllerFactory.PauseMenuAudioFadeController.FadeOut();
    }
}
