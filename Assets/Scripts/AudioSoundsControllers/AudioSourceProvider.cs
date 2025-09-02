using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

[RequireComponent(typeof(AudioSource))]
public class AudioSourceProvider : MonoBehaviour {
    public AudioClip AudioResource;
    private AudioSource audioSource;

    void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    public void Play(InputAction.CallbackContext context) {
        if (context.performed) Play();
    }

    public void Play() {
        if (!gameObject.activeInHierarchy) return;
        audioSource.PlayOneShot(AudioResource);
    }

    public void Play(AudioClip audioClip) {
        if (!gameObject.activeInHierarchy) return;
        audioSource.PlayOneShot(audioClip);
    }

    public void PlayIfSelected(BaseEventData baseEventData) {
        if (!gameObject.activeInHierarchy) return;
        if (!ReferenceEquals(baseEventData.selectedObject, gameObject)) return;
        Play();
    }
}
