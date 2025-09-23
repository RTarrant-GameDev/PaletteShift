using UnityEngine;

public class EndpointScript : MonoBehaviour
{
    public void EndLevel() {
        WinLoseManager.WinLoseManagerInstance.EndLevel(true);
    }
}
