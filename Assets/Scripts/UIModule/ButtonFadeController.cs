using System.Collections;
using UnityEngine;


public class ButtonFadeController : MonoBehaviour, IController {
    public float FadeTime = 1;

    [HideInInspector]
    private bool IsFadeComplited => Mathf.Approximately(ButtonCanvasGroup.alpha, 1f);

    [HideInInspector]
    private bool IsUnfadeComplited => Mathf.Approximately(ButtonCanvasGroup.alpha, 0f);

    public CanvasGroup ButtonCanvasGroup;

    private bool isInFadeInTransition = false;
    private bool isInFadeOutTransition = false;

    private IEnumerator FadeInCorutine() {
        isInFadeInTransition = true;
        while (!IsFadeComplited) {
            FadeIn();
            yield return new WaitForEndOfFrame();
        }
        ButtonCanvasGroup.gameObject.SetActive(true);
        isInFadeInTransition = false;
    }

    private IEnumerator FadeOutCorutine() {
        isInFadeOutTransition = true;
        while (!IsUnfadeComplited) {
            FadeOut();
            yield return new WaitForEndOfFrame();
        }
        ButtonCanvasGroup.gameObject.SetActive(false);
        isInFadeOutTransition = false;
    }

    public void SetButtonActive() {
        if (ButtonCanvasGroup.gameObject.activeInHierarchy) {
            if (!isInFadeInTransition) {
                StopAllCoroutines();
                isInFadeOutTransition = false;
                StartCoroutine(FadeInCorutine());
            }
        } else {
            ButtonCanvasGroup.alpha = 1;
            ButtonCanvasGroup.gameObject.SetActive(true);
        }
    }

    public void SetButtonInactive() {
        if (ButtonCanvasGroup.gameObject.activeInHierarchy) {
            if (!isInFadeOutTransition) {
                StopAllCoroutines();
                isInFadeInTransition = false;
                StartCoroutine(FadeOutCorutine());
            }
        } else {
            ButtonCanvasGroup.alpha = 0;
            ButtonCanvasGroup.gameObject.SetActive(false);
        }
    }

    public virtual void FadeOut() {
        ButtonCanvasGroup.alpha -= Time.unscaledDeltaTime > 0.1 ? 0 : Time.unscaledDeltaTime / FadeTime;
    }

    public virtual void FadeIn() {
        ButtonCanvasGroup.alpha += Time.unscaledDeltaTime > 0.1 ? 0 : Time.unscaledDeltaTime / FadeTime;
    }

    private void OnDisable() {
        if (isInFadeOutTransition) {
            ButtonCanvasGroup.alpha = 0;
            ButtonCanvasGroup.gameObject.SetActive(false);
            isInFadeOutTransition = false;
        }
        if (isInFadeInTransition) {
            ButtonCanvasGroup.alpha = 1;
            ButtonCanvasGroup.gameObject.SetActive(true);
            isInFadeInTransition = false;
        }
    }
}
