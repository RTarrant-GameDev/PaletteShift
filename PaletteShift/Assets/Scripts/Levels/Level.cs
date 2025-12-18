using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Scriptable Objects/Level", order = 0)]
public class Level : ScriptableObject
{
    public GameObject LevelStructurePrefab;
    public int LevelNumber;
    public bool LevelCompleted;
    public bool LevelUnlocked;
    public Sprite LevelScreenshot;
}
