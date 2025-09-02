using UnityEngine;


[CreateAssetMenu(fileName = "PauseMenuDialogsStateFactory", menuName = "Factories/Pause Menu SubState Factories/Pause Menu Stay SubState Factories/Pause Menu Dialogs Factory")]
public class PauseMenuDialogsStateFactory : ScriptableObject, IGameProcessStateFactory {
    public PauseMenuDialogsEnterState PauseMenuDialogsEnterState;
    public PauseMenuDialogsStayState PauseMenuDialogsStayState;

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