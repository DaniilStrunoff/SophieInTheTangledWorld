using UnityEngine;

[CreateAssetMenu(fileName = "DialogEnterState", menuName = "Game Process States/Dialog SubStates/Dialog Enter State")]
public class DialogEnterState : GameProcessBaseState {
    override public void UpdateState(IGameProcessManager manager) {
        if (manager.ControllerFactory.GameplayFadeController.IsUnfadeComplited &&
            manager.ControllerFactory.PauseMenuDefaultFadeController.IsUnfadeComplited &&
            manager.ControllerFactory.PauseMenuLocalizationFadeController.IsUnfadeComplited &&
            manager.ControllerFactory.PauseMenuScreenFadeController.IsUnfadeComplited &&
            manager.ControllerFactory.PauseMenuDialogsFadeController.IsUnfadeComplited &&
            manager.ControllerFactory.PauseMenuFadeController.IsUnfadeComplited &&
            manager.ControllerFactory.PauseMenuAudioFadeController.IsUnfadeComplited &&
            manager.ControllerFactory.DialogFadeController.IsFadeComplited) {
            manager.SwitchState(((DialogStateFactory)manager.StateFactory).DialogStayState);
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
        manager.ControllerFactory.GameplayFadeController.FadeOut();
        manager.ControllerFactory.DialogFadeController.FadeIn();
    }
}
