using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputInitializer : MonoBehaviour {
    public PlayerInput PlayerInput;

    void Awake() {
        //PlayerInput.defaultControlScheme = SaveAndLoadController.GetInstance().SaveData.ControlSchema;
    }
}
