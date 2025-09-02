using UnityEngine.InputSystem;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using System;


public class FlatCharacterController : BaseRigidbodyCharacterController {
	[SerializeField]
	private FlatRigidbody flatRigidbody {
		get {
			return (FlatRigidbody)baseRigidbody;
		}
		set {
			baseRigidbody = value;
		}
	}

	void Start() {
		flatRigidbody = GetComponent<FlatRigidbody>();
	}

	public Vector3 velocity => moveVector;

    public bool Grounded => flatRigidbody.isGrounded;

    public override void Idle() {
		flatRigidbody.ThisRigidbody.angularVelocity = Vector3.zero;
		flatRigidbody.ThisRigidbody.linearVelocity = Vector3.zero;
		flatRigidbody.ThisRigidbody.mass = 1000;
        Vector3 velocity = moveVector;
        if (flatRigidbody.GetGravityForce().y > 0) velocity = -velocity;
		flatRigidbody.AddForce(velocity, forceMode: ForceMode.Acceleration);
		foreach (Transform childTransform in transform) {
			float degree = Vector3.SignedAngle(childTransform.forward, Vector3.ProjectOnPlane(velocity, childTransform.up), childTransform.up);
			childTransform.rotation = Quaternion.AngleAxis(Math.Sign(degree) * Mathf.Min(Mathf.Abs(degree), 20), childTransform.up) * childTransform.rotation;
		}
	}

	public override void Move() {
		flatRigidbody.ThisRigidbody.mass = flatRigidbody.Mass;
		Vector3 velocity = moveVector;
        if (flatRigidbody.GetGravityForce().y > 0) velocity = -velocity;
		flatRigidbody.AddForce(velocity, forceMode: ForceMode.Acceleration);
		foreach (Transform childTransform in transform) {
			float degree = Vector3.SignedAngle(childTransform.forward, Vector3.ProjectOnPlane(velocity, childTransform.up), childTransform.up);
			childTransform.rotation = Quaternion.AngleAxis(Math.Sign(degree) * Mathf.Min(Mathf.Abs(degree), 20), childTransform.up) * childTransform.rotation;
		}
	}

    public override void Run() {
        throw new NotImplementedException();
    }
}
