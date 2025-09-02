using System;
using UnityEngine;


[RequireComponent(typeof(Animator))]
public class AnimationEventsProvider : MonoBehaviour {
    public Action LeftFootStep;
    public Action RightFootStep;

    private enum LastFoot {
        None,
        Left,
        Right,
    }

    private LastFoot lastFoot = LastFoot.None;

    private LastFoot lastFootRemembered = LastFoot.None;
    private float timer = 0;

    private void Update() {
        if (lastFoot == lastFootRemembered) {
            timer += Time.deltaTime;
        }
        lastFootRemembered = lastFoot;
        if (timer > 0.75) {
            timer = 0;
            lastFoot = LastFoot.None;
        }
    }

    public void InvokeLeftFootStep(string clipName) {
        if (lastFoot == LastFoot.None || lastFoot == LastFoot.Right) {
            LeftFootStep?.Invoke();
            lastFoot = LastFoot.Left;
        }
    }

    public void InvokeRightFootStep(string clipName) {
        if (lastFoot == LastFoot.None || lastFoot == LastFoot.Left) {
            RightFootStep?.Invoke();
            lastFoot = LastFoot.Right;
        }
    }
}
