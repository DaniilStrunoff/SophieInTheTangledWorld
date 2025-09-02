using UnityEngine;

public class OpenLink : MonoBehaviour {
    public string link;

    public void OpenLunk() {
        Application.OpenURL(link);
    }
}
