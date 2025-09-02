using UnityEngine;


[CreateAssetMenu(fileName = "PauseMenuLocalizationStateFactory", menuName = "Factories/Pause Menu SubState Factories/Pause Menu Stay SubState Factories/Pause Menu Localization Factory")]
public class PauseMenuLocalizationStateFactory : ScriptableObject, IGameProcessStateFactory {
    public PauseMenuLocalizationEnterState PauseMenuLocalizationEnterState;
    public PauseMenuLocalizationStayState PauseMenuLocalizationStayState;

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