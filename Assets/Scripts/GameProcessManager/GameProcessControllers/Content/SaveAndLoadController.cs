using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class SaveAndLoadController : MonoBehaviour {
    public SceneField FirstScene;

    private static SaveAndLoadController instance;

    public static SaveAndLoadController GetInstance() {
        return instance;
    }

    private SaveData saveData;

    public SaveData SaveData {get {return saveData;} set {saveData = value;}}

    private string persistentDataPath;
    private List<string> fileNameOldOnes = new() { "SaveData.json" };
    private string fileName = "SaveDataV1.json";

    private void Awake() {
        saveData = new(
            Language: ScriptableObject.CreateInstance<DefaultLanguage>().GetLanguage(),
            CurrentPlayerScene: new(FirstScene),
            Brightness: 0f,
            VerticalSynchronizationType: "VSync every frame",
            CharactersPerSecond: 50,
            WaitXSecondsBeforNextClipInAutoMode: 1f,
            MaxMasterVolume: 7f,
            MaxMusicVolume: 2f,
            MaxSoundsVolume: 6f,
            InputDevice: "Gamepad",
            AutoDialog: false,
            RunButtonHint: new(false, false)
        );
        instance = this;
        persistentDataPath = Application.persistentDataPath;
        ProcessOldJson();
        try {
            Debug.Log(SaveFileName());
            string jsonString = File.ReadAllText(SaveFileName());
            JsonUtility.FromJsonOverwrite(jsonString, saveData);
        }
        catch (FileNotFoundException e) {
            Debug.LogWarning(e);
            Save();
        }
    }

    private void ProcessOldJson() {
        foreach (string fileName_ in fileNameOldOnes) {
            string path = Path.Join(persistentDataPath, fileName_);
            if (File.Exists(path)) {
                string jsonString = File.ReadAllText(path);
                JsonUtility.FromJsonOverwrite(jsonString, saveData);
                saveData.CurrentPlayerScene = new(FirstScene);
            }
        }
    }

    private string SaveFileName() {
        return Path.Join(persistentDataPath, fileName);
    }

    public void Save() {
        File.WriteAllText(SaveFileName(), JsonUtility.ToJson(saveData, prettyPrint: true));
    }
}


[System.Serializable]
public class RunButtonHint {
    public bool KeyboardRunButtonShown;
    public bool GamepadRunButtonShown;

    public RunButtonHint(
        bool KeyboardRunButtonShown,
        bool GamepadRunButtonShown) {
        this.KeyboardRunButtonShown = KeyboardRunButtonShown;
        this.GamepadRunButtonShown = GamepadRunButtonShown;
    }

    public bool this[InputDevice key] {
        get {
            return key switch {
                Keyboard or Mouse => KeyboardRunButtonShown,
                Gamepad => GamepadRunButtonShown,
                _ => false,
            };
        }
        set {
            switch (key) {
                case Keyboard or Mouse:
                    KeyboardRunButtonShown = value;
                break;
                case Gamepad:
                    GamepadRunButtonShown = value;
                break;
            }
        }
    }
}


[System.Serializable]
public class SaveData {
    [SerializeField]
    public Language Language;

    [SerializeField]
    public SceneField CurrentPlayerScene;

    [SerializeField]
    public float Brightness;

    [SerializeField]
    public string VerticalSynchronizationType;

    [SerializeField]
    public int CharactersPerSecond;

    [SerializeField]
    public float WaitXSecondsBeforNextClipInAutoMode;

    [SerializeField]
    public float MaxMasterVolume;

    [SerializeField]
    public float MaxMusicVolume;

    [SerializeField]
    public float MaxSoundsVolume;

    [SerializeField]
    public string InputDevice;

    [SerializeField]
    public bool AutoDialog;

    [SerializeField]
    public RunButtonHint RunButtonHint;

    public SaveData(
        Language Language,
        SceneField CurrentPlayerScene,
        float Brightness,
        string VerticalSynchronizationType,
        int CharactersPerSecond,
        float WaitXSecondsBeforNextClipInAutoMode,
        float MaxMasterVolume,
        float MaxMusicVolume,
        float MaxSoundsVolume,
        string InputDevice,
        bool AutoDialog,
        RunButtonHint RunButtonHint
    ) {
        this.Language = Language;
        this.CurrentPlayerScene = CurrentPlayerScene;
        this.Brightness = Brightness;
        this.VerticalSynchronizationType = VerticalSynchronizationType;
        this.CharactersPerSecond = CharactersPerSecond;
        this.WaitXSecondsBeforNextClipInAutoMode = WaitXSecondsBeforNextClipInAutoMode;
        this.MaxMasterVolume = MaxMasterVolume;
        this.MaxMusicVolume = MaxMusicVolume;
        this.MaxSoundsVolume = MaxSoundsVolume;
        this.InputDevice = InputDevice;
        this.AutoDialog = AutoDialog;
        this.RunButtonHint = RunButtonHint;
    }
}
