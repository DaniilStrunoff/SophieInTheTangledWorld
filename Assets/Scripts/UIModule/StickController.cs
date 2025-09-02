using UnityEngine;


public class StickController : MonoBehaviour {
    void Start() {
        bool active = false;
        switch (Application.platform) {
            case RuntimePlatform.WindowsPlayer:
            case RuntimePlatform.OSXPlayer:
            case RuntimePlatform.LinuxPlayer:
            case RuntimePlatform.LinuxEditor:
            active = false;
            break;
            case RuntimePlatform.Android:
            case RuntimePlatform.IPhonePlayer:
            active = true;
            break;
        }
        gameObject.SetActive(active);
    }
}
