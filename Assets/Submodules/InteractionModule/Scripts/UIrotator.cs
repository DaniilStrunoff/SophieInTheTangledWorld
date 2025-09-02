using Unity.VisualScripting;
using UnityEngine;


public class UIrotator : MonoBehaviour {

    public Transform cameraTransform;

    public float distance = 1f;

    private Canvas canvas;

    void Start() {
        canvas = GetComponentInParent<Canvas>();
    }

    void Update() {
        Vector3 vec1 = canvas.transform.position - cameraTransform.position;
        Vector3 vec2 = Vector3.Project(vec1, cameraTransform.forward);
        transform.SetPositionAndRotation(
            cameraTransform.position + distance / vec2.magnitude * vec1,
            cameraTransform.rotation);
        Debug.DrawLine(canvas.transform.position, cameraTransform.position);
    }
}
