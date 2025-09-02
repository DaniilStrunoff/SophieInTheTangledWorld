using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class TextControllerClip : PlayableAsset, ITimelineClipAsset {
    public string NameTextKey;
    public string SpeachTextKey;

    [SerializeField]
    private TextControllerBehaviour timelineControllerBehaviour;

    [NonSerialized]
    public TimelineClip clipPassthrough;

    [NonSerialized]
    public TextController trackPassthrough;

    public ClipCaps clipCaps => ClipCaps.None;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner) {
        var playable = ScriptPlayable<TextControllerBehaviour>.Create(graph, timelineControllerBehaviour);
        TextControllerBehaviour timelineControllerBehaviour_ = playable.GetBehaviour();
        timelineControllerBehaviour_.Clip = this;
        timelineControllerBehaviour_.NameTextKey = NameTextKey;
        timelineControllerBehaviour_.SpeachTextKey = SpeachTextKey;
        return playable;
    }
}
