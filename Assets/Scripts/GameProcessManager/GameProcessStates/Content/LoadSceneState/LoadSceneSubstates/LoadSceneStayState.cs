using UnityEngine;

[CreateAssetMenu(fileName = "LoadSceneStayState", menuName = "Game Process States/Load Scene SubStates/Load Scene Stay State")]
public class LoadSceneStayState : GameProcessBaseState {
    override public void UpdateState(IGameProcessManager manager) {}

    override public void OnStateEnter(IGameProcessManager manager) {
        manager.ControllerFactory.ScenesContoller.ActivateScene();
    }

    override public void OnStateExit(IGameProcessManager manager) {}

    override public void Act(IGameProcessManager manager) {}
}
