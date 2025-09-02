public interface IInteractionManager {
    public InteractionManagerControllerFactory ControllerFactory {get;}
    public IInteractionManagerStateFactory StateFactory {get;}
    public void SwitchState(InteractionBaseState state);
}
