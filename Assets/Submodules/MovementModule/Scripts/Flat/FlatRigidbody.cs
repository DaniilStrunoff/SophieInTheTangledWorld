using UnityEngine;
using Vector3 = UnityEngine.Vector3;
using Quaternion = UnityEngine.Quaternion;
using System;


public class FlatRigidbody : BaseRigidbody {

    public Transform Platform;

    [HideInInspector]
    public bool isGrounded;

    [Range(1, 90)]
    public float GroundedAngle = 25;

    private Vector3 groundNormal;

    private const float sphereRadius = 0.25f;
    private const float sphereOffset = 0.26f;
    private LayerMask groundMask;
    private LayerMask chainMask;

    void Start() {
        ThisRigidbody.mass = Mass;
        ThisRigidbody.useGravity = false;
        ThisRigidbody.linearDamping = Drag;
        ThisRigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        if (Platform == null) {
            var newGAmeObject = new GameObject("FakePlatform");
            newGAmeObject.transform.position = new Vector3(0, -1, -100);
            Platform = newGAmeObject.transform;
        }
        groundMask = LayerMask.GetMask("Ground");
        chainMask = LayerMask.GetMask("Chain1", "Chain2", "Chain3", "Chain4");
    }

    void FixedUpdate() {
        isGrounded = false;
        if ((chainMask.value & (1 << gameObject.layer)) == 0) {
            Vector3 gravityDir = GetGravityForce().normalized;
            Vector3 SpherePosition = ThisRigidbodyTransform.position - sphereOffset * gravityDir;
            groundNormal = -gravityDir;
            if (Physics.SphereCast(SpherePosition, sphereRadius, gravityDir, out RaycastHit hit, 0.1f, groundMask)) {
                float angle = Vector3.Angle(gravityDir * -1f, hit.normal);
                if (angle < GroundedAngle) {
                    isGrounded = true;
                    groundNormal = hit.normal;
                }
            }
        }
        if (isGrounded) {
            ThisRigidbody.AddForce(-groundNormal * GravityMagnitude, ForceMode.Acceleration);
#if UNITY_EDITOR
            Debug.DrawRay(transform.position, groundNormal);
#endif
        } else {
            ThisRigidbody.AddForce(GetGravityForce(), ForceMode.Acceleration);
        }
        if ((chainMask.value & (1 << gameObject.layer)) == 0) {
            Quaternion rotation;
            for (int i = 0; i < ThisRigidbodyTransform.childCount; i++) {
                Transform child = ThisRigidbodyTransform.GetChild(i);
                rotation = Quaternion.FromToRotation(child.up, -GetGravityForce());
                child.rotation = rotation * child.rotation;
            }
        }
    }

    public void AddForce(Vector3 force, ForceMode forceMode = ForceMode.Force) {
        if (force.magnitude < 1e-3) return;
        ThisRigidbody.AddForce(force, forceMode);
    }

    public Vector3 GetGravityForce() {
        return GravityMagnitude * Math.Sign((Platform.position - transform.position).y) * new Vector3(0, 1, 0);
    }
}
