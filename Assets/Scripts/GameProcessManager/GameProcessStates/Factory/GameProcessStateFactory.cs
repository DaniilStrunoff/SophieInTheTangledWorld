using UnityEngine;

[CreateAssetMenu(fileName = "GameProcessManagerStateFactory", menuName = "Factories/Game Process State Factory")]
public class GameProcessManagerStateFactory: ScriptableObject, IGameProcessStateFactory {
    public GameplayState GameplayState;
    public PauseMenuState PauseMenuState;
    public DialogState DialogState;
    public SceneEnterState SceneEnterState;

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
