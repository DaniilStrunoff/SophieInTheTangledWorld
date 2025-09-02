using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


[RequireComponent(typeof(Image))]
public class InputImageController : MonoBehaviour {

    public PlayerInputController PlayerInputController;
    public Sprite GamepadImage;
    public Sprite TouchscreenImage;
    public Sprite KeyboardImage;

    private Image image;

    void Start() {
        image = GetComponent<Image>();
        PlayerInputController.DeviceChangeEvent += OnDeviceChange;
        OnDeviceChange(PlayerInputController.LastUsedDevice);
    }

    private void OnDeviceChange(InputDevice device) {
        if (image == null) Start();
        switch (device) {
            case Keyboard or Mouse:
            image.sprite = KeyboardImage;
            break;
            case Gamepad:
            image.sprite = GamepadImage;
            break;
            case Touchscreen:
            image.sprite = TouchscreenImage;
            break;
        }
    }
}
