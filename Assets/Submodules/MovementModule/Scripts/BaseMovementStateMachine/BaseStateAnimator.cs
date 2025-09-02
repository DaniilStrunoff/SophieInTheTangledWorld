using Unity.Profiling;
using UnityEngine;
using UnityEngine.Rendering;

public class BaseStateAnimator : MonoBehaviour {
    [Header("Animations")]
    [SerializeField]
    protected Animator _animator;

    public float FadeToIdleTime = 0.5f;
    public float FadeToMoveTime = 0.2f;
    public float FadeToRunTime = 0.2f;

    protected float idleTransitionTime = 0;

    protected float moveTransitionTime = 0;
    protected float currentMoveBlend = 0;

    protected float runTransitionTime = 0;
    protected float currentRunBlend = 0;

    public Animator Animator { get { return _animator; } }

    public virtual bool IsCurrentAnimationFinished() {
        return _animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1;
    }

    public virtual void UpdateMoveAnimationSpeed(float speed) {
        _animator.SetFloat("MoveSpeed", speed);
    }

    public virtual void StartIdleAnimation() {
        idleTransitionTime = 0;
        currentMoveBlend = _animator.GetFloat("MoveBlend");
        currentRunBlend = _animator.GetFloat("RunBlend");
    }

    public virtual void StartMoveAnimation() {
        moveTransitionTime = 0;
        currentMoveBlend = _animator.GetFloat("MoveBlend");
        currentRunBlend = _animator.GetFloat("RunBlend");
    }

    public virtual void StartRunAnimation() {
        runTransitionTime = 0;
        currentMoveBlend = _animator.GetFloat("MoveBlend");
        currentRunBlend = _animator.GetFloat("RunBlend");
    }

    public virtual void LateUpdateIdleAnimation() {
        idleTransitionTime += Time.deltaTime;
        _animator.SetFloat("MoveBlend", Mathf.Clamp01(currentMoveBlend - idleTransitionTime / FadeToIdleTime));
        _animator.SetFloat("RunBlend", Mathf.Clamp01(currentRunBlend - idleTransitionTime / FadeToIdleTime));
    }

    public virtual void LateUpdateMoveAnimation() {
        moveTransitionTime += Time.deltaTime;
        _animator.SetFloat("MoveBlend", Mathf.Clamp01(currentMoveBlend + moveTransitionTime / FadeToMoveTime));
        _animator.SetFloat("RunBlend", Mathf.Clamp01(currentRunBlend - moveTransitionTime / FadeToMoveTime));
    }

    public virtual void LateUpdateRunAnimation() {
        runTransitionTime += Time.deltaTime;
        _animator.SetFloat("RunBlend", Mathf.Clamp01(currentRunBlend + runTransitionTime / FadeToRunTime ));
        _animator.SetFloat("MoveBlend", Mathf.Clamp01(currentMoveBlend - runTransitionTime / FadeToRunTime));
    }
}

public class NullBaseStateAnimator : BaseStateAnimator {
    public override bool IsCurrentAnimationFinished() {
        return true;
    }

    public override void UpdateMoveAnimationSpeed(float speed) { }

    public override void StartIdleAnimation() { }

    public override void StartMoveAnimation() { }

    public override void LateUpdateIdleAnimation() { }

    public override void LateUpdateMoveAnimation() { }

    public override void LateUpdateRunAnimation() { }
}
