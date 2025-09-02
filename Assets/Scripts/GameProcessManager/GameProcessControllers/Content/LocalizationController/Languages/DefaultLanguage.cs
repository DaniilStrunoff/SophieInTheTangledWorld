using UnityEngine;


[System.Serializable]
public class DefaultLanguage : LanguageSO {
    public void OnEnable() {
        LanguageName = Application.systemLanguage switch {
            SystemLanguage.English => "English",
            SystemLanguage.Russian => "Русский",
            _ => "English"
        };
    }
}
