using UnityEngine;

public abstract class SuperState : GameProcessBaseState, IGameProcessManager {
    protected GameProcessControllerFactory controllerFactory;
    virtual public GameProcessControllerFactory ControllerFactory => controllerFactory;

    virtual public IGameProcessStateFactory StateFactory {get;}

    override public void UpdateState(IGameProcessManager manager) {
        StateFactory.CurrentState.UpdateState(this);
    }

    override public void OnStateEnter(IGameProcessManager manager) {
        controllerFactory = manager.ControllerFactory;
        controllerFactory.PlayerInputController.ManageCursorRaycasting();
    }

    override public void OnStateExit(IGameProcessManager manager) {
        StateFactory.CurrentState.OnStateExit(this);
    }

    override public void Act(IGameProcessManager manager) {
        StateFactory.CurrentState.Act(this);
    }

    public virtual void SwitchState(GameProcessBaseState state) {
        StateFactory.CurrentState.OnStateExit(this);
        StateFactory.CurrentState = state;
        StateFactory.CurrentState.OnStateEnter(this);
    }
}
