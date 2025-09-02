public interface IGameProcessManager {
    public GameProcessControllerFactory ControllerFactory {get;}
    public IGameProcessStateFactory StateFactory {get;}

    public void SwitchState(GameProcessBaseState state);
}
