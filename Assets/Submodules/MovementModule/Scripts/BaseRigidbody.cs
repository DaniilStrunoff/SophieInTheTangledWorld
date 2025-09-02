using Unity.VisualScripting;
using UnityEngine;


public abstract class BaseRigidbody : MonoBehaviour {
    public float GravityMagnitude = 1;

    [SerializeField]
    protected float mass = 1;

    public float Mass {
        get {
            return mass;
        }
        set {
            mass = value;
            ThisRigidbody.mass = mass;
        }
    }

    [SerializeField]
    protected float drag;

    public float Drag {
        get {
            return drag;
        }
        set {
            drag = value;
            ThisRigidbody.linearDamping = drag;
        }
    }

    private Rigidbody _cachedRigidbody;
    [HideInInspector]
    public Rigidbody ThisRigidbody {
        get {
            if (ReferenceEquals(_cachedRigidbody, null)) {
                if (!TryGetComponent(out _cachedRigidbody)) {
                    _cachedRigidbody = gameObject.AddComponent<Rigidbody>();
                }
            }
            return _cachedRigidbody;
        }
    }

    private Transform _cachedRigidbodyTransform;
    [HideInInspector]
    public Transform ThisRigidbodyTransform {
        get {
            if (ReferenceEquals(_cachedRigidbodyTransform, null)) {
                if (!TryGetComponent(out _cachedRigidbodyTransform)) {
                    _cachedRigidbodyTransform = ThisRigidbody.transform;
                }
            }
            return _cachedRigidbodyTransform;
        }
    }

    void Awake() {
        // Drag = 30; // 1  / Time.fixedDeltaTime - 1;
    }

    void OnValidate () {
        Mass = mass;
    }
}
