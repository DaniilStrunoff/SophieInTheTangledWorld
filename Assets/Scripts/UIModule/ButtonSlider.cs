using System;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


[RequireComponent(typeof(Slider))]
public class ButtonSlider : MonoBehaviour {
    public Slider slider;
    public GameObject ButtonLeft;
    public GameObject ButtonRight;
    public ButtonFadeController ButtonLeftFade;
    public ButtonFadeController ButtonRightFade;

    [SerializedDictionary(keyName: "Element", valueName: "TextGameObject")]
    public SerializedDictionary<string, GameObject> Elements;

    public UnityEvent<string> ValueChangedEvent;

    private Dictionary<string, int> elementsEnumMap;

    private Dictionary<int, string> elementsEnumMapInverse;

    void InitializeIfNeeded() {
        if (elementsEnumMap != null) return;
        elementsEnumMap = new();
        int i = 0;
        foreach (var key in Elements.Keys) {
            elementsEnumMap[key] = i;
            i++;
        }
        elementsEnumMapInverse = new();
        foreach ((var key, var value) in elementsEnumMap) {
            elementsEnumMapInverse[value] = key;
        }
    }

    [ExecuteInEditMode]
    private void OnValidate() {
        slider.wholeNumbers = true;
        slider.minValue = 0;
        float maxValue = Elements.Count > 0 ? Elements.Count - 1 : 0;
        slider.maxValue = maxValue;
    }

    public void CheckCorners(float value) {
        if (Mathf.Abs(value - slider.maxValue) < 0.1) {
            ButtonRightFade.SetButtonInactive();
        } else {
            ButtonRightFade.SetButtonActive();
        }
        if (Mathf.Abs(value - slider.minValue) < 0.1) {
            ButtonLeftFade.SetButtonInactive();
        } else {
            ButtonLeftFade.SetButtonActive();
        }
    }

    public void SetValueWithoutNotify(string value) {
        InitializeIfNeeded();
        slider.SetValueWithoutNotify(elementsEnumMap[value]);
        foreach ((var key_, var value_) in Elements) {
            value_.SetActive(key_ == value);
        }
        CheckCorners(elementsEnumMap[value]);
    }

    public void InvokeEvents(float value) {
        ValueChangedEvent.Invoke(elementsEnumMapInverse[Mathf.RoundToInt(value)]);
    }

    public void MakePerviousValue() {
        float value = slider.value - 1;
        if (value > -1) {
            InvokeEvents(value);
        }
    }

    public void MakeNextValue() {
        float value = slider.value + 1;
        if (value < slider.maxValue + 0.5) {
            InvokeEvents(value);
        }
    }
}
