using UnityEngine;


public class GameProcessManager : MonoBehaviour, IGameProcessManager {
    [Header("Controllers Settings")]
    [SerializeField]
    private GameProcessControllerFactory controllerFactory;
    public GameProcessControllerFactory ControllerFactory => controllerFactory;

    [Header("State Factory")]
    [SerializeField]
    private GameProcessManagerStateFactory stateFactory;
    public IGameProcessStateFactory StateFactory => stateFactory;

    public GameProcessBaseState StartingState;

    void Update() {
        stateFactory.CurrentState.UpdateState(this);
        stateFactory.CurrentState.Act(this);
    }

    public void Start() {
        SwitchState(stateFactory.SceneEnterState);
        controllerFactory.ScenesContoller.ExitSceneEvent += SwitchState;
        controllerFactory.DialogController.DialogEvent += () => SwitchState(stateFactory.DialogState);
        controllerFactory.DialogController.EndOfDialogEvent += SwitchState;
    }

    public void SwitchState(GameProcessBaseState state) {
        Debug.Log($"{stateFactory.CurrentState}, {state}");
        stateFactory.CurrentState.OnStateExit(this);
        stateFactory.CurrentState = state;
        stateFactory.CurrentState.OnStateEnter(this);   
    }
}
