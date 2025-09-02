using UnityEngine;


[CreateAssetMenu(fileName = "DialogAutoPlayStateFactory", menuName = "Factories/Dialog SubState Factories/Dialog Stay SubState Factories/Dialog AutoPlay Factory")]
public class DialogAutoPlayStateFactory : ScriptableObject, IGameProcessStateFactory {
    public DialogAutoPlayEnterState DialogAutoPlayEnterState;
    public DialogAutoPlayStayState DialogAutoPlayStayState;

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