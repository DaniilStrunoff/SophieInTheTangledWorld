using UnityEngine;
using UnityEngine.Playables;


public class CanvasGroupControllerMixer : PlayableBehaviour {
    private CanvasGroup canvasGroup;

    public override void ProcessFrame(Playable playable, FrameData info, object playerData) {
        canvasGroup = playerData as CanvasGroup;

        if (canvasGroup == null) return;

        int inputCount = playable.GetInputCount();
        canvasGroup.alpha = 0;
        canvasGroup.gameObject.SetActive(true);
        for (int i = 0; i < inputCount; i++) {
            float inputWeight = playable.GetInputWeight(i);
            canvasGroup.alpha = Mathf.Max(canvasGroup.alpha, inputWeight);
        }
        if (canvasGroup.alpha < 0.0001) {
            canvasGroup.gameObject.SetActive(false);
        }
    }
}
