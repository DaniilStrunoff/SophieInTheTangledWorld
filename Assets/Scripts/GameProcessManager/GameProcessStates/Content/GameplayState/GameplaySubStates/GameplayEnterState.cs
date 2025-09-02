using UnityEngine;

[CreateAssetMenu(fileName = "GameplayEnterState", menuName = "Game Process States/Gameplay SubStates/Gameplay Enter State")]
public class GameplayEnterState : GameProcessBaseState {
    override public void UpdateState(IGameProcessManager manager) {
        if (manager.ControllerFactory.GameplayFadeController.IsFadeComplited &&
            manager.ControllerFactory.PauseMenuDefaultFadeController.IsUnfadeComplited &&
            manager.ControllerFactory.PauseMenuLocalizationFadeController.IsUnfadeComplited &&
            manager.ControllerFactory.PauseMenuScreenFadeController.IsUnfadeComplited &&
            manager.ControllerFactory.PauseMenuDialogsFadeController.IsUnfadeComplited &&
            manager.ControllerFactory.PauseMenuFadeController.IsUnfadeComplited &&
            manager.ControllerFactory.PauseMenuAudioFadeController.IsUnfadeComplited &&
            manager.ControllerFactory.DialogFadeController.IsUnfadeComplited) {
            manager.SwitchState(((GameplayStateFactory)manager.StateFactory).GameplayStayState);
        }
    }

    override public void OnStateEnter(IGameProcessManager manager) {
        manager.ControllerFactory.TimeScaleController.SetTimeScaleToNormal();
    }

    override public void OnStateExit(IGameProcessManager manager) {}

    override public void Act(IGameProcessManager manager) {
        manager.ControllerFactory.PauseMenuFadeController.FadeOut();
        manager.ControllerFactory.PauseMenuDefaultFadeController.FadeOut();
        manager.ControllerFactory.PauseMenuLocalizationFadeController.FadeOut();
        manager.ControllerFactory.PauseMenuScreenFadeController.FadeOut();
        manager.ControllerFactory.PauseMenuDialogsFadeController.FadeOut();
        manager.ControllerFactory.PauseMenuAudioFadeController.FadeOut();
        manager.ControllerFactory.GameplayFadeController.FadeIn();
        manager.ControllerFactory.DialogFadeController.FadeOut();
    }
}
