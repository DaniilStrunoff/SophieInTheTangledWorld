using UnityEngine;
using UnityEngine.InputSystem;

public class CursorController : MonoBehaviour {
    public PlayerInputController PlayerInputController;

    public void Awake() {
        PlayerInputController.DeviceChangeEvent += OnDeviceChange;
        OnDeviceChange(PlayerInputController.LastUsedDevice);
    }

    public void OnDeviceChange(InputDevice device) {
        //PlayerInputController.SetSelectedGameObjectToNull();
        switch (device) {
            case Keyboard:
                Cursor.visible = false;
                break;
            case Gamepad:
                Cursor.visible = false;
                break;
            case Mouse:
                Cursor.visible = true;
                PlayerInputController.GraphicRaycaster.enabled = true;
                break;
        }
    }
}
