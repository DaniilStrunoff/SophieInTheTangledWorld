using System;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogStayState", menuName = "Game Process States/Dialog SubStates/Dialog Stay State")]
public class DialogStayState : SuperState {
    override public GameProcessControllerFactory ControllerFactory => controllerFactory;

    public Action SwitchToAutoPlay ;
    public Action SwitchToPause;
    public Action SwitchToPlay;

    [SerializeField]
    private DialogStayStateFactory stateFactory;
    override public IGameProcessStateFactory StateFactory => stateFactory;

    public override void UpdateState(IGameProcessManager manager) {
        base.UpdateState(manager);
    }

    override public void OnStateEnter(IGameProcessManager manager) {
        base.OnStateEnter(manager);
        SwitchToAutoPlay = () => SwitchState(stateFactory.DialogAutoPlayState);
        SwitchToPause = () => SwitchState(stateFactory.DialogPauseState);
        SwitchToPlay = () => SwitchState(stateFactory.DialogPlayState);
        if (SaveAndLoadController.GetInstance().SaveData.AutoDialog && Debug.isDebugBuild)
            stateFactory.CurrentState = stateFactory.DialogAutoPlayState;
        else if (controllerFactory.DialogController.IsInPouseState) {
            stateFactory.CurrentState = stateFactory.DialogPauseState;
        } else stateFactory.CurrentState = stateFactory.DialogPlayState;
        stateFactory.CurrentState.OnStateEnter(this);
    }
}
