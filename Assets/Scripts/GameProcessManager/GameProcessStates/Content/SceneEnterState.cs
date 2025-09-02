using UnityEngine;

[CreateAssetMenu(fileName = "SceneEnterState", menuName = "Game Process States/Scene Enter State")]
public class SceneEnterState : GameProcessBaseState {
    private float timeToWait;

    override public void UpdateState(IGameProcessManager manager) {
        if (manager.ControllerFactory.SceneFadeController.IsUnfadeComplited) {
            manager.SwitchState(((GameProcessManager)manager).StartingState);
        }
    }

    override public void OnStateEnter(IGameProcessManager manager) {
        manager.ControllerFactory.DialogController.IsInPouseState = false;
        manager.ControllerFactory.TimeScaleController.SetTimeScaleToNormal();
        manager.ControllerFactory.PlayerInputController.DisableAll();
        manager.ControllerFactory.AudioController.PlayMusic(manager.ControllerFactory.AudioController.AudioClipOnSceneLoad);
        timeToWait = 0.1f;
    }

    override public void OnStateExit(IGameProcessManager manager) {
        manager.ControllerFactory.PauseMenuController.SetLastState(this);
    }

    override public void Act(IGameProcessManager manager) {
        if (timeToWait > 0) {
            timeToWait -= Time.deltaTime;
            return;
        }
        manager.ControllerFactory.SceneFadeController.FadeOut();
    }
}
