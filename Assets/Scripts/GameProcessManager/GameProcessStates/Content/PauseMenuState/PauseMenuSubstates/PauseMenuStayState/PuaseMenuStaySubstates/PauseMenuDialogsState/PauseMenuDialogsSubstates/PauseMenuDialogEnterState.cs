using UnityEngine;


[CreateAssetMenu(fileName = "PauseMenuDialogsEnterState", menuName = "Game Process States/Pause Menu SubStates/Pause Menu Stay SubStates/Pause Menu Dialogs SubStates/Pause Menu Dialogs Enter State")]
public class PauseMenuDialogsEnterState : GameProcessBaseState {

    override public void UpdateState(IGameProcessManager manager) {
        if (manager.ControllerFactory.PauseMenuDialogsFadeController.IsFadeComplited &&
            manager.ControllerFactory.PauseMenuDefaultFadeController.IsUnfadeComplited &&
            manager.ControllerFactory.PauseMenuLocalizationFadeController.IsUnfadeComplited &&
            manager.ControllerFactory.PauseMenuScreenFadeController.IsUnfadeComplited &&
            manager.ControllerFactory.PauseMenuAudioFadeController.IsUnfadeComplited) {
            manager.SwitchState(((PauseMenuDialogsStateFactory)manager.StateFactory).PauseMenuDialogsStayState);
        }
    }

    override public void OnStateEnter(IGameProcessManager manager) {}

    override public void OnStateExit(IGameProcessManager manager) {}

    override public void Act(IGameProcessManager manager) {
        manager.ControllerFactory.PauseMenuDialogsFadeController.FadeIn();
        manager.ControllerFactory.PauseMenuDefaultFadeController.FadeOut();
        manager.ControllerFactory.PauseMenuLocalizationFadeController.FadeOut();
        manager.ControllerFactory.PauseMenuScreenFadeController.FadeOut();
        manager.ControllerFactory.PauseMenuAudioFadeController.FadeOut();
    }
}
