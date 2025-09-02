using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;


[Serializable]
public class CanvasGroupControllerClip : PlayableAsset, ITimelineClipAsset {
    [SerializeField]
    private CanvasGroupControllerBehaviour canvasGroupControllerBehaviour;

    public ClipCaps clipCaps => ClipCaps.Blending;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner) {
        var playable = ScriptPlayable<CanvasGroupControllerBehaviour>.Create(graph, canvasGroupControllerBehaviour);
        return playable;
    }
}
