using UnityEngine;


[CreateAssetMenu(fileName = "PauseMenuScreenStateFactory", menuName = "Factories/Pause Menu SubState Factories/Pause Menu Stay SubState Factories/Pause Menu Screen Factory")]
public class PauseMenuScreenStateFactory : ScriptableObject, IGameProcessStateFactory {
    public PauseMenuScreenEnterState PauseMenuScreenEnterState;
    public PauseMenuScreenStayState PauseMenuScreenStayState;

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