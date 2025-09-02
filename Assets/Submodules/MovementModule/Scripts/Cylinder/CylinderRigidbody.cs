using UnityEngine;
using Vector3 = UnityEngine.Vector3;
using Quaternion = UnityEngine.Quaternion;
using System;


public class CylinderRigidbody : BaseRigidbody {

    public Transform Center;

    public Vector3 Direction;

    [HideInInspector]
    public bool isGrounded;

    [HideInInspector]
    public bool useGravity = true;

    private Quaternion initialRotation;

    void Start() {
        ThisRigidbody.mass = Mass;
        ThisRigidbody.useGravity = false;
        ThisRigidbody.linearDamping = Drag;
        ThisRigidbody.constraints = RigidbodyConstraints.FreezeRotation;
    }

    void FixedUpdate() {
        if (useGravity) ThisRigidbody.AddForce(GetGravityForce(), ForceMode.Acceleration);
        Quaternion rotation;
        foreach (Transform transform_ in ThisRigidbody.transform) {
            rotation = Quaternion.FromToRotation(transform_.up, -GetGravityForce());
            transform_.rotation = rotation * transform_.rotation;
        }
    }

    public void AddForce(Vector3 force, ForceMode forceMode = ForceMode.Force) {
        if (force.magnitude < 1e-3) return;
        Quaternion rotation = Quaternion.FromToRotation(Vector3.up, -GetGravityForce());
        Vector3 force_ = rotation * force;
        if (GetGravityForce().y > 0) force_ -= 2 * Vector3.Project(force_, Direction);
        ThisRigidbody.AddForce(force_, forceMode);
    }

    public Vector3 GetGravityForce() {
        Vector3 projVec = Vector3.Project(transform.position - Center.position, Direction);
        Vector3 gravityCenter = projVec + Center.position;
        Debug.DrawLine(transform.position, gravityCenter, Color.red);
        return GravityMagnitude * (gravityCenter - transform.position).normalized;
    }

    void OnCollisionStay(Collision collisionInfo) {   
        foreach (ContactPoint contact in collisionInfo.contacts) {
            if (Vector3.Angle(-GetGravityForce(), contact.normal) < 25) {
                isGrounded = true;
            }
        }
    }
}
