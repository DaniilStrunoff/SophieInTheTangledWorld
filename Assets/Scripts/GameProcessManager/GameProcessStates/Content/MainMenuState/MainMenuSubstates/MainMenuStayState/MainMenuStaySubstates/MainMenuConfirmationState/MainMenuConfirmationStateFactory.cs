using UnityEngine;


[CreateAssetMenu(fileName = "MainMenuConfirmationStateFactory", menuName = "Factories/Main Menu SubState Factories/Main Menu Stay SubState Factories/Main Menu Confirmation Factory")]
public class MainMenuConfirmationStateFactory : ScriptableObject, IGameProcessStateFactory {
    public MainMenuConfirmationEnterState MainMenuConfirmationEnterState;
    public MainMenuConfirmationStayState MainMenuConfirmationStayState;

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
