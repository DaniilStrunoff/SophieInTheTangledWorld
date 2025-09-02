using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;


[RequireComponent(typeof(Volume))]
public class VolumeController : MonoBehaviour, IController {
    public Slider Slider;

    private Volume volume;

    private void Awake() {
        volume = GetComponent<Volume>();
        SetBrightness(SaveAndLoadController.GetInstance().SaveData.Brightness);
        Slider.value = SaveAndLoadController.GetInstance().SaveData.Brightness;
    }

    public void SetBrightnessAndSave(float value) {
        SetBrightness(value);
        SaveAndLoadController.GetInstance().SaveData.Brightness = value;
        SaveAndLoadController.GetInstance().Save();
    }

    public void SetBrightness(float brightness) {
        if (volume.profile.TryGet(out ColorAdjustments colorAdjustments)) {
            colorAdjustments.postExposure.value = brightness / Slider.maxValue;
        }
    }

    private void OnDestroy() {
        Slider.onValueChanged.RemoveAllListeners();
    }
}
