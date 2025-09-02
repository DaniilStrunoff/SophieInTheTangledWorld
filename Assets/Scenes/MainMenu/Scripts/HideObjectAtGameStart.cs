using UnityEngine;

public class HideObjectAtGameStart : MonoBehaviour {
    void Awake() {
        if (SaveAndLoadController.GetInstance().SaveData.CurrentPlayerScene.SceneName == SaveAndLoadController.GetInstance().FirstScene.SceneName) {
            gameObject.SetActive(false);
        } else {
            gameObject.SetActive(true);
        }
    }
}
