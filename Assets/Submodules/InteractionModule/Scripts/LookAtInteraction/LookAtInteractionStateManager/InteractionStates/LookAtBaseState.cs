using UnityEngine;

public abstract class LookAtBaseState : ScriptableObject, ILookAtState {
    abstract public void Act(ILookAtManager manager);
    abstract public void OnStateEnter(ILookAtManager manager);
    abstract public void OnStateExit(ILookAtManager manager);
    abstract public void UpdateState(ILookAtManager manager);
}
