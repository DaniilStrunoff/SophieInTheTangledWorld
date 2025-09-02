using System;
using UnityEngine;


public class LookAtObject : MonoBehaviour {
    public float FadeTime = 1;

    [Range(0, 1)]
    public float MaxHeadWeight = 0.5f;

    public bool IsActive = true;

    public event Action<LookAtObject> SetNotActiveEvent;

    public event Action<LookAtObject> SetActiveEvent;

    public void SetNotActive() {
        if (!IsActive) return;
        IsActive = false;
        SetNotActiveEvent?.Invoke(this);
    }

    public void SetActive() {
        if (IsActive) return;
        IsActive = true;
        SetActiveEvent?.Invoke(this);
    }
}
