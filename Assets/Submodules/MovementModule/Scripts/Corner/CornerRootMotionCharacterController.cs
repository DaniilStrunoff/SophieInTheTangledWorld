using UnityEngine.InputSystem;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using System;
using Unity.Mathematics;


[RequireComponent(typeof(CornerRigidbody), typeof(CornerStateManager))]
public class CornerRootMotionCharacterController : BaseRigidbodyCharacterController
{
	[SerializeField]
	private CornerRigidbody cornerRigidbody {
		get {
			return (CornerRigidbody)baseRigidbody;
		}
		set {
			baseRigidbody = value;
		}
	}

	private AnimatorIKValuesSetter animatorIKValuesSetter;

	void Start() {
		cornerRigidbody = GetComponent<CornerRigidbody>();
		animatorIKValuesSetter = GetComponentInChildren<AnimatorIKValuesSetter>();
	}

	public Vector3 velocity => moveVector;

	public bool Grounded => cornerRigidbody.isGrounded;

	public override float Forward => moveVector.magnitude > 0.001 ? 1 : 0;

    public override void Idle() {
		cornerRigidbody.ThisRigidbody.angularVelocity = Vector3.zero;
		cornerRigidbody.ThisRigidbody.linearVelocity = Vector3.zero;
		cornerRigidbody.ThisRigidbody.mass = 1000;
		cornerRigidbody.ThisRigidbody.AddForce(animatorIKValuesSetter.DeltaPosition * moveAcceleration / Time.fixedDeltaTime / MoveSpeed, ForceMode.Acceleration);
		if (Forward < 0.1) return;
		Vector3 velocity_ = cornerRigidbody.RotateVector(velocity);
		for (int i = 0; i < transform.childCount; i++) {
			Transform childTransform = transform.GetChild(i);
			float degree = Vector3.SignedAngle(childTransform.forward, Vector3.ProjectOnPlane(velocity_, childTransform.up), childTransform.up);
			childTransform.rotation = Quaternion.AngleAxis(Math.Sign(degree) * Mathf.Min(Mathf.Abs(degree), 8), childTransform.up) * childTransform.rotation;
		}
	}

	public override void Move() {
		cornerRigidbody.ThisRigidbody.mass = cornerRigidbody.Mass;
		cornerRigidbody.ThisRigidbody.AddForce(animatorIKValuesSetter.DeltaPosition * moveAcceleration / Time.fixedDeltaTime / MoveSpeed, ForceMode.Acceleration);
		Vector3 velocity_ = cornerRigidbody.RotateVector(velocity);
		for (int i = 0; i < transform.childCount; i++) {
			Transform childTransform = transform.GetChild(i);
			float degree = Vector3.SignedAngle(childTransform.forward, Vector3.ProjectOnPlane(velocity_, childTransform.up), childTransform.up);
			childTransform.rotation = Quaternion.AngleAxis(Math.Sign(degree) * Mathf.Min(Mathf.Abs(degree), 8), childTransform.up) * childTransform.rotation;
		}
	}

    public override void Run() {
        cornerRigidbody.ThisRigidbody.mass = cornerRigidbody.Mass;
		cornerRigidbody.ThisRigidbody.AddForce(animatorIKValuesSetter.DeltaPosition * moveAcceleration / Time.fixedDeltaTime / MoveSpeed, ForceMode.Acceleration);
		Vector3 velocity_ = cornerRigidbody.RotateVector(velocity);
		for (int i = 0; i < transform.childCount; i++) {
			Transform childTransform = transform.GetChild(i);
			float degree = Vector3.SignedAngle(childTransform.forward, Vector3.ProjectOnPlane(velocity_, childTransform.up), childTransform.up);
			childTransform.rotation = Quaternion.AngleAxis(Math.Sign(degree) * Mathf.Min(Mathf.Abs(degree), 8), childTransform.up) * childTransform.rotation;
		}
    }
}
