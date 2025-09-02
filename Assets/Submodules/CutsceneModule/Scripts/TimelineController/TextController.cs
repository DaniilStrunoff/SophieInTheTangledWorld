using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;


[Serializable]
[TrackColor(1, 1,1)]
[TrackClipType(typeof(TextControllerClip))]
[TrackBindingType(typeof(BaseDialogController))]
public class TextController : TrackAsset {
    [NonSerialized]
    public TextControllerClip ActiveClip = null;

    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount) {
        var clips = GetClips();
        foreach(var clip in clips) {
            var loopClip = clip.asset as TextControllerClip;
            loopClip.clipPassthrough = clip;
            loopClip.trackPassthrough = this;
        }
        var playable = ScriptPlayable<TextControllerMixer>.Create(graph, inputCount);
        TextControllerMixer textControllerMixer = playable.GetBehaviour();
        textControllerMixer.textController = this;
        ActiveClip = null;
        return playable;
    }
}
