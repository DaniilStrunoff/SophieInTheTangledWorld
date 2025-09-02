using UnityEngine;


[CreateAssetMenu(fileName = "DialogPauseState", menuName = "Game Process States/Dialog SubStates/Dialog Stay SubStates/Dialog Pause State")]
public class DialogPauseState : SuperState {
    override public GameProcessControllerFactory ControllerFactory => controllerFactory;

    [SerializeField]
    private DialogPauseStateFactory stateFactory;
    override public IGameProcessStateFactory StateFactory => stateFactory;
    override public void OnStateEnter(IGameProcessManager manager) {
        base.OnStateEnter(manager);
        controllerFactory.DialogController.IsInPouseState = true;
        manager.ControllerFactory.DialogController.PauseTimeline();
        manager.ControllerFactory.AutoButtonController.SetAutoButtonInactive();
        manager.ControllerFactory.DialogController.DialogAutoPlayEvent += ((DialogStayState)manager).SwitchToAutoPlay;
        manager.ControllerFactory.DialogController.DialogPlayEvent += ((DialogStayState)manager).SwitchToPlay;
        SwitchState(stateFactory.DialogPauseEnterState);
    }

    public override void OnStateExit(IGameProcessManager manager) {
        base.OnStateExit(manager);
        manager.ControllerFactory.DialogController.PauseTimeline();
        manager.ControllerFactory.DialogController.DialogAutoPlayEvent -= ((DialogStayState)manager).SwitchToAutoPlay;
        manager.ControllerFactory.DialogController.DialogPlayEvent -= ((DialogStayState)manager).SwitchToPlay;
    }
}
