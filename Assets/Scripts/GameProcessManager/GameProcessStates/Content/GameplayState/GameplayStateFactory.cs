using UnityEngine;


[CreateAssetMenu(fileName = "GameplayStateFactory", menuName = "Factories/Gameplay State Factory")]
public class GameplayStateFactory : ScriptableObject, IGameProcessStateFactory {
    public GameplayEnterState GameplayEnterState;
    public GameplayStayState GameplayStayState;

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
