using UnityEngine;


[CreateAssetMenu(fileName = "DialogStateFactory", menuName = "Factories/Dialog State Factory")]
public class DialogStateFactory : ScriptableObject, IGameProcessStateFactory {
    public DialogEnterState DialogEnterState;
    public DialogStayState DialogStayState;

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