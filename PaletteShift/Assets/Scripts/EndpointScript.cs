using UnityEngine;

public class EndpointScript : MonoBehaviour
{
    public void EndLevel() {
        GameSystemManagerScript.GameSystemManagerInstance.EndLevel(true);
    }
}
