using UnityEngine;


[CreateAssetMenu(fileName = "PauseMenuStateFactory", menuName = "Factories/Pause Menu State Factory")]
public class PauseMenuStateFactory : ScriptableObject, IGameProcessStateFactory {
    public PauseMenuEnterState PauseMenuEnterState;
    public PauseMenuStayState PauseMenuStayState;

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