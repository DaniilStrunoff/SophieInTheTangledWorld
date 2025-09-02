using System;
using UnityEngine;


[CreateAssetMenu(fileName = "MainMenuStayState", menuName = "Game Process States/Main Menu SubStates/Main Menu Stay State")]
public class MainMenuStayState : SuperState {
    override public GameProcessControllerFactory ControllerFactory => controllerFactory;

    [SerializeField]
    private MainMenuStayStateFactory stateFactory;
    override public IGameProcessStateFactory StateFactory => stateFactory;

    public void SwitchToDefaultMenu() {
        SwitchState(stateFactory.MainMenuDefaultState);
    }

    public void SwitchToConfirmationMenu() {
        SwitchState(stateFactory.MainMenuConfirmationState);
    }

    public Action OpenOptions;

    override public void UpdateState(IGameProcessManager manager) {
        base.UpdateState(manager);
    }

    override public void OnStateEnter(IGameProcessManager manager) {
        base.OnStateEnter(manager);
        OpenOptions = ((MainMenuState)manager).OpenOptions;
        SwitchState(stateFactory.MainMenuDefaultState);
    }

    override public void OnStateExit(IGameProcessManager manager) {
        base.OnStateExit(manager);
    }
}
