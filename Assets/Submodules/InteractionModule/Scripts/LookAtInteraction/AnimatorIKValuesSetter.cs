using UnityEngine;

public class AnimatorIKValuesSetter : MonoBehaviour {

    public Transform LeftFoot;
    public Transform RightFoot;

    [HideInInspector]
    public Transform ObjectToLookAt;

    private float headWeight;
    [HideInInspector]
    public float HeadWeight {
        set {
            headWeight = value; 
        }
        get {
            return headWeight;
        }
    }
    [HideInInspector]
    public float bodyWeight = 0;

    [HideInInspector]
    public Vector3 PivotFootPositionCurrent {get; private set;}
    [HideInInspector]
    public Vector3 LeftFootPositionCurrent {get; private set;}
    [HideInInspector]
    public Vector3 RightFootPositionCurrent {get; private set;}

    public Quaternion LeftFootRotationCurrent {get; private set;}
    [HideInInspector]
    public Quaternion RightFootRotationCurrent {get; private set;}

    [HideInInspector]
    public Vector3 LeftFootPosition {private get; set;}
    [HideInInspector]
    public Vector3 RightFootPosition {private get; set;}

    [HideInInspector]
    public float LeftFootPositionWeight {private get; set;}
    [HideInInspector]
    public float RightFootPositionWeight {private get; set;}

    public Quaternion LeftFootRotation {private get; set;}
    [HideInInspector]
    public Quaternion RightFootRotation {private get; set;}

    [HideInInspector]
    public float LeftFootRotationWeight {private get; set;}
    [HideInInspector]
    public float RightFootRotationWeight {private get; set;}

    public enum CurrentPivotFoot {
        Left,
        Right
    }
    [HideInInspector]
    public CurrentPivotFoot currentPivotFoot {get; private set;}

    [HideInInspector]
    public Vector3 DeltaPosition {get; private set;}
    public Quaternion DeltaRotation {get; private set;}

    [HideInInspector]
    public float LeftFootGrounded {get; private set;}
    [HideInInspector]
    public float RightFootGrounded {get; private set;}

    [HideInInspector]
    public float LeftFootWeight = 0;

    [HideInInspector]
    private Animator animator;

    void Awake() {
        animator = GetComponent<Animator>();
    }

    private void OnAnimatorMove() {
        DeltaPosition = animator.deltaPosition;
        DeltaRotation = animator.deltaRotation;
    }

    private void OnAnimatorIK(int layerIndex) {
        if (ObjectToLookAt != null) animator.SetLookAtPosition(ObjectToLookAt.position);
        animator.SetLookAtWeight(1, bodyWeight, headWeight);
        RightFootPositionCurrent =  RightFoot.position; // animator.GetIKPosition(AvatarIKGoal.RightFoot );
        LeftFootPositionCurrent = LeftFoot.position; // animator.GetIKPosition(AvatarIKGoal.LeftFoot);
        RightFootRotationCurrent = animator.GetIKRotation(AvatarIKGoal.RightFoot);
        LeftFootRotationCurrent = animator.GetIKRotation(AvatarIKGoal.LeftFoot);
        float rCoord = Mathf.Sign(Vector3.Dot(RightFootPositionCurrent - animator.bodyPosition, transform.forward)) * Vector3.Project(RightFootPositionCurrent - animator.bodyPosition, transform.forward).magnitude;
        float lCoord = Mathf.Sign(Vector3.Dot(LeftFootPositionCurrent - animator.bodyPosition, transform.forward)) * Vector3.Project(LeftFootPositionCurrent - animator.bodyPosition, transform.forward).magnitude;
        if (lCoord < rCoord) {
            currentPivotFoot = CurrentPivotFoot.Right;
        } else {
            currentPivotFoot = CurrentPivotFoot.Left;
        }
        LeftFootGrounded = animator.GetFloat("LeftFootGrounded");
        RightFootGrounded = animator.GetFloat("RightFootGrounded");
        // animator.SetIKPosition(AvatarIKGoal.RightFoot, RightFootPosition);
        // animator.SetIKPosition(AvatarIKGoal.LeftFoot, LeftFootPosition);
        // animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, RightFootPositionWeight);
        // animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, LeftFootPositionWeight);

        // animator.SetIKRotation(AvatarIKGoal.RightFoot, RightFootRotation);
        // animator.SetIKRotation(AvatarIKGoal.LeftFoot, LeftFootRotation);
        // animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, RightFootRotationWeight);
        // animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, LeftFootRotationWeight);    
#if UNITY_EDITOR
        // Debug.DrawRay(PivotFootPositionCurrent, Vector3.up, Color.yellow);
#endif
    }
}
