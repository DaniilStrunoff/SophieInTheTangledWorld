using System;
using UnityEngine;
using UnityEngine.InputSystem;


[CreateAssetMenu(fileName = "MainMenuDefaultState", menuName = "Game Process States/Main Menu SubStates/Main Menu Stay SubStates/Main Menu Default State")]
public class MainMenuDefaultState : SuperState {
    override public GameProcessControllerFactory ControllerFactory => controllerFactory;

    [SerializeField]
    private MainMenuDefaultStateFactory stateFactory;
    override public IGameProcessStateFactory StateFactory => stateFactory;

    public Action OpenOptions;

    override public void OnStateEnter(IGameProcessManager manager) {
        base.OnStateEnter(manager);
        SwitchState(stateFactory.MainMenuDefaultEnterState);
        controllerFactory.MainMenuController.ConfirmationMenuEvent += ((MainMenuStayState)manager).SwitchToConfirmationMenu;
        OpenOptions = ((MainMenuStayState)manager).OpenOptions;
        controllerFactory.PauseMenuController.PauseEvent += OpenOptions;
    }

    override public void OnStateExit(IGameProcessManager manager) {
        base.OnStateExit(manager);
        controllerFactory.MainMenuController.ConfirmationMenuEvent -= ((MainMenuStayState)manager).SwitchToConfirmationMenu;
        controllerFactory.PauseMenuController.PauseEvent -= OpenOptions;
    }
}
