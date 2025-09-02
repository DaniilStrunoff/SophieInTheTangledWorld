public interface IInteractionState {
    public void UpdateState(IInteractionManager manager);
    public void OnStateEnter(IInteractionManager manager);
    public void OnStateExit(IInteractionManager manager);
    public void Act(IInteractionManager manager);
}
