using UnityEngine;

public class PlayerCollision : MonoBehaviour {

    // Update is called once per frame
    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.GetComponent<EndpointScript>()){
            collision.gameObject.GetComponent<EndpointScript>().EndLevel();
        } else if (collision.gameObject.GetComponent<TutorialHintScript>()) {
            CanvasManager.CanvasManagerInstance.DisplayTutorialText(collision.gameObject.GetComponent<TutorialHintScript>().TutorialHintText);
        }
    }
}
