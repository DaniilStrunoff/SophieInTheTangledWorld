using System;
using UnityEngine;


[CreateAssetMenu(fileName = "PauseMenuState", menuName = "Game Process States/Pause Menu State")]
public class PauseMenuState : SuperState {
    override public GameProcessControllerFactory ControllerFactory => controllerFactory;

    [SerializeField]
    private PauseMenuStateFactory stateFactory;
    override public IGameProcessStateFactory StateFactory => stateFactory;

    private void Unpause(IGameProcessManager manager) {
        manager.SwitchState(controllerFactory.PauseMenuController.LastState);
    }

    public Action UnpauseAction;

    override public void OnStateEnter(IGameProcessManager manager) {
        base.OnStateEnter(manager);
        UnpauseAction = () => Unpause(manager);
        controllerFactory.PauseMenuController.PauseEvent += UnpauseAction;
        controllerFactory.PauseMenuController.InputActionPauseEvent += UnpauseAction;
        controllerFactory.PlayerInputController.EnableUISchemaOnly();
        manager.ControllerFactory.PlayerInputController.SetSelectedGameObjectToNull();
        SwitchState(stateFactory.PauseMenuEnterState);
    }

    public override void OnStateExit(IGameProcessManager manager) {
        base.OnStateExit(manager);
        controllerFactory.PauseMenuController.PauseEvent -= UnpauseAction;
        controllerFactory.PauseMenuController.InputActionPauseEvent -= UnpauseAction;
        controllerFactory.PauseMenuController.SetDefaultMenuSelectedObject(controllerFactory.PauseMenuController.FirstDefaultMenuSelectedObject);
    }
}
