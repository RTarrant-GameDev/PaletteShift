using UnityEngine;

public class GameSystemManagerScript : MonoBehaviour {
    // Kick off essential game systems on Start (StartTimer, etc)
    void Start() {
        MissionTimer.TimerInstance.StartTimer();
    }

}
