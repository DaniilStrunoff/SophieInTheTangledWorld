using System;
using System.Collections.Generic;
using UnityEngine;


public class LookAtController : MonoBehaviour, ILookAtController {
    public event Action<LookAtObject> TriggerLookAtEnterEvent;
    public event Action<LookAtObject> TriggerLookAtExitEvent;

    private float fadeTime = 1;

    private float maxHeadWeight = 0.5f;

    [HideInInspector]
    public bool IsFadeComplited = false;

    [HideInInspector]
    public bool IsUnfadeComplited = false;

    [HideInInspector]
    public bool IsLookComplited = false;

    private AnimatorIKValuesSetter animatorIKValuesSetter;

    private LookAtObject objectToLookAt;
    private GameObject fakeObjectToLookAt;
    private Vector3 fakeObjectToLookAtInitialPosition;

    private float currentNormalizedFadeTime = 0f;

    private float currentNormalizedLookTime = 0f;

    void Start() {
        animatorIKValuesSetter = GetComponentInChildren<AnimatorIKValuesSetter>();
        fakeObjectToLookAt = new GameObject("FakeObjectToLookAt");
        animatorIKValuesSetter.ObjectToLookAt = fakeObjectToLookAt.transform;
    }

    public void FadeOut() {
        if (currentNormalizedFadeTime > 0) {
            animatorIKValuesSetter.HeadWeight = currentNormalizedFadeTime * maxHeadWeight;
            currentNormalizedFadeTime -= (Time.unscaledDeltaTime > 0.1 ? 0 : Time.unscaledDeltaTime) / fadeTime;
            IsFadeComplited = false;
            IsUnfadeComplited = false;
        } else {
            animatorIKValuesSetter.HeadWeight = 0;
            currentNormalizedFadeTime = 0f;
            IsUnfadeComplited = true;
        }
    }

    public void FadeIn() {
        if (currentNormalizedFadeTime < 1) {
            animatorIKValuesSetter.HeadWeight = currentNormalizedFadeTime * maxHeadWeight;
            currentNormalizedFadeTime += Time.unscaledDeltaTime / fadeTime;
            IsFadeComplited = false;
            IsUnfadeComplited = false;
        } else {
            animatorIKValuesSetter.HeadWeight = maxHeadWeight;
            currentNormalizedFadeTime = 1;
            IsFadeComplited = true;
        }
    }

    public void LookAt() {
        if (currentNormalizedLookTime < 1) {
            fakeObjectToLookAt.transform.position = Vector3.Lerp(
                fakeObjectToLookAtInitialPosition,
                objectToLookAt.transform.position,
                currentNormalizedLookTime
            );
            currentNormalizedLookTime += Time.unscaledDeltaTime / fadeTime;
            IsLookComplited = false;
        } else {
            fakeObjectToLookAt.transform.localPosition = Vector3.zero;
            currentNormalizedLookTime = 1;
            IsLookComplited = true;
        }
    }

    public void SetValues(LookAtObject lookAtObject) {
        objectToLookAt = lookAtObject;
        fakeObjectToLookAt.transform.SetParent(objectToLookAt.transform);
        fakeObjectToLookAt.transform.localPosition = Vector3.zero;
        fadeTime = lookAtObject.FadeTime;
        maxHeadWeight = lookAtObject.MaxHeadWeight;
    }

    public void SetNextObjectToLookAt(LookAtObject lookAtObject) {
        objectToLookAt = lookAtObject;
        fakeObjectToLookAtInitialPosition = fakeObjectToLookAt.transform.position;
        fakeObjectToLookAt.transform.SetParent(objectToLookAt.transform);
        currentNormalizedLookTime = 0;
        IsLookComplited = false;
        fadeTime = lookAtObject.FadeTime;
        maxHeadWeight = lookAtObject.MaxHeadWeight;
    }

    public void OnTriggerEnter(Collider collider) {
        var objectToLookAt_ = collider.GetComponentInParent<LookAtObject>();
        InvokeTriggerLookAtEnterEvent(objectToLookAt_);
    }

    public void OnTriggerExit(Collider collider) {
        var objectToLookAt_ = collider.GetComponentInParent<LookAtObject>();
        InvokeTriggerLookAtExitEvent(objectToLookAt_);
    }

    public void InvokeTriggerLookAtEnterEvent(LookAtObject objectToLookAt_) {
        if (objectToLookAt_ != null) {
            TriggerLookAtEnterEvent?.Invoke(objectToLookAt_);
        }
    }

    public void InvokeTriggerLookAtExitEvent(LookAtObject objectToLookAt_) {
        if (objectToLookAt_ != null && ReferenceEquals(objectToLookAt_, objectToLookAt)) {
            TriggerLookAtExitEvent?.Invoke(objectToLookAt_);
        }
    }
}
