using UnityEngine;


public abstract class BaseMimic : MonoBehaviour  {

    public GameObject objectToMimic;

    public Transform centerOfSimetry;

    protected GameObject mimicObject;

    protected Animator mimicAnimator;

    protected Animator animatorToMimic;

    protected abstract (Vector3, Quaternion) GetPositionAndRotation();

    virtual public void Start() {
        if (objectToMimic == null) objectToMimic = gameObject;
        (Vector3 position, Quaternion rotation) = GetPositionAndRotation();
        mimicObject = Instantiate(objectToMimic, position, rotation * objectToMimic.transform.rotation);
        foreach (BaseMimic baseMimic in mimicObject.GetComponents<BaseMimic>()) {
            Destroy(baseMimic);
        }
        animatorToMimic = (Animator)gameObject.GetComponentInChildren(typeof(Animator));
        if (animatorToMimic != null) {
            mimicAnimator = mimicObject.GetComponentInChildren<Animator>();
        }
    }

    void LateUpdate() {
        (Vector3 position, Quaternion rotation) = GetPositionAndRotation();
        mimicObject.transform.SetLocalPositionAndRotation(position, rotation);
        if (animatorToMimic != null) {
            mimicAnimator.SetFloat("MoveSpeed", animatorToMimic.GetFloat("MoveSpeed"));
            mimicAnimator.SetFloat("MoveBlend", animatorToMimic.GetFloat("MoveBlend"));
            mimicAnimator.SetFloat("RunBlend", animatorToMimic.GetFloat("RunBlend"));
        }
    }
}