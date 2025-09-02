using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[TrackColor(1, 1,1)]
[TrackClipType(typeof(RigidbodyControllerClip))]
[TrackBindingType(typeof(Rigidbody))]
public class RigidbodyController : TrackAsset {
    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount) {
        return ScriptPlayable<RigidbodyControllerMixer>.Create(graph, inputCount);
    }
}
