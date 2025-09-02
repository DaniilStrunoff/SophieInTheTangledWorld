using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChangeControls : MonoBehaviour {
    public PlayerInputController playerInputController;
    public TextMeshProUGUI textMeshProUGUI;
    int i = 0;

    void Awake() {
        if (Debug.isDebugBuild) {
            playerInputController.DeviceChangeEvent += OnDevieChange;
            gameObject.SetActive(true);
        } else {
            gameObject.SetActive(false);
        }
    }

    public void OnControlsChange(PlayerInput playerInput) {
        if (!Debug.isDebugBuild) return;
        i++;
        textMeshProUGUI.text += $"\n{i}, {playerInput.currentControlScheme}";
    }

    public void OnDevieChange(InputDevice inputDevice) {
        i++;
        textMeshProUGUI.text = $"{i}, {inputDevice.layout}";
    }
}
