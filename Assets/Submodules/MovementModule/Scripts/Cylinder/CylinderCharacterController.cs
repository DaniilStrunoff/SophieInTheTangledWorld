using UnityEngine.InputSystem;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using System;


public class CylinderCharacterController : BaseRigidbodyCharacterController {
	[SerializeField]
	private CylinderRigidbody cylinderRigidbody;

	void Start() {
		cylinderRigidbody = GetComponent<CylinderRigidbody>();
	}

	public Vector3 velocity => moveVector;

    public bool Grounded => cylinderRigidbody.isGrounded;

    public override void Idle() {
		cylinderRigidbody.ThisRigidbody.angularVelocity = Vector3.zero;
		cylinderRigidbody.ThisRigidbody.linearVelocity = Vector3.zero;
		cylinderRigidbody.ThisRigidbody.mass = 1000;
        Vector3 velocity = moveVector;
		cylinderRigidbody.AddForce(velocity, forceMode: ForceMode.Acceleration);
        Quaternion rotation = Quaternion.FromToRotation(Vector3.up, -cylinderRigidbody.GetGravityForce());
		Vector3 velocity_ = rotation * velocity;
		if (cylinderRigidbody.GetGravityForce().y > 0) velocity_ -= 2 * Vector3.Project(velocity_, cylinderRigidbody.Direction);
		foreach (Transform childTransform in transform) {
			float degree = Vector3.SignedAngle(childTransform.forward, Vector3.ProjectOnPlane(velocity_, childTransform.up), childTransform.up);
			childTransform.rotation = Quaternion.AngleAxis(Math.Sign(degree) * Mathf.Min(Mathf.Abs(degree), 20), childTransform.up) * childTransform.rotation;
		}
	}

	public override void Move() {
		cylinderRigidbody.ThisRigidbody.mass = cylinderRigidbody.Mass;
		Vector3 velocity = moveVector;
		cylinderRigidbody.AddForce(velocity, forceMode: ForceMode.Acceleration);
        Quaternion rotation = Quaternion.FromToRotation(Vector3.up, -cylinderRigidbody.GetGravityForce());
		Vector3 velocity_ = rotation * velocity;
		if (cylinderRigidbody.GetGravityForce().y > 0) velocity_ -= 2 * Vector3.Project(velocity_, cylinderRigidbody.Direction);
		foreach (Transform childTransform in transform) {
			float degree = Vector3.SignedAngle(childTransform.forward, Vector3.ProjectOnPlane(velocity_, childTransform.up), childTransform.up);
			childTransform.rotation = Quaternion.AngleAxis(Math.Sign(degree) * Mathf.Min(Mathf.Abs(degree), 20), childTransform.up) * childTransform.rotation;
		}
	}

    public override void Run()
    {
        throw new NotImplementedException();
    }
}
