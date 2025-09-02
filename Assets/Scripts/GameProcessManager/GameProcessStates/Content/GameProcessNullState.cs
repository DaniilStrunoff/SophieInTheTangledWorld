
using UnityEngine;

[CreateAssetMenu(fileName = "GameProcessNullState", menuName = "Game Process States/Game Process Null State")]
public class GameProcessNullState : GameProcessBaseState  {
    override public void UpdateState(IGameProcessManager manager) {}
    override public void OnStateEnter(IGameProcessManager manager) {}
    override public void OnStateExit(IGameProcessManager manager) {}
    override public void Act(IGameProcessManager manager) {}
}
