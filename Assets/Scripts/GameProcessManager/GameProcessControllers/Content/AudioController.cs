
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;


public class AudioController : MonoBehaviour, IController {

    [RequireComponent(typeof(AudioSource))]
    public class AudioPlayer : MonoBehaviour {
        public AudioMixerGroup MusicMixerGroup {
            get {
                return musicSource.outputAudioMixerGroup;
            }
            set {
                musicSource.outputAudioMixerGroup = value;
            }
        }

        public float MusicFadeTime;
        public AnimationCurve EaseInCurve;
        private float maxMusicVolume;
        private float maxSoundsVolume;
        private const string MIXER_MUSIC = "MusicVolume";

        public float MaxMusicVolume {
             private get {
                return maxMusicVolume;
            }
            set {
                maxMusicVolume = value;
            }
        }
    
        public float MaxSoundsVolume {
             private get {
                return maxSoundsVolume;
            }
            set {
                maxSoundsVolume = value;
            }
        }

        private float currentMusicVolume = 1;
        public float CurrentMusicVolume {
            internal get {
                return  currentMusicVolume;
            }
            set {
                if (!Mathf.Approximately(currentMusicVolume, value)) OnCurrentMusicVolumeChange?.Invoke(value);
                currentMusicVolume = value;
                MusicMixerGroup.audioMixer.SetFloat(MIXER_MUSIC, Mathf.Log10(value * MaxMusicVolume + 1e-10f) * 20);
            }
        }

        public Action<float> OnCurrentMusicVolumeChange;

        private static AudioPlayer instance;
        private AudioSource musicSource;

        public static AudioPlayer GetInstance() {
            return instance;
        }

        void Awake() {
            if (instance != null) {
                Destroy(gameObject);
                return;
            }
            instance = this;
            DontDestroyOnLoad(gameObject);
            musicSource = GetComponent<AudioSource>();
            musicSource.loop = true;
        }

        public void ChangeMusic(AudioClip newAudioClip) { 
        }

        public void StopMusic() {
            StartCoroutine(FadeMusicOut());
        }

        public void PlayMusic(AudioClip newAudioClip) {
            if (newAudioClip == null) return;
            if (CurrentMusicVolume > 0)
                if (musicSource.resource != null) 
                    if (musicSource.resource == newAudioClip) return;
            musicSource.resource = newAudioClip;
            StartCoroutine(FadeMusicIn());
        }

        private IEnumerator FadeMusicIn() {
            CurrentMusicVolume = 0;
            float time = 0;
            musicSource.Play();
            while (CurrentMusicVolume < 1) {
                time += (Time.unscaledDeltaTime > 0.1 ? 0 : Time.unscaledDeltaTime) / MusicFadeTime;
                CurrentMusicVolume = EaseInCurve.Evaluate(time);
                yield return new WaitForEndOfFrame();
            }
        }

        private IEnumerator FadeMusicOut() {
            float time = CurrentMusicVolume;
            while (CurrentMusicVolume > 0) {
                time -= (Time.unscaledDeltaTime > 0.1 ? 0 : Time.unscaledDeltaTime) / MusicFadeTime;
                CurrentMusicVolume = EaseInCurve.Evaluate(time);
                yield return new WaitForEndOfFrame();
            }
            musicSource.Stop();
        }
    }

    public AudioClip AudioClipOnSceneLoad;
    public AudioMixerGroup MusicMixerGroup;
    public AudioMixerGroup SoundsMixerGroup;
    public float MusicFadeTime = 5;

    public AnimationCurve EaseInCurve;
    [Range(0, 1)]
    public float MaxMasterVolume;
    [Range(0, 1)]
    public float MaxMusicVolume;
    [Range(0, 1)]
    public float CurrentMusicVolume;
    [Range(0, 1)]
    public float MaxSoundsVolume;

    public UnityEvent<float> MaxMasterVolumeChangedEvent;
    public UnityEvent<float> MaxMusicVolumeChangedEvent;
    public UnityEvent<float> MaxSoundsVolumeChangedEvent;

    private const string MIXER_MASTER = "MasterVolume";
    private const string MIXER_SOUNDS = "SoundsVolume";
    private const int MASTER_SCALE = 10;
    private const int MUSIC_SCALE = 10;
    private const int SOUNDS_SCALE = 10;

