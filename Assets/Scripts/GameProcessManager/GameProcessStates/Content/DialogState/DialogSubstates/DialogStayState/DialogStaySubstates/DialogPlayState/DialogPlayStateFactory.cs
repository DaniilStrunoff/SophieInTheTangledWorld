using UnityEngine;


[CreateAssetMenu(fileName = "DialogSkipPauseStateFactory", menuName = "Factories/Dialog SubState Factories/Dialog Stay SubState Factories/Dialog Play Factory")]
public class DialogPlayStateFactory : ScriptableObject, IGameProcessStateFactory {
    public DialogPlayEnterState DialogPlayEnterState;
    public DialogPlayStayState DialogPlayStayState;

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