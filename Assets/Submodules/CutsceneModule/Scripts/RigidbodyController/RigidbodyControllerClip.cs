using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Splines;
using UnityEngine.Timeline;

[Serializable]
public class RigidbodyControllerClip : PlayableAsset, ITimelineClipAsset {

    public ExposedReference<SplineContainer> SplineContainer;

    public float FirstNodeRadius = 3;

    [SerializeField]
    private RigidbodyControllerBehaviour rigidbodyControllerBehaviour;

    public ClipCaps clipCaps => ClipCaps.None;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner) {
        var playable = ScriptPlayable<RigidbodyControllerBehaviour>.Create(graph, rigidbodyControllerBehaviour);
        RigidbodyControllerBehaviour rigidbodyControllerBehaviour_ = playable.GetBehaviour();
        rigidbodyControllerBehaviour_.SplineContainer = SplineContainer.Resolve(graph.GetResolver());
        rigidbodyControllerBehaviour_.FirstNodeRadius = FirstNodeRadius;
        return playable;
    }
}
