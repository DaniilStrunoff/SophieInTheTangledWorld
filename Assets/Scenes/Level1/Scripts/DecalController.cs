using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(DecalProjector))]
public class DecalController : MonoBehaviour {

    [Range(0, 1)]
    public float FadeFactor;
    private DecalProjector decalProjector;

    void Awake() {
        decalProjector = GetComponent<DecalProjector>();
        FadeFactor = decalProjector.fadeFactor;
    }

    void LateUpdate()
    {
        decalProjector.fadeFactor = FadeFactor;
    }
}
