using UnityEngine;


[CreateAssetMenu(fileName = "PauseMenuDefaultEnterState", menuName = "Game Process States/Pause Menu SubStates/Pause Menu Stay SubStates/Pause Menu Default SubStates/Pause Menu Default Enter State")]
public class PauseMenuDefaultEnterState : GameProcessBaseState {

    override public void UpdateState(IGameProcessManager manager) {
        if (manager.ControllerFactory.PauseMenuDefaultFadeController.IsFadeComplited &&
            manager.ControllerFactory.PauseMenuLocalizationFadeController.IsUnfadeComplited &&
            manager.ControllerFactory.PauseMenuScreenFadeController.IsUnfadeComplited &&
            manager.ControllerFactory.PauseMenuDialogsFadeController.IsUnfadeComplited &&
            manager.ControllerFactory.PauseMenuAudioFadeController.IsUnfadeComplited) {
            manager.SwitchState(((PauseMenuDefaultStateFactory)manager.StateFactory).PauseMenuDefaultStayState);
        }
    }

    override public void OnStateEnter(IGameProcessManager manager) {}

    override public void OnStateExit(IGameProcessManager manager) {}

    override public void Act(IGameProcessManager manager) {
        manager.ControllerFactory.PauseMenuDefaultFadeController.FadeIn();
        manager.ControllerFactory.PauseMenuLocalizationFadeController.FadeOut();
        manager.ControllerFactory.PauseMenuScreenFadeController.FadeOut();
        manager.ControllerFactory.PauseMenuDialogsFadeController.FadeOut();
        manager.ControllerFactory.PauseMenuAudioFadeController.FadeOut();
    }
}
