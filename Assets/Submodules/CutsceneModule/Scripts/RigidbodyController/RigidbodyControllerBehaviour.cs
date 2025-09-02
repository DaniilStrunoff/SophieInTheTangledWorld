using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Splines;

public class RigidbodyControllerBehaviour : PlayableBehaviour {

    public SplineContainer SplineContainer;
    private List<BezierKnot> initialKnots = new() {};

    public float FirstNodeRadius;

    private bool isFirstFrame = true;

    private Rigidbody rigidbody;

    private Vector3? pendingMovePosition;

    public override void OnBehaviourPlay(Playable playable, FrameData info) {
        FixedUpdateDispatcher.EnsureExists();
        FixedUpdateDispatcher.OnFixedUpdate += OnFixedUpdate;
    }

    public override void ProcessFrame(Playable playable, FrameData info, object playerData) {
        if (!Application.isPlaying) return;
        if (playable.GetDuration() - playable.GetTime() < 2 * info.deltaTime) {
            SplineContainer.Spline.Knots = initialKnots;
            return;
        }
        rigidbody = playerData as Rigidbody;
        if (isFirstFrame) {
            var array = SplineContainer.Spline.ToList();
            initialKnots = new(array);
            Vector3 dir = Vector3.zero;
            foreach (Transform childTransform in rigidbody.gameObject.transform) {
                dir = childTransform.transform.forward;
                break;
            }
            array.Insert(0, new BezierKnot(
                    rigidbody.transform.position,
                    - Vector3.forward * FirstNodeRadius,
                    Vector3.forward * FirstNodeRadius,
                    Quaternion.LookRotation(dir)
                )
            );
            SplineContainer.Spline.Knots = array;
            isFirstFrame = false;
        }
        SplineUtility.Evaluate(
            SplineContainer.Spline,
            (float)(playable.GetTime()/playable.GetDuration()),
            out Unity.Mathematics.float3 position,
            out Unity.Mathematics.float3 direction,
            out Unity.Mathematics.float3 up
        );
        pendingMovePosition = position;
        foreach (Transform childTransform in rigidbody.gameObject.transform) {
            float degree = Vector3.SignedAngle(childTransform.forward, Vector3.ProjectOnPlane(direction, childTransform.up), childTransform.up);
            childTransform.rotation = Quaternion.AngleAxis(degree, childTransform.up) * childTransform.rotation;
        }
    }

    public override void OnBehaviourPause(Playable playable, FrameData info) {
        FixedUpdateDispatcher.OnFixedUpdate -= OnFixedUpdate;
    }

    private void OnFixedUpdate() {
        if (!ReferenceEquals(rigidbody, null) && pendingMovePosition.HasValue) {
            if (Time.deltaTime > 0f) {
                rigidbody.MovePosition(pendingMovePosition.Value);
            }
        }
    }
}
