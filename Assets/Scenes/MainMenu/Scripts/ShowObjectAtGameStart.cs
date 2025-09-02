using UnityEngine;

public class ShowObjectAtGameStart : MonoBehaviour {
    void Awake() {
        if (SaveAndLoadController.GetInstance().SaveData.CurrentPlayerScene.SceneName == SaveAndLoadController.GetInstance().FirstScene.SceneName) {
            gameObject.SetActive(true);
        } else {
            gameObject.SetActive(false);
        }
    }
}
