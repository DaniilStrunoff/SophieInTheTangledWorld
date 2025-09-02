using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class AutoButtonController : MonoBehaviour, IController {
    public float FadeTime = 1;

    [HideInInspector]
    private bool IsFadeComplited => Mathf.Approximately(AutoButtonSlider.value, 1f);

    [HideInInspector]
    private bool IsUnfadeComplited => Mathf.Approximately(AutoButtonSlider.value, 0f);

    public MultiGraphicButton AutoButton;
    private Slider AutoButtonSlider;

    private bool isInFadeInTransition = false;
    private bool isInFadeOutTransition = false;

    private void Awake() {
        AutoButtonSlider = AutoButton.gameObject.GetComponentInChildren<Slider>();
    }

    private IEnumerator FadeInCorutine() {
        isInFadeInTransition = true;
        while (!IsFadeComplited) {
            FadeIn();
            yield return new WaitForEndOfFrame();
        }
        isInFadeInTransition = false;
    }

    private IEnumerator FadeOutCorutine() {
        isInFadeOutTransition = true;
        while (!IsUnfadeComplited) {
            FadeOut();
            yield return new WaitForEndOfFrame();
        }
        isInFadeOutTransition = false;
    }

    public void SetAutoButtonActive() {
        if (AutoButtonSlider.gameObject.activeInHierarchy) {
            if (!isInFadeInTransition) {
                StopAllCoroutines();
                isInFadeOutTransition = false;
                StartCoroutine(FadeInCorutine());
            }
        } else {
            AutoButtonSlider.value = 1;
        }
		ColorBlock colorVar = AutoButton.colors;
		colorVar.normalColor = new Color (colorVar.normalColor.r, colorVar.normalColor.g, colorVar.normalColor.b, 0.65f);
		colorVar.highlightedColor = new Color (colorVar.highlightedColor.r, colorVar.highlightedColor.g, colorVar.highlightedColor.b, 1f);
		colorVar.pressedColor = new Color (colorVar.pressedColor.r, colorVar.pressedColor.g, colorVar.pressedColor.b, 1f);
		AutoButton.colors = colorVar;
    }

    public void SetAutoButtonInactive() {
        if (AutoButtonSlider.gameObject.activeInHierarchy) {
            if (!isInFadeOutTransition) {
                StopAllCoroutines();
                isInFadeInTransition = false;
                StartCoroutine(FadeOutCorutine());                         
            }
        } else {
            AutoButtonSlider.value = 0;
        }
		ColorBlock colorVar = AutoButton.colors;
		colorVar.normalColor = new Color (colorVar.normalColor.r, colorVar.normalColor.g, colorVar.normalColor.b, 0.2F);
		colorVar.highlightedColor = new Color (colorVar.highlightedColor.r, colorVar.highlightedColor.g, colorVar.highlightedColor.b, 0.5f);
		colorVar.pressedColor = new Color (colorVar.pressedColor.r, colorVar.pressedColor.g, colorVar.pressedColor.b, 0.5f);
		AutoButton.colors = colorVar;
    }

    public virtual void FadeOut() {
        AutoButtonSlider.value -= Time.unscaledDeltaTime > 0.1 ? 0 : Time.unscaledDeltaTime / FadeTime;
    }

    public virtual void FadeIn() {
        AutoButtonSlider.value += Time.unscaledDeltaTime > 0.1 ? 0 : Time.unscaledDeltaTime / FadeTime;
    }

    private void OnDisable() {
        if (isInFadeOutTransition) {
            AutoButtonSlider.value = 0;
            isInFadeOutTransition = false;
        }
        if (isInFadeInTransition) {
            AutoButtonSlider.value = 1;
            isInFadeInTransition = false;
        }
    }
}
