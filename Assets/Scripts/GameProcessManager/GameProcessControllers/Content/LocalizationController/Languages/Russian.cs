using UnityEngine;


[CreateAssetMenu(fileName = "Russian", menuName = "Languages/Russian")]
public class Russian : LanguageSO {
    public void OnEnable() {
        LanguageName = "Русский";
    }
}
