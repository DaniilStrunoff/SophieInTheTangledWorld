using UnityEngine;

public abstract class InteractionBaseState : ScriptableObject, IInteractionState {
    abstract public void Act(IInteractionManager manager);
    abstract public void OnStateEnter(IInteractionManager manager);
    abstract public void OnStateExit(IInteractionManager manager);
    abstract public void UpdateState(IInteractionManager manager);
}
