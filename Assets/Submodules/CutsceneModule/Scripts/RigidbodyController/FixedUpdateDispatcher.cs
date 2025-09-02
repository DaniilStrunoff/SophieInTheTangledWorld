using System;
using UnityEngine;

public class FixedUpdateDispatcher : MonoBehaviour
{
    public static event Action OnFixedUpdate;

    private void FixedUpdate() {
        OnFixedUpdate?.Invoke();
    }

    public static void EnsureExists() {
        if (FindFirstObjectByType<FixedUpdateDispatcher>() == null) {
            var go = new GameObject("FixedUpdateDispatcher (Auto)");
            go.hideFlags = HideFlags.HideAndDontSave; // Скрыть объект в редакторе
            go.AddComponent<FixedUpdateDispatcher>();
        }
    }
}
