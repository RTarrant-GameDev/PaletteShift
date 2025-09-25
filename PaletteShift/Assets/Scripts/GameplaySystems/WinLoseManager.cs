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
            MissionTimer.TimerInstance.StopTimer();
            float HealthScore = ((float)Player.GetComponent<HealthManagementScript>().HealthModel.CurrentHealth / (float)Player.GetComponent<HealthManagementScript>().HealthModel.MaxHealth) * .6f;
            float TimeScore = (1f - (MissionTimer.TimerInstance.TimePassed/MaxTime)) * .4f;
            float FinalScore = MissionScoreCalculatorScript.MissionScoreCalculatorInstance.CalculateScore(HealthScore, TimeScore);

            Debug.Log($"Health: {HealthScore}");
            Debug.Log($"Time: {TimeScore}");
            Debug.Log($"Final score: {FinalScore}");

            if(FinalScore > 100.0f) {
                FinalScore = 100.0f;
            }
            
            CanvasManager.CanvasManagerInstance.LevelComplete(Player.GetComponent<HealthManagementScript>().HealthModel.CurrentHealth, MissionTimer.TimerInstance.TimePassed, FinalScore);
        } else {
            CanvasManager.CanvasManagerInstance.GameOver();
        }
    }
}