    void Start() {
        MaxMasterVolume = SaveAndLoadController.GetInstance().SaveData.MaxMasterVolume / MASTER_SCALE;
        MaxMusicVolume = SaveAndLoadController.GetInstance().SaveData.MaxMusicVolume / MUSIC_SCALE;
        MaxSoundsVolume = SaveAndLoadController.GetInstance().SaveData.MaxSoundsVolume / SOUNDS_SCALE;
        GameObject audioPlayerGameObject = new("AudioPlayer");
        audioPlayerGameObject.AddComponent<AudioPlayer>();
        AudioPlayer.GetInstance().EaseInCurve = EaseInCurve;
        AudioPlayer.GetInstance().MusicFadeTime = MusicFadeTime;
        AudioPlayer.GetInstance().MaxMusicVolume = MaxMusicVolume;
        AudioPlayer.GetInstance().MusicMixerGroup = MusicMixerGroup;
        AudioPlayer.GetInstance().OnCurrentMusicVolumeChange += UpdateCurrentMusicVolume;
        UpdateCurrentMusicVolume(AudioPlayer.GetInstance().CurrentMusicVolume);
        MusicMixerGroup.audioMixer.SetFloat(MIXER_MASTER, Mathf.Log10(MaxMasterVolume + 1e-10f) * 20);
        MusicMixerGroup.audioMixer.SetFloat(MIXER_SOUNDS, Mathf.Log10(MaxSoundsVolume + 1e-10f) * 20);
        MaxMasterVolumeChangedEvent.Invoke(MaxMasterVolume * MASTER_SCALE);
        MaxMusicVolumeChangedEvent.Invoke(MaxMusicVolume * MUSIC_SCALE);
        MaxSoundsVolumeChangedEvent.Invoke(MaxSoundsVolume * SOUNDS_SCALE);
    }

    private void UpdateCurrentMusicVolume(float newCurrentVolume) {
        CurrentMusicVolume = newCurrentVolume;
    }

    public void SetMaxMasterVolumeAndSave(float newMaxVolume) {
        SaveAndLoadController.GetInstance().SaveData.MaxMasterVolume = newMaxVolume;
        SaveAndLoadController.GetInstance().Save();
        MaxMasterVolume = newMaxVolume / MASTER_SCALE;
        MusicMixerGroup.audioMixer.SetFloat(MIXER_MASTER, Mathf.Log10(MaxMasterVolume + 1e-10f) * 20);
        MaxMasterVolumeChangedEvent.Invoke(newMaxVolume);
    }

    public void SetMaxMusicVolumeAndSave(float newMaxVolume) {
        SaveAndLoadController.GetInstance().SaveData.MaxMusicVolume = newMaxVolume;
        SaveAndLoadController.GetInstance().Save();
        MaxMusicVolume = newMaxVolume / MUSIC_SCALE;
        MaxMusicVolumeChangedEvent.Invoke(newMaxVolume);
    }

    public void SetMaxSoundsVolumeAndSave(float newMaxVolume) {
        SaveAndLoadController.GetInstance().SaveData.MaxSoundsVolume = newMaxVolume;
        SaveAndLoadController.GetInstance().Save();
        MaxSoundsVolume = newMaxVolume / SOUNDS_SCALE;
        MusicMixerGroup.audioMixer.SetFloat(MIXER_SOUNDS, Mathf.Log10(MaxSoundsVolume + 1e-10f) * 20);
        MaxSoundsVolumeChangedEvent.Invoke(newMaxVolume);
    }

    void Update() {
        AudioPlayer.GetInstance().CurrentMusicVolume = CurrentMusicVolume;
        AudioPlayer.GetInstance().MaxMusicVolume = MaxMusicVolume;
    }

    public void SetMusicFadeTime(float t) {
        MusicFadeTime = t;
    }

    public void ChangeMusic(AudioClip newAudioClip) {
        AudioPlayer.GetInstance().MusicFadeTime = MusicFadeTime;
        AudioPlayer.GetInstance().ChangeMusic(newAudioClip);
    }

    public void StopMusic() {
        AudioPlayer.GetInstance().MusicFadeTime = MusicFadeTime;
        AudioPlayer.GetInstance().StopMusic();
    }

    public void PlayMusic(AudioClip newAudioClip) {
        AudioPlayer.GetInstance().MusicFadeTime = MusicFadeTime;
        AudioPlayer.GetInstance().PlayMusic(newAudioClip);
    }

    void OnDestriy() {
        AudioPlayer.GetInstance().OnCurrentMusicVolumeChange -= UpdateCurrentMusicVolume;
    }
}
