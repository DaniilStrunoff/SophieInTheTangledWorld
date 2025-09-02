using UnityEngine;

public class BaseStateManager : MonoBehaviour {
    [Header("Character Controller")]
    [SerializeField]
    public BaseCharacterController characterController;

    [Header("Animator")]
    [SerializeField]
    public BaseStateAnimator animator;

    BaseState _currentState;
    public BaseIdleState idleState = new();
    public BaseMoveState moveState = new();
    public BaseRunState runState = new();

    void Start() {
        _currentState = idleState;
        if (animator == null) animator = gameObject.AddComponent<NullBaseStateAnimator>(); 
    }

    void FixedUpdate() {
        _currentState.UpdateState(this);
        _currentState.Act(this);
    }

    void LateUpdate() {
        _currentState.LateAct(this);
    }

    public void SwitchState(BaseState state) {
        _currentState?.OnStateExit(this);
        _currentState = state;
        _currentState.OnStateEnter(this);
    }

    public BaseState GetCurrentState() {
        return _currentState;
    }
}
