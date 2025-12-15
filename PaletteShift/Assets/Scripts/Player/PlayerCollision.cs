using UnityEngine;

public class PlayerCollision : MonoBehaviour {

    // Update is called once per frame
    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.GetComponent<EndpointScript>()) 
            collision.gameObject.GetComponent<EndpointScript>().EndLevel();
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.GetComponent<TutorialHintScript>()) 
            CanvasManager.CanvasManagerInstance.DisplayTutorialText(collider.gameObject.GetComponent<TutorialHintScript>().TutorialHintText);  
    }
}
