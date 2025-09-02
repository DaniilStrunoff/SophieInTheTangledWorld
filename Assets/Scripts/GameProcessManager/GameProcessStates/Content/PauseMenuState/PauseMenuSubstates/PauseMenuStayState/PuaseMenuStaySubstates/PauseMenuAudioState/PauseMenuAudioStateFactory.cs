using UnityEngine;


[CreateAssetMenu(fileName = "PauseMenuAudioStateFactory", menuName = "Factories/Pause Menu SubState Factories/Pause Menu Stay SubState Factories/Pause Menu Audio Factory")]
public class PauseMenuAudioStateFactory : ScriptableObject, IGameProcessStateFactory {
    public PauseMenuAudioEnterState PauseMenuAudioEnterState;
    public PauseMenuAudioStayState PauseMenuAudioStayState;

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