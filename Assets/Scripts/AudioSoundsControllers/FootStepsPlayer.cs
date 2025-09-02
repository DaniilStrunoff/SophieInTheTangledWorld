using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class FootStepsPlayer : MonoBehaviour {
    public AudioClip LeftFootAudioClip;
    public AudioClip RightFootAudioClip;
    public AnimationEventsProvider animationEventsProvider;
    private AudioSource audioSource;

    void Start() {
        audioSource = GetComponent<AudioSource>();
        animationEventsProvider.LeftFootStep += PlayLeftFootStep;
        animationEventsProvider.RightFootStep += PlayRightFootStep;
    }

    private void PlayLeftFootStep() {
        audioSource.pitch = Random.Range(0.875f, 1f);
        audioSource.resource = LeftFootAudioClip;
        audioSource.Play();
    }

    private void PlayRightFootStep() {
        audioSource.pitch = Random.Range(0.75f, 0.875f);
        audioSource.resource = RightFootAudioClip;
        audioSource.Play();
    }
}
