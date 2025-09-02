using UnityEngine;


[CreateAssetMenu(fileName = "GameplayStayState", menuName = "Game Process States/Gameplay SubStates/Gameplay Stay State")]
public class GameplayStayState : GameProcessBaseState {
    override public void UpdateState(IGameProcessManager manager) {}

    override public void OnStateEnter(IGameProcessManager manager) {
        manager.ControllerFactory.TimeScaleController.SetTimeScaleToNormal();
    }

    override public void OnStateExit(IGameProcessManager manager) {}

    override public void Act(IGameProcessManager manager) {}
}
