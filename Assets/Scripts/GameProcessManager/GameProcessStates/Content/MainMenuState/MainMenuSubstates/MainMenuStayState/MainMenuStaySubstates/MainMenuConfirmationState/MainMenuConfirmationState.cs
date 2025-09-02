using System;
using UnityEngine;
using UnityEngine.InputSystem;


[CreateAssetMenu(fileName = "MainMenuConfirmationState", menuName = "Game Process States/Main Menu SubStates/Main Menu Stay SubStates/Main Menu Confirmation State")]
public class MainMenuConfirmationState : SuperState {
    override public GameProcessControllerFactory ControllerFactory => controllerFactory;

    [SerializeField]
    private MainMenuConfirmationStateFactory stateFactory;
    override public IGameProcessStateFactory StateFactory => stateFactory;

    override public void OnStateEnter(IGameProcessManager manager) {
        base.OnStateEnter(manager);
        SwitchState(stateFactory.MainMenuConfirmationEnterState);
        controllerFactory.MainMenuController.BackEvent += ((MainMenuStayState)manager).SwitchToDefaultMenu;
        controllerFactory.MainMenuController.InputActionBackEvent += ((MainMenuStayState)manager).SwitchToDefaultMenu;
    }

    override public void OnStateExit(IGameProcessManager manager) {
        base.OnStateExit(manager);
        controllerFactory.MainMenuController.BackEvent -= ((MainMenuStayState)manager).SwitchToDefaultMenu;
        controllerFactory.MainMenuController.InputActionBackEvent -= ((MainMenuStayState)manager).SwitchToDefaultMenu;
    }
}
