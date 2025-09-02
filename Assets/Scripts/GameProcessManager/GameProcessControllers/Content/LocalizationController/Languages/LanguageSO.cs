using UnityEngine;


public class LanguageSO : ScriptableObject {
    [SerializeField]
    public string LanguageName;

    public Language GetLanguage() {
        return new Language(LanguageName);
    }
}
