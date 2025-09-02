using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Graphic))]
public class MultiTargetGraphics : MonoBehaviour {
    [SerializeField]
    private List<Graphic> targetGraphics = new() {};

    public List<Graphic> GetTargetGraphics => targetGraphics;

    void OnValidate() {
        if (targetGraphics.Count == 0) {
            targetGraphics.Add(GetComponent<Graphic>());
        }
    }
}
