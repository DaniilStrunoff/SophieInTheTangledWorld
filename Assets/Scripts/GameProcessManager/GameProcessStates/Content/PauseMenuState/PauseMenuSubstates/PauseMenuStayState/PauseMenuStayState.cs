using System;
using UnityEngine;


[CreateAssetMenu(fileName = "PauseMenuStayState", menuName = "Game Process States/Pause Menu SubStates/Pause Menu Stay State")]
public class PauseMenuStayState : SuperState {
    override public GameProcessControllerFactory ControllerFactory => controllerFactory;

    [SerializeField]
    private PauseMenuStayStateFactory stateFactory;
    override public IGameProcessStateFactory StateFactory => stateFactory;

    public void SwitchToLocalizationMenu() {
        SwitchState(stateFactory.PauseMenuLocalizationState);
    }

    public void SwitchToScreenMenu() {
        SwitchState(stateFactory.PauseMenuScreenState);
    }

    public void SwitchToDefaultMenu() {
        SwitchState(stateFactory.PauseMenuDefaultState);
    }

    public void SwitchToDialogMenu() {
        SwitchState(stateFactory.PauseMenuDialogState);
    }

    public void SwitchToAudioMenu() {
        SwitchState(stateFactory.PauseMenuAudioState);
    }

    public void SwitchToDefaultMenuViaInputAction() {
        SwitchState(stateFactory.PauseMenuDefaultState);
    }

    private void Unpause(IGameProcessManager manager) {
        if (((PauseMenuState)manager).UnpauseAction == null) return;
        ((PauseMenuState)manager).UnpauseAction.Invoke();
    }

    public Action UnpauseAction;

    override public void UpdateState(IGameProcessManager manager) {
        base.UpdateState(manager);
    }

    override public void OnStateEnter(IGameProcessManager manager) {
        base.OnStateEnter(manager);
        manager.ControllerFactory.TimeScaleController.SetTimeScaleToZero();
        UnpauseAction = () => Unpause(manager);
        SwitchState(stateFactory.PauseMenuDefaultState);
    }

    override public void OnStateExit(IGameProcessManager manager) {
        base.OnStateExit(manager);
    }
}
