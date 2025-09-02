using UnityEngine;
using Vector3 = UnityEngine.Vector3;
using Quaternion = UnityEngine.Quaternion;
using System;
using System.Numerics;
using System.Collections.Generic;


public class CornerRigidbody : BaseRigidbody {

    public List<Transform> Platforms;

    public float Size;

    [HideInInspector]
    public bool isGrounded;

    public Transform ChildTransform;

    [Range(1, 90)]
    public float GroundedAngle = 25;

    private Quaternion initialRotation;

    private Vector3 groundNormal;

    private const int contactBufferSize = 10;
    private ContactPoint[] contactBuffer = new ContactPoint[contactBufferSize];

    private Vector3 _cachedGravity;
    private int _cachedFrame = -1;

    void Start() {
        if (ChildTransform == null) {
            foreach (Transform transform in transform) {
                ChildTransform = transform;
                break;
            }
        }
        ThisRigidbody.mass = Mass;
        ThisRigidbody.useGravity = false;
        ThisRigidbody.linearDamping = Drag;
        ThisRigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        groundMask = LayerMask.GetMask("Ground");
        chainMask = LayerMask.GetMask("Chain1", "Chain2", "Chain3", "Chain4");
    }

    private const float sphereRadius = 0.25f;
    private const float sphereOffset = 0.26f;
    private LayerMask groundMask;
    private LayerMask chainMask;

    void FixedUpdate() {
        isGrounded = false;
        if ((chainMask.value & (1 << gameObject.layer)) == 0) {
            Vector3 gravityDir = GetGravityForce().normalized;
            Vector3 SpherePosition = ChildTransform.position - sphereOffset * gravityDir;
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

    public Vector3 RotateVector(Vector3 vec) {
        Vector3 gravity = GetGravityForce();
        Quaternion rotation = Quaternion.FromToRotation(Vector3.up, gravity * Mathf.Sign(gravity.y));
        vec *= -Mathf.Sign(gravity.y);
        return rotation * vec;
    }

    public void AddForce(Vector3 force, ForceMode forceMode = ForceMode.Force) {
        if (force.magnitude < 1e-3) return;
        Vector3 force_ = RotateVector(force);
        ThisRigidbody.AddForce(force_, forceMode);
    }

    public Vector3 GetGravityForce() {
        if (Time.frameCount == _cachedFrame)
            return _cachedGravity;
        _cachedFrame = Time.frameCount;
        if (Platforms.Count == 0) {
            var newGAmeObject = new GameObject("FakePlatform");
            newGAmeObject.transform.position = new Vector3(0, -1, -100);
            Platforms.Add(newGAmeObject.transform);
        }
        Vector3 childPos = ChildTransform.position;
        float minDistanceSqr = float.PositiveInfinity;
        int nearestIndex = 0;
        for (int i = 0; i < Platforms.Count; i++) {
            Vector3 platformPos = Platforms[i].position;
            float sqrDist = (childPos - platformPos).sqrMagnitude;
            if (sqrDist < minDistanceSqr) {
                minDistanceSqr = sqrDist;
                nearestIndex = i;
            }
        }
        Vector3 nearestPos = Platforms[nearestIndex].position;
        Vector3 dirToPlatform = Vector3.ProjectOnPlane(childPos - nearestPos, Platforms[nearestIndex].up);
        float halfSize = Size * 0.5f;
        Vector3 gravityPoint = dirToPlatform.sqrMagnitude <= halfSize * halfSize
            ? nearestPos + dirToPlatform.normalized * halfSize
            : nearestPos + dirToPlatform;
        _cachedGravity = (gravityPoint - childPos).normalized * GravityMagnitude;
        return _cachedGravity;
    }
}
