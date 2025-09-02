using UnityEngine;


[CreateAssetMenu(fileName = "PauseMenuDefaultStateFactory", menuName = "Factories/Pause Menu SubState Factories/Pause Menu Stay SubState Factories/Pause Menu Default Factory")]
public class PauseMenuDefaultStateFactory : ScriptableObject, IGameProcessStateFactory {
    public PauseMenuDefaultEnterState PauseMenuDefaultEnterState;
    public PauseMenuDefaultStayState PauseMenuDefaultStayState;

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