using Assets.SimpleLocalization.Scripts;
using TMPro;
using UnityEngine;


[RequireComponent(typeof(TextMeshProUGUI))]
public class ShowDifferentTextAtGameStart : MonoBehaviour {
    public string LocalizationKeyAtGameStart;
    public string LocalizationKeyAtGameContinue;

    void Start() {
        Localize();
        LocalizationManager.OnLocalizationChanged += Localize;
    }

    public void OnDestroy()
    {
        LocalizationManager.OnLocalizationChanged -= Localize;
    }

    private void Localize() {
        TextMeshProUGUI text = GetComponent<TextMeshProUGUI>();
        if (SaveAndLoadController.GetInstance().SaveData.CurrentPlayerScene.SceneName == SaveAndLoadController.GetInstance().FirstScene.SceneName) {
            text.text = LocalizationManager.Localize(LocalizationKeyAtGameStart);
        } else {
            text.text = LocalizationManager.Localize(LocalizationKeyAtGameContinue);
        }
    }
}
