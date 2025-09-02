public interface ILookAtManager {
    public LookAtManagerControllerFactory ControllerFactory {get;}
    public ILookAtManagerStateFactory StateFactory {get;}
    public void SwitchState(LookAtBaseState state);
}
