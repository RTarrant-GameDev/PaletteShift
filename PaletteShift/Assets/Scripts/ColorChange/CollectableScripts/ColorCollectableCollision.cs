using UnityEngine;

public class ColorCollectableCollision : MonoBehaviour {

    // Update is called once per frame
    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player" && other.gameObject.GetComponent<ColorMatchMechanic>().CompareColorWithObstacle(GetComponent<ColorCollectableScript>().CollectableColor) != true) {
            GameObject.Find("ColorChangeManagerObject").GetComponent<ColorChangeManager>().TriggerColorChange(GetComponent<ColorCollectableScript>().GetCollectableColor());
            Destroy(this.gameObject);
        }  
    }
}
