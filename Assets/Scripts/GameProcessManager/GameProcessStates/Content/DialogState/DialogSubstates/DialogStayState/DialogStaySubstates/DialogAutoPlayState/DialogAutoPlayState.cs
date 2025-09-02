using System;
using System.Collections;
using UnityEngine;


[CreateAssetMenu(fileName = "DialogAutoPlayState", menuName = "Game Process States/Dialog SubStates/Dialog Stay SubStates/Dialog Auto Play State")]
public class DialogAutoPlayState : SuperState {
    override public GameProcessControllerFactory ControllerFactory => controllerFactory;

    [SerializeField]
    private DialogAutoPlayStateFactory stateFactory;
    override public IGameProcessStateFactory StateFactory => stateFactory;

    override public void OnStateEnter(IGameProcessManager manager) {
        base.OnStateEnter(manager);
        SaveAndLoadController.GetInstance().SaveData.AutoDialog = true;
        SaveAndLoadController.GetInstance().Save();
        controllerFactory.DialogController.IsInPouseState = false;
        controllerFactory.DialogController.PlayTimeline();
        controllerFactory.AutoButtonController.SetAutoButtonActive();
        controllerFactory.DialogController.DialogAutoPlayEvent += ((DialogStayState)manager).SwitchToPlay;
        controllerFactory.DialogController.DialogSetSpeedEvent += controllerFactory.DialogController.PlayTimeline;
        controllerFactory.DialogController.DialogPauseEvent += controllerFactory.DialogController.StartWaitCoroutine;
        stateFactory.CurrentState = stateFactory.DialogAutoPlayEnterState;
        stateFactory.CurrentState.OnStateEnter(this);
    }

    override public void OnStateExit(IGameProcessManager manager) {
        base.OnStateExit(manager);
        controllerFactory.DialogController.PauseTimeline();
        controllerFactory.DialogController.DialogAutoPlayEvent -= ((DialogStayState)manager).SwitchToPlay;
        controllerFactory.DialogController.DialogSetSpeedEvent -= controllerFactory.DialogController.PlayTimeline;
        controllerFactory.DialogController.DialogPauseEvent -= controllerFactory.DialogController.StartWaitCoroutine;
        controllerFactory.DialogController.StopAllCoroutines();
    }
}
