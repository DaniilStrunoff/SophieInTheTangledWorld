using UnityEngine;

public class AmbientController : MonoBehaviour {

    public Color color;

    void Update() {
        RenderSettings.ambientLight = color;
    }
}
