using UnityEngine;


public class LookAtManager : MonoBehaviour, ILookAtManager {
    [Header("Controllers Settings")]
    [SerializeField]
    private LookAtManagerControllerFactory controllerFactory;
    public LookAtManagerControllerFactory ControllerFactory => controllerFactory;

    [Header("State Factory")]
    private LookAtManagerStateFactory stateFactory;
    public ILookAtManagerStateFactory StateFactory => stateFactory;

    public void SwitchToLookAtEnterState(LookAtObject lookAtObject) {
        lookAtObject.SetActiveEvent -= controllerFactory.LookAtController.InvokeTriggerLookAtEnterEvent; // SwitchToLookAtEnterState;
        lookAtObject.SetNotActiveEvent -= controllerFactory.LookAtController.InvokeTriggerLookAtExitEvent; //SwitchToLookAtExitState;
        lookAtObject.SetActiveEvent += controllerFactory.LookAtController.InvokeTriggerLookAtEnterEvent;
        lookAtObject.SetNotActiveEvent += controllerFactory.LookAtController.InvokeTriggerLookAtExitEvent;
        if (lookAtObject.IsActive) {
            controllerFactory.LookAtController.SetValues(lookAtObject);
            SwitchState(stateFactory.LookAtEnterState);
        }
    }

    public void SwitchToLookAtChangeState(LookAtObject lookAtObject) {
        lookAtObject.SetActiveEvent -= controllerFactory.LookAtController.InvokeTriggerLookAtEnterEvent; // SwitchToLookAtEnterState;
        lookAtObject.SetNotActiveEvent -= controllerFactory.LookAtController.InvokeTriggerLookAtExitEvent; //SwitchToLookAtExitState;
        lookAtObject.SetActiveEvent += controllerFactory.LookAtController.InvokeTriggerLookAtEnterEvent;
        lookAtObject.SetNotActiveEvent += controllerFactory.LookAtController.InvokeTriggerLookAtExitEvent;
        if (lookAtObject.IsActive) {
            controllerFactory.LookAtController.SetNextObjectToLookAt(lookAtObject);
            SwitchState(stateFactory.LookAtChangeState);
        }
    }

    public void SwitchToLookAtExitState(LookAtObject lookAtObject) {
        controllerFactory.LookAtController.SetValues(lookAtObject);
        SwitchState(stateFactory.LookAtExitState);
    }

    void Start() {
        stateFactory = ScriptableObject.CreateInstance<LookAtManagerStateFactory>();
        controllerFactory = gameObject.AddComponent(typeof(LookAtManagerControllerFactory)) as LookAtManagerControllerFactory;
        controllerFactory.LookAtController = gameObject.AddComponent(typeof(LookAtController)) as LookAtController;
        SwitchState(stateFactory.NoLookAtState);
    }

    void Update() {
        stateFactory.CurrentState.UpdateState(this);
        stateFactory.CurrentState.Act(this);
    }

    public void SwitchState(LookAtBaseState state) {
        stateFactory.CurrentState.OnStateExit(this);
        stateFactory.CurrentState = state;
        stateFactory.CurrentState.OnStateEnter(this);
    }
}
