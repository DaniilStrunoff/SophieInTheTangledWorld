using UnityEngine;


[CreateAssetMenu(fileName = "DialogPlayState", menuName = "Game Process States/Dialog SubStates/Dialog Stay SubStates/Dialog Play State")]
public class DialogPlayState : SuperState {
    override public GameProcessControllerFactory ControllerFactory => controllerFactory;

    [SerializeField]
    private DialogPlayStateFactory stateFactory;
    override public IGameProcessStateFactory StateFactory => stateFactory;

    override public void OnStateEnter(IGameProcessManager manager) {
        base.OnStateEnter(manager);
        SaveAndLoadController.GetInstance().SaveData.AutoDialog = false;
        SaveAndLoadController.GetInstance().Save();
        controllerFactory.DialogController.IsInPouseState = false;
        controllerFactory.DialogController.PlayTimeline();
        controllerFactory.AutoButtonController.SetAutoButtonInactive();
        controllerFactory.DialogController.DialogAutoPlayEvent += ((DialogStayState)manager).SwitchToAutoPlay;
        controllerFactory.DialogController.DialogPauseEvent += ((DialogStayState)manager).SwitchToPause;
        controllerFactory.DialogController.DialogSetSpeedEvent += controllerFactory.DialogController.PlayTimeline;
        stateFactory.CurrentState = stateFactory.DialogPlayEnterState;
        stateFactory.CurrentState.OnStateEnter(this);
    }

    override public void OnStateExit(IGameProcessManager manager) {
        base.OnStateExit(manager);
        controllerFactory.DialogController.PauseTimeline();
        controllerFactory.DialogController.DialogAutoPlayEvent -= ((DialogStayState)manager).SwitchToAutoPlay;
        controllerFactory.DialogController.DialogPauseEvent -= ((DialogStayState)manager).SwitchToPause;
        controllerFactory.DialogController.DialogSetSpeedEvent -= controllerFactory.DialogController.PlayTimeline;
    }
}
