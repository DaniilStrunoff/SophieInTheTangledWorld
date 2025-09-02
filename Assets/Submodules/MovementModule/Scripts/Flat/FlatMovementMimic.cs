using Unity.VisualScripting;
using UnityEngine;


public class FlatMovementMimic : BaseMimic {
    protected override (Vector3, Quaternion) GetPositionAndRotation() {
        return (new Vector3(objectToMimic.transform.position.x,
                            centerOfSimetry.transform.position.y,
                            objectToMimic.transform.position.z),
                Quaternion.FromToRotation(objectToMimic.transform.up, centerOfSimetry.up) * objectToMimic.transform.rotation);
    }
}
