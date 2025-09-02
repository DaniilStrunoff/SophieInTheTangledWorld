using UnityEngine;

[CreateAssetMenu(fileName = "LookAtManagerStateFactory", menuName = "Factories/Look At Manager State Factory")]
public class LookAtManagerStateFactory: ScriptableObject, ILookAtManagerStateFactory {
    public NoLookAtState NoLookAtState;
    public LookAtChangeState LookAtChangeState;
    public LookAtEnterState LookAtEnterState;
    public LookAtStayState LookAtStayState;
    public LookAtExitState LookAtExitState;

    [SerializeField]
    private LookAtNullState nullState;
    private LookAtBaseState currentState;
    private bool isNotSet;
    public LookAtBaseState CurrentState {
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
        NoLookAtState = CreateInstance<NoLookAtState>();
        LookAtChangeState = CreateInstance<LookAtChangeState>();
        LookAtEnterState = CreateInstance<LookAtEnterState>();
        LookAtStayState = CreateInstance<LookAtStayState>();
        LookAtExitState = CreateInstance<LookAtExitState>();
        nullState = CreateInstance<LookAtNullState>();
    }
}
