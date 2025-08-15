using UnityEngine;

public class TimerManager : MonoBehaviour {
    float MissionTime = 0.0f;

    // Update is called once per frame
    void Update() {
        MissionTime -= Time.deltaTime;
    }
}
