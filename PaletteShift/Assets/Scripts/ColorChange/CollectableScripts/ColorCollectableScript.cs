using UnityEngine;

public class ColorCollectableScript : MonoBehaviour {

    public Color CollectableColor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        GetComponent<SpriteRenderer>().color = CollectableColor; //set color of collectable when level starts up
    }

    // Update is called once per frame
    void Update() {
    }

    public Color GetCollectableColor() {
        return CollectableColor;
    }
}
