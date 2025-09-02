using UnityEngine;
using UnityEngine.Playables;


public class CanvasGroupControllerBehaviour : PlayableBehaviour {
    private CanvasGroup canvasGroup;
    
    public float Alpha;

    public override void ProcessFrame(Playable playable, FrameData info, object playerData) {
        canvasGroup = playerData as CanvasGroup;
        canvasGroup.alpha = Alpha;
    }
}
