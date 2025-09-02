using UnityEngine;


[CreateAssetMenu(fileName = "MainMenuStateFactory", menuName = "Factories/Main Menu State Factory")]
public class MainMenuStateFactory : ScriptableObject, IGameProcessStateFactory {
    public MainMenuEnterState MainMenuEnterState;
    public MainMenuStayState MainMenuStayState;

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