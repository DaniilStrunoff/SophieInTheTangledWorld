public interface ILookAtState {
    public void UpdateState(ILookAtManager manager);
    public void OnStateEnter(ILookAtManager manager);
    public void OnStateExit(ILookAtManager manager);
    public void Act(ILookAtManager manager);
}
