using UnityEngine;
using UnityEngine.InputSystem;


public abstract class BaseCharacterController : MonoBehaviour {
    public float MoveSpeed = 1;

    public abstract float Forward { get; }

    public abstract float PhysicVelocity { get; }

    public abstract bool IsRunning { get; protected set; }

    public abstract void Idle();

    public abstract void Move();

    public abstract void Run();
}
