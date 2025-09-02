using UnityEngine;


[CreateAssetMenu(fileName = "PauseMenuLocalizationEnterState", menuName = "Game Process States/Pause Menu SubStates/Pause Menu Stay SubStates/Pause Menu Localization SubStates/Pause Menu Localization Enter State")]
public class PauseMenuLocalizationEnterState : GameProcessBaseState {

    override public void UpdateState(IGameProcessManager manager) {
        if (manager.ControllerFactory.PauseMenuLocalizationFadeController.IsFadeComplited &&
            manager.ControllerFactory.PauseMenuDefaultFadeController.IsUnfadeComplited &&
            manager.ControllerFactory.PauseMenuScreenFadeController.IsUnfadeComplited &&
            manager.ControllerFactory.PauseMenuDialogsFadeController.IsUnfadeComplited &&
            manager.ControllerFactory.PauseMenuAudioFadeController.IsUnfadeComplited) {
            manager.SwitchState(((PauseMenuLocalizationStateFactory)manager.StateFactory).PauseMenuLocalizationStayState);
        }
    }

    override public void OnStateEnter(IGameProcessManager manager) {}

    override public void OnStateExit(IGameProcessManager manager) {}

    override public void Act(IGameProcessManager manager) {
        manager.ControllerFactory.PauseMenuLocalizationFadeController.FadeIn();
        manager.ControllerFactory.PauseMenuDefaultFadeController.FadeOut();
        manager.ControllerFactory.PauseMenuScreenFadeController.FadeOut();
        manager.ControllerFactory.PauseMenuDialogsFadeController.FadeOut();
        manager.ControllerFactory.PauseMenuAudioFadeController.FadeOut();
    }
}
