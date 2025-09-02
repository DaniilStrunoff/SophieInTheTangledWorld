using UnityEngine;


public class TimeScaleController : MonoBehaviour, IController {

    public void SetTimeScaleToNormal() {
        Time.timeScale = 1;
    }

    public void SetTimeScaleToZero() {
        Time.timeScale = 0;
    }

    public void SetTimeScaleToFast() {
        Time.timeScale = 5;
    }
}
