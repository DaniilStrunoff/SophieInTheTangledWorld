using UnityEngine;


[CreateAssetMenu(fileName = "PauseMenuStayStateFactory", menuName = "Factories/Pause Menu SubState Factories/Pause Menu Stay State Factory")]
public class PauseMenuStayStateFactory : ScriptableObject, IGameProcessStateFactory {
    public PauseMenuDefaultState PauseMenuDefaultState;
    public PauseMenuLocalizationState PauseMenuLocalizationState;
    public PauseMenuScreenState PauseMenuScreenState;
    public PauseMenuDialogsState PauseMenuDialogState;
    public PauseMenuAudioState PauseMenuAudioState;

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
