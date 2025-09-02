using System;
using UnityEngine;
using UnityEngine.InputSystem;


[CreateAssetMenu(fileName = "PauseMenuAudioState", menuName = "Game Process States/Pause Menu SubStates/Pause Menu Stay SubStates/Pause Menu Audio State")]
public class PauseMenuAudioState : SuperState {
    override public GameProcessControllerFactory ControllerFactory => controllerFactory;

    [SerializeField]
    private PauseMenuAudioStateFactory stateFactory;
    override public IGameProcessStateFactory StateFactory => stateFactory;

    override public void OnStateEnter(IGameProcessManager manager) {
        base.OnStateEnter(manager);
        SwitchState(stateFactory.PauseMenuAudioEnterState);
        manager.ControllerFactory.PauseMenuController.BackEvent += ((PauseMenuStayState)manager).SwitchToDefaultMenu;
        manager.ControllerFactory.PauseMenuController.InputActionBackEvent += ((PauseMenuStayState)manager).SwitchToDefaultMenuViaInputAction;
    }

    override public void OnStateExit(IGameProcessManager manager) {
        base.OnStateExit(manager);
        manager.ControllerFactory.PauseMenuController.BackEvent -= ((PauseMenuStayState)manager).SwitchToDefaultMenu;
        manager.ControllerFactory.PauseMenuController.InputActionBackEvent -= ((PauseMenuStayState)manager).SwitchToDefaultMenuViaInputAction;
    }
}
