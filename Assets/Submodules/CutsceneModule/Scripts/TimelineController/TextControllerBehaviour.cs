using System;
using Assets.SimpleLocalization.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;


public class TextControllerBehaviour : PlayableBehaviour {
    public string SpeachTextKey;
    public string NameTextKey;
    private BaseDialogController dialogController;
    [NonSerialized]
    public TextControllerClip Clip;
    private bool isFirstFrame = true;
    private bool isLastFrameReached = false;

    private void Localize() {
        dialogController.NameText.text = LocalizationManager.Localize(NameTextKey);
        dialogController.SpeachText.text = LocalizationManager.Localize(SpeachTextKey);
        int textLength = dialogController.SpeachText.text.Length;
        if (dialogController.SpeachText.text.Length < dialogController.CharactersPerSecond) {
            for (int i = 0; i < dialogController.CharactersPerSecond - textLength; i++) 
                dialogController.SpeachText.text += " ";
        }
    }

    private void SetCharAlpha(TMP_TextInfo textInfo, int iCharId, int iAlpha){
        int iMaterialIndex = textInfo.characterInfo[iCharId].materialReferenceIndex;
        Color32[] rVertexColors = textInfo.meshInfo[iMaterialIndex].colors32;
        TMP_CharacterInfo charInfo = textInfo.characterInfo[iCharId];
        if (!charInfo.isVisible) return;
        int iVertexIndex = charInfo.vertexIndex;

        byte alpha = (byte)Mathf.Clamp(iAlpha, 0, 255);
        rVertexColors[iVertexIndex + 0].a = alpha;
        rVertexColors[iVertexIndex + 1].a = alpha;
        rVertexColors[iVertexIndex + 2].a = alpha;
        rVertexColors[iVertexIndex + 3].a = alpha;
    }

    private void ProcessSmoothNormalizedTime(Playable playable, FrameData info, object playerData) {
        if (isFirstFrame) {
            Localize();
            LocalizationManager.OnLocalizationChanged += Localize;
            double textLength = (double)dialogController.SpeachText.text.Length;
            double newDuration = textLength / dialogController.CharactersPerSecond;
            double speed = playable.GetDuration() / ( textLength / dialogController.CharactersPerSecond);
            dialogController.SetSpeed(speed);
            isFirstFrame = false;
        }
        dialogController.SpeachText.ForceMeshUpdate();
        TMP_TextInfo textInfo = dialogController.SpeachText.textInfo;
        float normalizedTime = Mathf.Clamp01((float)(playable.GetTime() / (playable.GetDuration() - dialogController.PauseXSecondsBeforEndOfClip)));
        int currentCharCount = (int)((textInfo.characterCount + dialogController.UnfadingCharacterNum) * normalizedTime);
        for (int i = 0; i < textInfo.characterCount; ++i) {
            if (i < currentCharCount - dialogController.UnfadingCharacterNum) {
                SetCharAlpha(textInfo, i, 255);
            } else if (currentCharCount - dialogController.UnfadingCharacterNum <= i && i <= currentCharCount) {
                SetCharAlpha(textInfo, i, (currentCharCount - i) * 255 / dialogController.UnfadingCharacterNum);
            } else {
                SetCharAlpha(textInfo, i, 0);
            }
        }
        dialogController.SpeachText.UpdateVertexData(TMP_VertexDataUpdateFlags.All);
    }

    public override void ProcessFrame(Playable playable, FrameData info, object playerData) {
        if (Clip.trackPassthrough.ActiveClip == null) {
            Clip.trackPassthrough.ActiveClip = Clip;
        }
        if (!Clip.trackPassthrough.ActiveClip.Equals(Clip)) {
            return;
        }
        if (isLastFrameReached) {
            Localize();
        }
        dialogController = playerData as BaseDialogController;
        ProcessSmoothNormalizedTime(playable, info, playerData);
    }

    public override void OnBehaviourPause(Playable playable, FrameData info) {
        var duration = playable.GetDuration();
        var count = playable.GetTime() + info.deltaTime;
        if ((info.effectivePlayState == PlayState.Paused && count > duration) || playable.GetGraph().GetRootPlayable(0).IsDone()) {
            if (isLastFrameReached) {
                LocalizationManager.OnLocalizationChanged -= Localize;
                Clip.trackPassthrough.ActiveClip = null;
                dialogController.SpeachText.text = "";
                dialogController.NameText.text = "";
            } else {
                dialogController.EvaluateTimeline((float)Clip.clipPassthrough.end - dialogController.PauseXSecondsBeforEndOfClip);
                dialogController.InvokePauseEvent();
                dialogController.SetNormalSpeedWithoutNotofy();
                isLastFrameReached = true;
            }
        }
    }
}
