using System.Collections.Generic;
using Assets.SimpleLocalization.Scripts;
using AYellowpaper.SerializedCollections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;


public class VerticalSynchronizationController : MonoBehaviour {
    public UnityEvent<string> VerticalSynchronizationTypeChangedEvent;

    private void Awake() {
        Application.targetFrameRate = -1;
        switch (SaveAndLoadController.GetInstance().SaveData.VerticalSynchronizationType) {
            case "No VSync":
            QualitySettings.vSyncCount = 0;
            break;
            case "VSync every frame":
            QualitySettings.vSyncCount = 1;
            break;
            case "VSync every two frames":
            QualitySettings.vSyncCount = 2;
            break;
            default:
            return;
        }
    }

    private void Start() {
        VerticalSynchronizationTypeChangedEvent.Invoke(SaveAndLoadController.GetInstance().SaveData.VerticalSynchronizationType);
    }

    public void SetVerticalSynchronizationType(string newVerticalSynchronizationType) {
        Application.targetFrameRate = -1;
        switch (newVerticalSynchronizationType) {
            case "No VSync":
            QualitySettings.vSyncCount = 0;
            break;
            case "VSync every frame":
            QualitySettings.vSyncCount = 1;
            break;
            case "VSync every two frames":
            QualitySettings.vSyncCount = 2;
            break;
            default:
            return;
        }
        SaveAndLoadController.GetInstance().SaveData.VerticalSynchronizationType = newVerticalSynchronizationType;
        SaveAndLoadController.GetInstance().Save();
        VerticalSynchronizationTypeChangedEvent.Invoke(newVerticalSynchronizationType);
        Debug.Log("vSyncCount now = " + QualitySettings.vSyncCount);
    }
}
