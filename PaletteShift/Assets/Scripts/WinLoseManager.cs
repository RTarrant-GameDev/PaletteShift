using UnityEngine;

public class WinLoseManager : MonoBehaviour {

    public static WinLoseManager WinLoseManagerInstance {get; private set;}

    private void Awake() {
        if(WinLoseManagerInstance != null && WinLoseManagerInstance != this) {
            Destroy(this);
            return;
        }

        WinLoseManagerInstance = this;
    }
    
    public void EndLevel(bool EndpointReached) {
        if (EndpointReached == true) {
            Debug.Log("Player has reached endpoint");
        } else {
            Debug.Log("Game Over");
        }
    }
}
