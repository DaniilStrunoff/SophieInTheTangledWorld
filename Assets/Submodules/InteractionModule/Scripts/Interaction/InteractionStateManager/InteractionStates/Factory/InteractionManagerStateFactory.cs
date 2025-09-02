using UnityEngine;

[CreateAssetMenu(fileName = "InteractionManagerStateFactory", menuName = "Factories/Interaction Manager State Factory")]
public class InteractionManagerStateFactory: ScriptableObject, IInteractionManagerStateFactory {
    public NoInteractionState NoInteractionState;
    public InteractionEnterState InteractionEnterState;
    public InteractionStayState InteractionStayState;
    public InteractionExitState InteractionExitState;

    [SerializeField]
    private InteractionNullState nullState;
    private InteractionBaseState currentState;
    private bool isNotSet;
    public InteractionBaseState CurrentState {
        get {
            if (isNotSet) return nullState;
            return currentState;
        }
        set {
            isNotSet = false;
            currentState = value;
        }
    }

    public void OnEnable() {
        isNotSet = true;
        NoInteractionState = CreateInstance<NoInteractionState>();
        InteractionEnterState = CreateInstance<InteractionEnterState>();
        InteractionStayState = CreateInstance<InteractionStayState>();
        InteractionExitState = CreateInstance<InteractionExitState>();
        nullState = CreateInstance<InteractionNullState>();
    }
}
