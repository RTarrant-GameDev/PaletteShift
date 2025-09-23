using UnityEngine;

public class WinLoseManager : MonoBehaviour {

    public static WinLoseManager WinLoseManagerInstance {get; private set;}

    public float MaxTime;

    [SerializeField] private GameObject Player;

    private void Awake() {
        if(WinLoseManagerInstance != null && WinLoseManagerInstance != this) {
            Destroy(this);
            return;
        }

        WinLoseManagerInstance = this;
    }

    private void Start() {
        Player = GameObject.Find("PlayerCharacter");
    }
    
    public void EndLevel(bool EndpointReached) {
        if (EndpointReached == true) {
            Debug.Log("Player has reached endpoint");
            MissionTimer.TimerInstance.StopTimer();
            float HealthScore = (Player.GetComponent<HealthManagementScript>().HealthModel.CurrentHealth / Player.GetComponent<HealthManagementScript>().HealthModel.MaxHealth) *.6f;
            float TimeScore = (1.0f - (MissionTimer.TimerInstance.TimePassed / MaxTime)) * .4f;
            float FinalScore = MissionScoreCalculatorScript.MissionScoreCalculatorInstance.CalculateScore(HealthScore, TimeScore);
            Debug.LogFormat("FinalScore: {0}", FinalScore);
        } else {
            Debug.Log("Game Over");
        }
    }
}
