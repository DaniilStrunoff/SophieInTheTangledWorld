using TMPro;
using UnityEngine;


public class BaseDialogController : MonoBehaviour {
    public virtual TextMeshProUGUI NameText {get; set;}
    public virtual TextMeshProUGUI SpeachText {get; set;}
    public virtual int CharactersPerSecond {get; set;}
    public virtual int UnfadingCharacterNum {get; set;}
    public virtual float PauseXSecondsBeforEndOfClip {get; set;}
    public virtual ITextBackgroundFadeController TextBackgroundFadeController {get;}

    public virtual void SetSpeed(double speed) {}
    public virtual void EvaluateTimeline(float time) {}
    public virtual void InvokePauseEvent() {}
    public virtual void SetNormalSpeedWithoutNotofy() {}
}