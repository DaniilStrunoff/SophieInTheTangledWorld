using UnityEngine;
using UnityEngine.InputSystem;


public abstract class BaseRigidbodyCharacterController : BaseCharacterController {
    // Magic formula for top speed
    protected float moveAcceleration => MoveSpeed / Mathf.Clamp01(1 / baseRigidbody.Drag - Time.fixedDeltaTime);

    protected BaseRigidbody baseRigidbody;

    protected Vector3 moveVector;
    public override float Forward => Vector3.Project(baseRigidbody.ThisRigidbody.linearVelocity, moveVector).magnitude > 1e-5 ? 1 : 0;

    public override float PhysicVelocity => baseRigidbody.ThisRigidbody.linearVelocity.magnitude;

    private bool isRunning = false;
    public override bool IsRunning { get { return isRunning; } protected set { isRunning = value; }}

    public PlayerInputController PlayerInputController;

    void Awake() {
        PlayerInputController.PlayerInputActions.FindAction("Move").performed += HandleMove;
        PlayerInputController.PlayerInputActions.FindAction("Move").started += HandleMove;
        PlayerInputController.PlayerInputActions.FindAction("Move").canceled += HandleMove;
        PlayerInputController.PlayerInputActions.FindAction("Run").started += HandleRun;
        PlayerInputController.PlayerInputActions.FindAction("Run").canceled += HandleRun;
    }

    public virtual void HandleMove(InputAction.CallbackContext context) {
        Vector2 tmpVector2 = context.ReadValue<Vector2>().normalized;
        moveVector = moveAcceleration * new Vector3(tmpVector2.x, 0f, tmpVector2.y);
    }

    public virtual void HandleRun(InputAction.CallbackContext context) {
        if (context.started) isRunning = true;
        if (context.canceled) isRunning = false;
    }

    void OnDestroy() {
        PlayerInputController.PlayerInputActions.FindAction("Move").performed -= HandleMove;
        PlayerInputController.PlayerInputActions.FindAction("Move").started -= HandleMove;
        PlayerInputController.PlayerInputActions.FindAction("Move").canceled -= HandleMove;
        PlayerInputController.PlayerInputActions.FindAction("Run").started -= HandleRun;
        PlayerInputController.PlayerInputActions.FindAction("Run").canceled -= HandleRun;
    }
}
