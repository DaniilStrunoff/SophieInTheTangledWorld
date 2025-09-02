using UnityEngine;

[CreateAssetMenu(fileName = "LoadSceneEnterState", menuName = "Game Process States/Load Scene SubStates/Load Scene Enter State")]
public class LoadSceneEnterState : GameProcessBaseState {

    override public void UpdateState(IGameProcessManager manager) {
        if (manager.ControllerFactory.SceneFadeController.IsFadeComplited &&
            manager.ControllerFactory.GameplayFadeController.IsUnfadeComplited &&
            manager.ControllerFactory.DialogFadeController.IsUnfadeComplited &&
            manager.ControllerFactory.ScenesContoller.IsSceneLoaded) {
            manager.SwitchState(((LoadSceneStateFactory)manager.StateFactory).LoadSceneStayState);
        }
    }

    override public void OnStateEnter(IGameProcessManager manager) {
        manager.ControllerFactory.TimeScaleController.SetTimeScaleToZero();
        manager.ControllerFactory.ScenesContoller.LoadScene(((LoadSceneState)manager).NextScene);
    }

    override public void OnStateExit(IGameProcessManager manager) {}

    override public void Act(IGameProcessManager manager) {
        manager.ControllerFactory.SceneFadeController.FadeIn();
        manager.ControllerFactory.GameplayFadeController.FadeOut();
        manager.ControllerFactory.DialogFadeController.FadeOut();
    }
}
