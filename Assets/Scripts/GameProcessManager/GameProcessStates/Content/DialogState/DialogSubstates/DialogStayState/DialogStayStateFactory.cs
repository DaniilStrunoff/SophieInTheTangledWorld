using UnityEngine;


[CreateAssetMenu(fileName = "DialogStayStateFactory", menuName = "Factories/Dialog SubState Factories/Dialog Stay State Factory")]
public class DialogStayStateFactory : ScriptableObject, IGameProcessStateFactory {
    public DialogPlayState DialogPlayState;
    public DialogAutoPlayState DialogAutoPlayState;
    public DialogPauseState DialogPauseState;

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