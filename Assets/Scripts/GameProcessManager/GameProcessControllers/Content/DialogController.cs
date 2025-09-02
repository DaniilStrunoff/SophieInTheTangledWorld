using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Playables;


public class DialogController : BaseDialogController, IController {    
    public event Action DialogEvent;
    public event Action<GameProcessBaseState> EndOfDialogEvent;

    public event Action DialogSetSpeedEvent;
    public event Action DialogPlayEvent;
    public event Action DialogAutoPlayEvent;
    public event Action DialogPauseEvent;

    public TextMeshProUGUI nameText;
    public override TextMeshProUGUI NameText {get {return nameText;} set {nameText = value;}} 

    public TextMeshProUGUI speachText;
    public override TextMeshProUGUI SpeachText {get {return speachText;} set {speachText = value;}}

    public TextBackgroundFadeController textBackgroundFadeController;

    public override ITextBackgroundFadeController TextBackgroundFadeController => textBackgroundFadeController;

    public PlayableDirector timeline;

    public MultiGraphicButton AutoButton;

    private int charactersPerSecond;
    public override int CharactersPerSecond {get {return charactersPerSecond;} set {charactersPerSecond = value;}}
    private int unfadingCharacterNum = 15;
    public override int UnfadingCharacterNum {get {return unfadingCharacterNum;} set {unfadingCharacterNum = value;}}

    [Range(0.25f, 1)]
    public float pauseXSecondsBeforEndOfClip = 0.25f;
    public override float PauseXSecondsBeforEndOfClip {get {return pauseXSecondsBeforEndOfClip;} set {pauseXSecondsBeforEndOfClip = value;}}

    [HideInInspector]
    public float WaitXSecondsBeforNextClipInAutoMode;

    public UnityEvent<float> CharactersPerSecondChangedEvent;
    public UnityEvent<float> WaitXSecondsBeforNextClipInAutoModeChangedEvent;

    public bool IsInPouseState {get; set;}

    private double speed = 1.0;

    private void Awake() {
        CharactersPerSecond = SaveAndLoadController.GetInstance().SaveData.CharactersPerSecond;
        WaitXSecondsBeforNextClipInAutoMode = SaveAndLoadController.GetInstance().SaveData.WaitXSecondsBeforNextClipInAutoMode;
    }

    private void Start() {
        CharactersPerSecondChangedEvent.Invoke(CharactersPerSecond);
        WaitXSecondsBeforNextClipInAutoModeChangedEvent.Invoke(WaitXSecondsBeforNextClipInAutoMode);
    }

    public void SetCharactersPerSecond(float value) {
        SaveAndLoadController.GetInstance().SaveData.CharactersPerSecond = (int)value;
        SaveAndLoadController.GetInstance().Save();
        CharactersPerSecond = (int)value;
        CharactersPerSecondChangedEvent.Invoke(value);
    }

    public void SetWaitXSecondsBeforNextClipInAutoMode(float value) {
        SaveAndLoadController.GetInstance().SaveData.WaitXSecondsBeforNextClipInAutoMode = value;
        SaveAndLoadController.GetInstance().Save();
        WaitXSecondsBeforNextClipInAutoMode = (int)value;
        WaitXSecondsBeforNextClipInAutoModeChangedEvent.Invoke(value);
    }

    private IEnumerator Wait() {
        PauseTimeline();
        yield return new WaitForSeconds(WaitXSecondsBeforNextClipInAutoMode);
        PlayTimeline();
    }

    public void StartWaitCoroutine() {
        StartCoroutine(Wait());
    }

    public override void SetSpeed(double newSpeed) {
        speed = newSpeed;
        DialogSetSpeedEvent?.Invoke();
    }

    public void SetNormalSpeed() {
        SetSpeed(1);
    }

    public void SetSpeedWithoutNotofy(double newSpeed) {
        speed = newSpeed;
    }

    public override void SetNormalSpeedWithoutNotofy() {
        SetSpeedWithoutNotofy(1);
    }

    public void InvokePlayEvent(InputAction.CallbackContext context) {
        if (context.performed) InvokePlayEvent();
    }

    public void InvokePlayEvent() {
        DialogPlayEvent?.Invoke();
    }

    public void TrySwitchAutoPlayMode(InputAction.CallbackContext context) {
        if (context.performed) TrySwitchAutoPlayMode();
    }

    public void TrySwitchAutoPlayMode() {
        if (AutoButton.gameObject.activeInHierarchy) DialogAutoPlayEvent?.Invoke();
    }

    public void InvokePauseEvent(InputAction.CallbackContext context) {
        if (context.performed) InvokePauseEvent();
    }

    public override void InvokePauseEvent() {
        DialogPauseEvent?.Invoke();
    }

    public void InvokeDialogEvent(PlayableDirector playableDirector) {
        timeline = playableDirector;
        DialogEvent?.Invoke();
    }

    public void InvokeEndOfDialogEvent(GameProcessBaseState state) {
        EndOfDialogEvent?.Invoke(state);
        timeline.Stop();
    }

    public override void EvaluateTimeline(float time) {
        timeline.time = time;
        timeline.DeferredEvaluate();
    }

    public void PlayTimeline() {
        timeline.Play();
        timeline.playableGraph.GetRootPlayable(0).SetSpeed(speed);
    }

    public void PauseTimeline() {
        // timeline.Pause();
        timeline.playableGraph.GetRootPlayable(0).SetSpeed(0);
    }
}
