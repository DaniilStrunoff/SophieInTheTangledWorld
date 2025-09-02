using UnityEngine;


[CreateAssetMenu(fileName = "English", menuName = "Languages/English")]
public class English : LanguageSO {
    public void OnEnable() {
        LanguageName = "English";
    }
}
