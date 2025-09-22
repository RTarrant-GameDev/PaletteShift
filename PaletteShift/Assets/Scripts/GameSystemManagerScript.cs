using UnityEngine;

public class GameSystemManagerScript : MonoBehaviour {

    public static GameSystemManagerScript GameSystemManagerInstance {get; private set;}

    private void Awake() {
        if(GameSystemManagerInstance != null && GameSystemManagerInstance != this) {
            Destroy(this);
            return;
        }

        GameSystemManagerInstance = this;
    }

    // Kick off essential game systems on Start (StartTimer, etc)
    void Start() {
        MissionTimer.TimerInstance.StartTimer();
    }

    public void EndLevel(bool EndpointReached) {
        if (EndpointReached == true) {
            Debug.Log("Player has reached endpoint");
        } else {
            Debug.Log("Game Over");
        }
    }
}
