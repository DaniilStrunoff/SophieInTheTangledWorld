using UnityEngine;


[CreateAssetMenu(fileName = "MainMenuStayStateFactory", menuName = "Factories/Main Menu SubState Factories/Main Menu Stay State Factory")]
public class MainMenuStayStateFactory : ScriptableObject, IGameProcessStateFactory {
    public MainMenuConfirmationState MainMenuConfirmationState;
    public MainMenuDefaultState MainMenuDefaultState;

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
