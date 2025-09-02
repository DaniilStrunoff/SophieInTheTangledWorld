public interface IGameProcessState {
    public void UpdateState(IGameProcessManager manager);
    public void OnStateEnter(IGameProcessManager manager);
    public void OnStateExit(IGameProcessManager manager);
    public void Act(IGameProcessManager manager);
}
