using Assets.SimpleLocalization.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


[RequireComponent(typeof(TextMeshProUGUI))]
public class SliderTextController : MonoBehaviour {
    public UnityEvent<float> FunctionToInvokeOnSetValueEvent;
    public TextMeshProUGUI TextMesh;
    public Slider Slider;
    public string TextKey;
    public float Shift = 0;
    public float Scale = 1;

    public void Start() {
        SetValue();
        LocalizationManager.OnLocalizationChanged += SetValue;
    }

    public void OnDestroy() {
        LocalizationManager.OnLocalizationChanged -= SetValue;
    }

    private void SetValue() {
        TextMesh.text = string.Format(LocalizationManager.Localize(TextKey), Scale * Slider.value + Shift );
    }

    public void SetValueWithoutNotify(float value) {
        Slider.SetValueWithoutNotify((value - Shift) / Scale);
        TextMesh.text = string.Format(LocalizationManager.Localize(TextKey), value);
    }

    public void InvokeEvents(float value) {
        FunctionToInvokeOnSetValueEvent.Invoke(Scale * value + Shift);
    }
}
