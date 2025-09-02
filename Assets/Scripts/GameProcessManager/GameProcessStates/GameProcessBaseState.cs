using UnityEngine;

public abstract class GameProcessBaseState : ScriptableObject, IGameProcessState {
    abstract public void UpdateState(IGameProcessManager manager);
    abstract public void OnStateEnter(IGameProcessManager manager);
    abstract public void OnStateExit(IGameProcessManager manager);
    abstract public void Act(IGameProcessManager manager);
}
