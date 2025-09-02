using UnityEngine;


public abstract class AbstractFadeConroller : MonoBehaviour, IController {
    public float fadeTime = 1;

    public float FadeTime => fadeTime;

    public float DarkTime = 1;

    public bool IsInitiallyActive;

    [HideInInspector]
    public bool IsFadeComplited;

    [HideInInspector]
    public bool IsUnfadeComplited;

    public CanvasGroup fadeCanvas;

    private float time = 0f;

    void Awake() {
        fadeCanvas.gameObject.SetActive(IsInitiallyActive);
        IsFadeComplited = IsInitiallyActive;
        IsUnfadeComplited = !IsInitiallyActive;
        fadeCanvas.alpha = IsInitiallyActive ? 1 : 0;
    }

    public virtual void FadeOut() {
        if (IsUnfadeComplited) return;
        if (fadeCanvas.alpha > 0) {
            fadeCanvas.alpha -= Time.unscaledDeltaTime > 0.1 ? 0 : Time.unscaledDeltaTime / FadeTime;
            IsFadeComplited = false;
            IsUnfadeComplited = false;
        } else {
            fadeCanvas.gameObject.SetActive(false);
            fadeCanvas.alpha = 0f;
            IsUnfadeComplited = true;   
        }
    }

    public virtual void FadeIn() {
        if (IsFadeComplited) return;
        fadeCanvas.gameObject.SetActive(true);
        if (fadeCanvas.alpha < 1) {
            fadeCanvas.alpha += Time.unscaledDeltaTime > 0.1 ? 0 : Time.unscaledDeltaTime / FadeTime;
            IsFadeComplited = false;
            IsUnfadeComplited = false;
        } else if (time <= DarkTime) {
            fadeCanvas.alpha = 1;
            time += Time.unscaledDeltaTime;
            IsUnfadeComplited = false;
            IsFadeComplited = false;
        } else {
            time = 0f;
            IsFadeComplited = true;
        }
    }

    public virtual void FadeOut(float deltaTime) {
        if (IsUnfadeComplited) return;
        if (fadeCanvas.alpha > 0) {
            fadeCanvas.alpha -= deltaTime > 0.1 ? 0 : deltaTime / FadeTime;
            IsFadeComplited = false;
            IsUnfadeComplited = false;
        } else {
            fadeCanvas.gameObject.SetActive(false);
            fadeCanvas.alpha = 0f;
            IsUnfadeComplited = true;   
        }
    }

    public virtual void FadeIn(float deltaTime) {
        if (IsFadeComplited) return;
        fadeCanvas.gameObject.SetActive(true);
        if (fadeCanvas.alpha < 1) {
            fadeCanvas.alpha += deltaTime > 0.1 ? 0 : deltaTime / FadeTime;
            IsFadeComplited = false;
            IsUnfadeComplited = false;
        } else if (time <= DarkTime) {
            fadeCanvas.alpha = 1;
            time += Time.unscaledDeltaTime;
            IsUnfadeComplited = false;
            IsFadeComplited = false;
        } else {
            time = 0f;
            IsFadeComplited = true;
        }
    }
}
