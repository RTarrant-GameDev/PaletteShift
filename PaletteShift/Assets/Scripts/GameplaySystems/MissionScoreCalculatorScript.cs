using UnityEngine;

public class MissionScoreCalculatorScript : MonoBehaviour {
    public static MissionScoreCalculatorScript MissionScoreCalculatorInstance {get; private set;}


    private void Awake() {
        if (MissionScoreCalculatorInstance != null && MissionScoreCalculatorInstance != this) {
            Destroy(this);
            return;
        }

        MissionScoreCalculatorInstance = this;
    }

    public float CalculateScore(float Health, float Time) {
        float finalScore = (Health + Time) * 100;

        return finalScore;
    }
}
