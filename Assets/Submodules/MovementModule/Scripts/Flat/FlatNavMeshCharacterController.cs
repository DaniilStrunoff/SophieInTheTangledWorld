using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(FlatStateManager), typeof(FlatRigidbody))]
public class FlatNavMeshCharacterController : BaseRigidbodyCharacterController {
    public float RunDistance = 4;
    public float MaxDistance = 2;
    public float MinDistance = 1.5f;

    public Transform target;
    [Range(0, 1)]
    public float rotationSmoothness = 0.1f;

	[SerializeField]
	private FlatRigidbody flatRigidbody {
		get {
			return (FlatRigidbody)baseRigidbody;
		}
		set {
			baseRigidbody = value;
		}
	}

    private AnimatorIKValuesSetter animatorIKValuesSetter;

    private NavMeshPath path;

	void Awake() {
		flatRigidbody = GetComponent<FlatRigidbody>();
        animatorIKValuesSetter = GetComponentInChildren<AnimatorIKValuesSetter>();
        path = new NavMeshPath();
	}

	public Vector3 velocity => moveVector;

    public bool Grounded => flatRigidbody.isGrounded;

    private float forward = 0;

    public override float Forward => forward;

    public void SetTarget(Transform NewTarget) {
        target = NewTarget;
    }

    public void SetMaxDistance(float NewMaxDistance) {
        MaxDistance = NewMaxDistance;
    }

    public void SetMinDistance(float NewMinDistance) {
        MinDistance = NewMinDistance;
    }

    public void SetRunDistance(float NewRunDistance) {
        RunDistance = NewRunDistance;
    }

    public override void Idle() {
        Vector3 targetPosition;
        float distance = 0;
        if (NavMesh.SamplePosition(Vector3.ProjectOnPlane(target.position, Vector3.up), out NavMeshHit hit, float.PositiveInfinity, NavMesh.AllAreas)) {
            targetPosition = hit.position;
            Debug.DrawRay(targetPosition, Vector3.up, Color.red);
            if (NavMesh.SamplePosition(Vector3.ProjectOnPlane(transform.position, Vector3.up), out NavMeshHit agentHit, float.PositiveInfinity, NavMesh.AllAreas)) {
                if (NavMesh.CalculatePath(Vector3.ProjectOnPlane(agentHit.position, Vector3.up), targetPosition, NavMesh.AllAreas, path)) {
                    for (int i = 0; i < path.corners.Length - 1; i++) {
                        Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red);
                        distance += Vector3.Distance(path.corners[i], path.corners[i + 1]);
                    }
                }
            }
        }
        flatRigidbody.AddForce(animatorIKValuesSetter.DeltaPosition * moveAcceleration / Time.fixedDeltaTime / MoveSpeed, forceMode: ForceMode.Acceleration);
        forward = distance > MaxDistance ? 1 : 0;
        IsRunning = distance > RunDistance;
    }

    public override void Move() {
        Vector3 targetPosition;
        float distance = 0;
        if (NavMesh.SamplePosition(Vector3.ProjectOnPlane(target.position, Vector3.up), out NavMeshHit hit, float.PositiveInfinity, NavMesh.AllAreas)) {
            targetPosition = hit.position;
            Debug.DrawRay(targetPosition, Vector3.up, Color.red);
            if (NavMesh.SamplePosition(Vector3.ProjectOnPlane(transform.position, Vector3.up), out NavMeshHit agentHit, float.PositiveInfinity, NavMesh.AllAreas)) {
                if (NavMesh.CalculatePath(Vector3.ProjectOnPlane(agentHit.position, Vector3.up), targetPosition, NavMesh.AllAreas, path)) {
                    for (int i = 0; i < path.corners.Length - 1; i++) {
                        moveVector = Vector3.ProjectOnPlane(path.corners[1] - path.corners[0], Vector3.up);
                        Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red);
                        distance += Vector3.Distance(path.corners[i], path.corners[i + 1]);
                    }
                }
            }
        }
        flatRigidbody.AddForce(animatorIKValuesSetter.DeltaPosition * moveAcceleration / Time.fixedDeltaTime / MoveSpeed, forceMode: ForceMode.Acceleration);
        foreach (Transform transform_ in transform) {
            transform_.rotation = Quaternion.Slerp(Quaternion.LookRotation(moveVector), transform_.rotation, rotationSmoothness);
        }
        forward = distance > MinDistance ? 1 : 0;
        IsRunning = distance > RunDistance;
	}

    public override void Run() {
        Vector3 targetPosition;
        float distance = 0;
        if (NavMesh.SamplePosition(Vector3.ProjectOnPlane(target.position, Vector3.up), out NavMeshHit hit, float.PositiveInfinity, NavMesh.AllAreas)) {
            targetPosition = hit.position;
            Debug.DrawRay(targetPosition, Vector3.up, Color.red);
            if (NavMesh.SamplePosition(Vector3.ProjectOnPlane(transform.position, Vector3.up), out NavMeshHit agentHit, float.PositiveInfinity, NavMesh.AllAreas)) {
                if (NavMesh.CalculatePath(Vector3.ProjectOnPlane(agentHit.position, Vector3.up), targetPosition, NavMesh.AllAreas, path)) {
                    for (int i = 0; i < path.corners.Length - 1; i++) {
                        moveVector = Vector3.ProjectOnPlane(path.corners[1] - path.corners[0], Vector3.up);
                        Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red);
                        distance += Vector3.Distance(path.corners[i], path.corners[i + 1]);
                    }
                }
            }
        }
        flatRigidbody.AddForce(animatorIKValuesSetter.DeltaPosition * moveAcceleration / Time.fixedDeltaTime / MoveSpeed, forceMode: ForceMode.Acceleration);
        foreach (Transform transform_ in transform) {
            transform_.rotation = Quaternion.Slerp(Quaternion.LookRotation(moveVector), transform_.rotation, rotationSmoothness);
        }
        forward = distance > MinDistance ? 1 : 0;
        IsRunning = distance > (MaxDistance + RunDistance) / 2;
    }
}
