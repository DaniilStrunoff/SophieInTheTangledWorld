using System;
using UnityEngine;


[CreateAssetMenu(fileName = "GameplayState", menuName = "Game Process States/Gameplay State")]
public class GameplayState : SuperState {
    override public GameProcessControllerFactory ControllerFactory => controllerFactory;

    [SerializeField]
    private GameplayStateFactory stateFactory;
    override public IGameProcessStateFactory StateFactory => stateFactory;

    private void OnPause(IGameProcessManager manager) {
        var stateFactory = (GameProcessManagerStateFactory)((GameProcessManager)manager).StateFactory;
        manager.SwitchState(stateFactory.PauseMenuState);
    }

    private void OnInputActionPause(IGameProcessManager manager) {
        var stateFactory = (GameProcessManagerStateFactory)((GameProcessManager)manager).StateFactory;
        manager.SwitchState(stateFactory.PauseMenuState);
    }

    private Action OnPauseAction;
    private Action OnInputActionPauseAction;

    public override void UpdateState(IGameProcessManager manager) {
        base.UpdateState(manager);
    }

    override public void OnStateEnter(IGameProcessManager manager) {
        base.OnStateEnter(manager);
        controllerFactory.PauseMenuController.SetLastState(this);
        controllerFactory.PlayerInputController.EnableAll();
        stateFactory.CurrentState = stateFactory.GameplayEnterState;
        stateFactory.CurrentState.OnStateEnter(this);
        OnPauseAction = () => OnPause(manager);
        controllerFactory.PauseMenuController.PauseEvent += OnPauseAction;
        OnInputActionPauseAction = () => OnInputActionPause(manager);
        controllerFactory.PauseMenuController.InputActionPauseEvent += OnInputActionPauseAction;
    }

    override public void OnStateExit(IGameProcessManager manager) {
        base.OnStateExit(manager);
        controllerFactory.PauseMenuController.PauseEvent -= OnPauseAction;
        controllerFactory.PauseMenuController.InputActionPauseEvent -= OnInputActionPauseAction;
    }
}
