using UnityEngine;


[RequireComponent(typeof(InteractionManagerControllerFactory))]
public class InteractionManager : MonoBehaviour, IInteractionManager {
    [Header("Controllers Settings")]
    [SerializeField]
    private InteractionManagerControllerFactory controllerFactory;
    public InteractionManagerControllerFactory ControllerFactory => controllerFactory;

    [Header("State Factory")]
    private InteractionManagerStateFactory stateFactory;
    public IInteractionManagerStateFactory StateFactory => stateFactory;

    public void SwitchToInteractionEnterState() {
        SwitchState(stateFactory.InteractionEnterState);
    }

    public void SwitchToInteractionExitState() {
        SwitchState(stateFactory.InteractionExitState);
    }

    public void SwitchToInteractionStayState() {
        SwitchState(stateFactory.InteractionStayState);
    }

    public void SwitchToNoInteractionState() {
        SwitchState(stateFactory.NoInteractionState);
    }

    void Start() {
        stateFactory = ScriptableObject.CreateInstance<InteractionManagerStateFactory>();
        SwitchState(stateFactory.NoInteractionState);
        controllerFactory.UIElementController.TriggerEnterEvent += SwitchToInteractionEnterState;
        controllerFactory.UIElementController.TriggerExitEvent += SwitchToInteractionExitState;
    }

    void Update() {
        stateFactory.CurrentState.UpdateState(this);
        stateFactory.CurrentState.Act(this);
    }

    public void SwitchState(InteractionBaseState state) {
        stateFactory.CurrentState.OnStateExit(this);
        stateFactory.CurrentState = state;
        stateFactory.CurrentState.OnStateEnter(this);
    }
}
