using UnityEngine;

public class GameOverScript : MonoBehaviour {
    
    public void RestartFunction(){
        GameSystemManagerScript.GameSystemManagerInstance.RestartGame();
    }

    public void QuitFunction() {
        GameSystemManagerScript.GameSystemManagerInstance.QuitToMainMenu();
    }
}
