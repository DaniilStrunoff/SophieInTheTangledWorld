using UnityEngine.InputSystem;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using System;


[RequireComponent(typeof(CornerRigidbody))]
public class CornerCharacterController : BaseRigidbodyCharacterController {
	[SerializeField]
	private CornerRigidbody cornerRigidbody {
		get {
			return (CornerRigidbody)baseRigidbody;
		}
		set {
			baseRigidbody = value;
		}
	}

	void Start() {
		cornerRigidbody = GetComponent<CornerRigidbody>();
	}

	public Vector3 velocity => moveVector;

    public bool Grounded => cornerRigidbody.isGrounded;

    public override void Idle() {
		cornerRigidbody.ThisRigidbody.angularVelocity = Vector3.zero;
		cornerRigidbody.ThisRigidbody.linearVelocity = Vector3.zero;
		cornerRigidbody.ThisRigidbody.mass = 1000;
        Vector3 velocity = moveVector;
		cornerRigidbody.AddForce(velocity, forceMode: ForceMode.Acceleration);
		Vector3 velocity_ = cornerRigidbody.ThisRigidbody.GetAccumulatedForce();
		foreach (Transform childTransform in transform) {
			float degree = Vector3.SignedAngle(childTransform.forward, Vector3.ProjectOnPlane(velocity_, childTransform.up), childTransform.up);
			childTransform.rotation = Quaternion.AngleAxis(Math.Sign(degree) * Mathf.Min(Mathf.Abs(degree), 8), childTransform.up) * childTransform.rotation;
		}
	}

	public override void Move() {
		cornerRigidbody.ThisRigidbody.mass = cornerRigidbody.Mass;
		Vector3 velocity = moveVector;
		cornerRigidbody.AddForce(velocity, forceMode: ForceMode.Acceleration);
		Vector3 velocity_ = cornerRigidbody.ThisRigidbody.GetAccumulatedForce();
		foreach (Transform childTransform in transform) {
			float degree = Vector3.SignedAngle(childTransform.forward, Vector3.ProjectOnPlane(velocity_, childTransform.up), childTransform.up);
			childTransform.rotation = Quaternion.AngleAxis(Math.Sign(degree) * Mathf.Min(Mathf.Abs(degree), 8), childTransform.up) * childTransform.rotation;
		}
	}

    public override void Run() {
        cornerRigidbody.ThisRigidbody.mass = cornerRigidbody.Mass;
		Vector3 velocity = moveVector;
		cornerRigidbody.AddForce(velocity, forceMode: ForceMode.Acceleration);
		Vector3 velocity_ = cornerRigidbody.ThisRigidbody.GetAccumulatedForce();
		foreach (Transform childTransform in transform) {
			float degree = Vector3.SignedAngle(childTransform.forward, Vector3.ProjectOnPlane(velocity_, childTransform.up), childTransform.up);
			childTransform.rotation = Quaternion.AngleAxis(Math.Sign(degree) * Mathf.Min(Mathf.Abs(degree), 8), childTransform.up) * childTransform.rotation;
		}
    }
}
