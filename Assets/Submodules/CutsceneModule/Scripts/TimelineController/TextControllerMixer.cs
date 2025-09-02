using TMPro;
using UnityEngine;
using UnityEngine.Playables;


public class TextControllerMixer : PlayableBehaviour {
    public TextController textController;

    private BaseDialogController dialogController;

    private bool isFirstFrame = true;

    private float time = 0;

    public override void ProcessFrame(Playable playable, FrameData info, object playerData) {
        time = (float)playable.GetTime();
        if (isFirstFrame) {
            dialogController = playerData as BaseDialogController;
            dialogController.SpeachText.text = "";
            dialogController.NameText.text = "";
            isFirstFrame = false;
        }

        int inputCount = playable.GetInputCount();
        for (int i = 0; i < inputCount; i++) {
            var palyable = (ScriptPlayable<TextControllerBehaviour>)playable.GetInput(i);
            float startTime = (float)palyable.GetBehaviour().Clip.clipPassthrough.start;
            float endTime = (float)palyable.GetBehaviour().Clip.clipPassthrough.end;
            if ((time + dialogController.TextBackgroundFadeController.FadeTime > startTime) &&
                (time < endTime)) {
                dialogController.TextBackgroundFadeController.FadeIn(info.deltaTime);
                return;
            }
        }
        dialogController.TextBackgroundFadeController.FadeOut(info.deltaTime);

        // if (text == null) return;
        // int inputCount = playable.GetInputCount();
        // for (int i = 0; i < inputCount; i++) {
        //     float inputWeight = playable.GetInputWeight(i);
        //     if (inputWeight > 0) return;
        // }
        // text.text = "";
    }
}
