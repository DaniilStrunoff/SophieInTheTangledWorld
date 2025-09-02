using System;
using UnityEngine;


[CreateAssetMenu(fileName = "MainMenuState", menuName = "Game Process States/Main Menu State")]
public class MainMenuState : SuperState {
    override public GameProcessControllerFactory ControllerFactory => controllerFactory;

    [SerializeField]
    private MainMenuStateFactory stateFactory;
    override public IGameProcessStateFactory StateFactory => stateFactory;

    public Action OpenOptions;

    override public void OnStateEnter(IGameProcessManager manager) {
        base.OnStateEnter(manager);
        controllerFactory.PlayerInputController.EnableUISchemaOnly();
        controllerFactory.PauseMenuController.SetLastState(this);
        controllerFactory.PlayerInputController.SetSelectedGameObjectToNull();
        OpenOptions = () => manager.SwitchState(((GameProcessManagerStateFactory)((GameProcessManager)manager).StateFactory).PauseMenuState);
        SwitchState(stateFactory.MainMenuEnterState);
    }

    public override void OnStateExit(IGameProcessManager manager) {
        base.OnStateExit(manager);
    }
}
