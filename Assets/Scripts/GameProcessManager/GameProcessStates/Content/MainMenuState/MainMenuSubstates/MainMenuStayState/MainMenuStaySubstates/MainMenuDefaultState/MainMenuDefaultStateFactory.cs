using UnityEngine;


[CreateAssetMenu(fileName = "MainMenuDefaultStateFactory", menuName = "Factories/Main Menu SubState Factories/Main Menu Stay SubState Factories/Main Menu Default Factory")]
public class MainMenuDefaultStateFactory : ScriptableObject, IGameProcessStateFactory {
    public MainMenuDefaultEnterState MainMenuDefaultEnterState;
    public MainMenuDefaultStayState MainMenuDefaultStayState;

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
