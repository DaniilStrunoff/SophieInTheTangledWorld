using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;


[TrackColor(1, 1,1)]
[TrackClipType(typeof(CanvasGroupControllerClip))]
[TrackBindingType(typeof(CanvasGroup))]
public class CanvasGroupController : TrackAsset {
    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount) {
        return ScriptPlayable<CanvasGroupControllerMixer>.Create(graph, inputCount);
    }
}
