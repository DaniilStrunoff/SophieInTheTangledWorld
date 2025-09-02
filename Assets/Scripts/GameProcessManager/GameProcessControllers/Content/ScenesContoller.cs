using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class ScenesContoller : MonoBehaviour, IController {
    public SceneField MainMunuScene;
    public delegate void SceneLoadHandler(LoadSceneState nextSceneField);
    public event SceneLoadHandler ExitSceneEvent;

    private AsyncOperation asyncOperation;

    public bool IsSceneLoaded => asyncOperation.progress >= 0.89;

    public void InvokeLoadSceneEvent(LoadSceneState state) {
        ExitSceneEvent.Invoke(state);
    }

    public void InvokeLoadCurrentPlayerSceneEvent() {
        LoadSceneState state = ScriptableObject.CreateInstance<LoadSceneState>();
        state.NextScene = SaveAndLoadController.GetInstance().SaveData.CurrentPlayerScene;
        ExitSceneEvent.Invoke(state);
    }

    public void SaveCurrentPlayerScene(SceneField scene) {
        if (MainMunuScene == null) return;
        if (scene.SceneName == MainMunuScene.SceneName) return;
        SaveAndLoadController.GetInstance().SaveData.CurrentPlayerScene = scene;
        SaveAndLoadController.GetInstance().Save();
    }

    public void ActivateScene() {
        asyncOperation.allowSceneActivation = true;
    }

    [HideInCallstack]
    public void LoadScene(SceneField scene) {
        StartCoroutine(LoadSceneAsync(scene));
    }

    IEnumerator LoadSceneAsync(SceneField scene) {
        asyncOperation = SceneManager.LoadSceneAsync(scene);
        asyncOperation.allowSceneActivation = false;
        while (asyncOperation.progress < 0.9f) {
            yield return null;
        }
    }
}
