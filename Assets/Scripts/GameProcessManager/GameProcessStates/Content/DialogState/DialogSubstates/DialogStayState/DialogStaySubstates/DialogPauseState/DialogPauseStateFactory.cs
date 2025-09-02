using UnityEngine;


[CreateAssetMenu(fileName = "DialogPauseStateFactory", menuName = "Factories/Dialog SubState Factories/Dialog Stay SubState Factories/Dialog Pause Factory")]
public class DialogPauseStateFactory : ScriptableObject, IGameProcessStateFactory {
    public DialogPauseEnterState DialogPauseEnterState;
    public DialogPauseStayState DialogPauseStayState;

    [SerializeField]
    private GameProcessNullState nullState;

    private GameProcessBaseState currentState;
    private bool isNotSet;
    public GameProcessBaseState CurrentState {
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
    }
}