using UnityEngine;

public class LevelManager : MonoBehaviour {
    public Level TutorialLevel;
    public Level[] Levels;
    public GameObject NewLevel;
    public static Level SelectedLevel;

    [SerializeField]
    private Level NextLevel;
    public Level CurrentLevel;

    public static LevelManager LevelManagerInstance { get; private set; }

    private void Awake() {
        if(LevelManagerInstance != null && LevelManagerInstance != this) {
            Destroy(this);
            return;
        }

        LevelManagerInstance = this;
    }

    void Start() {
        EventObserver.StartListening("GenerateLevel", GenerateLevel);
        EventObserver.StartListening("GenerateNextLevel", GenerateNextLevel);
        EventObserver.StartListening("GenerateTutorialLevel", GenerateTutorialLevel);
        EventObserver.StartListening("SetLevelAsComplete", SetLevelAsComplete);
        EventObserver.StartListening("GenerateCurrentLevel", GenerateCurrentLevel);
        EventObserver.StartListening("DestroyCurrentLevel", DestroyCurrentLevel);
        
        foreach(Level level in Levels) {
            if(level.LevelCompleted == false && level.LevelUnlocked == true) NextLevel = level;
        }
    }

    public static void SetSelectedLevel(Level LevelChoice) {
        SelectedLevel = LevelChoice;
    }

    void GenerateLevel() {
        NewLevel = Instantiate(SelectedLevel.LevelStructurePrefab) as GameObject;
        NewLevel.transform.parent = this.gameObject.transform;
        CurrentLevel = SelectedLevel;
    }

    void GenerateTutorialLevel() {
        NewLevel = Instantiate(TutorialLevel.LevelStructurePrefab) as GameObject;
        NewLevel.transform.parent = this.gameObject.transform;
        CurrentLevel = TutorialLevel;
    }

    void GenerateNextLevel() {
        NewLevel = Instantiate(NextLevel.LevelStructurePrefab) as GameObject;
        NewLevel.transform.parent = this.gameObject.transform;
        CurrentLevel = NextLevel;
    }

    void SetLevelAsComplete() {
        CurrentLevel.LevelCompleted = true;
        if(CurrentLevel.LevelNumber > 0 && CurrentLevel.LevelNumber < 6) Levels[CurrentLevel.LevelNumber].LevelUnlocked = true;

        foreach(Level level in Levels) {
            if(level.LevelCompleted == false && level.LevelUnlocked == true) NextLevel = level; 
        }
    }

    void GenerateCurrentLevel() {
        NewLevel = Instantiate(CurrentLevel.LevelStructurePrefab) as GameObject;
        NewLevel.transform.parent = this.gameObject.transform;
    }

    void DestroyCurrentLevel() {
        Destroy(this.transform.GetChild(0).gameObject);
    }

    void OnDestroy()
    {
        EventObserver.StopListening("GenerateLevel", GenerateLevel);
        EventObserver.StopListening("GenerateNextLevel", GenerateNextLevel);
        EventObserver.StopListening("GenerateTutorialLevel", GenerateTutorialLevel);
        EventObserver.StopListening("SetLevelAsComplete", SetLevelAsComplete);
        EventObserver.StopListening("GenerateCurrentLevel", GenerateCurrentLevel);
        EventObserver.StopListening("DestroyCurrentLevel", DestroyCurrentLevel);
    }
}
