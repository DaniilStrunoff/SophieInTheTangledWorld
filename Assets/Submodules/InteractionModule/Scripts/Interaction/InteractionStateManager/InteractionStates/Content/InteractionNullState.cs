using UnityEngine;

[CreateAssetMenu(fileName = "InteractionNullState", menuName = "Interaction States/Interaction Null State")]
public class InteractionNullState : InteractionBaseState {
    override public void UpdateState(IInteractionManager manager) {}

    override public void OnStateEnter(IInteractionManager manager) {}

    override public void OnStateExit(IInteractionManager manager) {}

    override public void Act(IInteractionManager manager) {}
}
