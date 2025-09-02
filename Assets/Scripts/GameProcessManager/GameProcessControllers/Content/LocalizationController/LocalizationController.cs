using System.Collections.Generic;
using Assets.SimpleLocalization.Scripts;
using AYellowpaper.SerializedCollections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;


public class LocalizationController : MonoBehaviour {
    public GameObject PauseMenuFade;

    [SerializedDictionary(keyName: "Language", valueName: "Font")]
    public SerializedDictionary<string, TMP_FontAsset> PauseMenuFonts;

    public UnityEvent<string> LanguageChangedEvent;

    private List<TextMeshProUGUI> PauseMenuTexts;

    private void OnValidate() {
        foreach (var language in PauseMenuFonts.Keys) {
            if (!PauseMenuFonts.ContainsKey(language)) {
                PauseMenuFonts.Add(language, null);
            }
        }
    }

    private void Awake() {
        LocalizationManager.Read();
        Language language = SaveAndLoadController.GetInstance().SaveData.Language;
        LocalizationManager.Language = language.LanguageName;
        PauseMenuTexts = new(PauseMenuFade.GetComponentsInChildren<TextMeshProUGUI>(includeInactive: true));
        foreach (var text in PauseMenuTexts) {
            text.font = PauseMenuFonts[language.LanguageName];
        }
    }

    private void Start() {
        LanguageChangedEvent.Invoke(LocalizationManager.Language);
    }

    public void SetLanguage(string language) {
        switch (language) {
            case "Русский":
            SetLanguage(ScriptableObject.CreateInstance<Russian>());
            break;
            case "English":
            SetLanguage(ScriptableObject.CreateInstance<English>());
            break;
        }
        LanguageChangedEvent.Invoke(language);
    }

    public void SetLanguage(LanguageSO languageSO) {
        SaveAndLoadController.GetInstance().SaveData.Language = languageSO.GetLanguage();
        SaveAndLoadController.GetInstance().Save();
        string languageName = languageSO.GetLanguage().LanguageName;
        LocalizationManager.Language = languageName;
        if (PauseMenuTexts == null) return;
        foreach (var text in PauseMenuTexts) {
            text.font = PauseMenuFonts[languageName];
        }
    }
}
