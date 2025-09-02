using System;
using UnityEngine;
using UnityEngine.InputSystem;


[CreateAssetMenu(fileName = "PauseMenuDefaultState", menuName = "Game Process States/Pause Menu SubStates/Pause Menu Stay SubStates/Pause Menu Default State")]
public class PauseMenuDefaultState : SuperState {
    override public GameProcessControllerFactory ControllerFactory => controllerFactory;

    [SerializeField]
    private PauseMenuDefaultStateFactory stateFactory;
    override public IGameProcessStateFactory StateFactory => stateFactory;

    override public void OnStateEnter(IGameProcessManager manager) {
        base.OnStateEnter(manager);
        SwitchState(stateFactory.PauseMenuDefaultEnterState);
        controllerFactory.PauseMenuController.PauseLocalizationMenuEvent += ((PauseMenuStayState)manager).SwitchToLocalizationMenu;
        controllerFactory.PauseMenuController.PauseScreenMenuEvent += ((PauseMenuStayState)manager).SwitchToScreenMenu;
        controllerFactory.PauseMenuController.PauseDialogMenuEvent += ((PauseMenuStayState)manager).SwitchToDialogMenu;
        controllerFactory.PauseMenuController.PauseAudioMenuEvent += ((PauseMenuStayState)manager).SwitchToAudioMenu;
        controllerFactory.PauseMenuController.BackEvent += ((PauseMenuStayState)manager).UnpauseAction;
        controllerFactory.PauseMenuController.InputActionBackEvent += ((PauseMenuStayState)manager).UnpauseAction;
    }

    override public void OnStateExit(IGameProcessManager manager) {
        base.OnStateExit(manager);
        controllerFactory.PauseMenuController.PauseLocalizationMenuEvent -= ((PauseMenuStayState)manager).SwitchToLocalizationMenu;
        controllerFactory.PauseMenuController.PauseScreenMenuEvent -= ((PauseMenuStayState)manager).SwitchToScreenMenu;
        controllerFactory.PauseMenuController.PauseDialogMenuEvent -= ((PauseMenuStayState)manager).SwitchToDialogMenu;
        controllerFactory.PauseMenuController.PauseAudioMenuEvent -= ((PauseMenuStayState)manager).SwitchToAudioMenu;
        controllerFactory.PauseMenuController.BackEvent -= ((PauseMenuStayState)manager).UnpauseAction;
        controllerFactory.PauseMenuController.InputActionBackEvent -= ((PauseMenuStayState)manager).UnpauseAction;
    }
}
