using UnityEngine;


public class Observer : MonoBehaviour {
    private LookAtManager lookAtManager;

    void Start() {
        lookAtManager = gameObject.AddComponent<LookAtManager>();
    }
}
