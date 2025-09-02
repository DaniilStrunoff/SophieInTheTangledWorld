using UnityEngine;


[CreateAssetMenu(fileName = "LoadSceneStateFactory", menuName = "Factories/Load Scene State Factory")]
public class LoadSceneStateFactory : ScriptableObject, IGameProcessStateFactory {
    public LoadSceneEnterState LoadSceneEnterState;
    public LoadSceneStayState LoadSceneStayState;

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
        LoadSceneEnterState = CreateInstance<LoadSceneEnterState>();
        LoadSceneStayState = CreateInstance<LoadSceneStayState>();
        nullState = CreateInstance<GameProcessNullState>();
    }
}