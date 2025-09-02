public abstract class BaseState {
    abstract public void OnStateEnter(BaseStateManager manager);
    abstract public void OnStateExit(BaseStateManager manager);
    abstract public void UpdateState(BaseStateManager manager);
    abstract public void Act(BaseStateManager manager);
    abstract public void LateAct(BaseStateManager manager);
}
