using UnityEngine;

public class ColorChangeMechanic: MonoBehaviour
{
    SpriteRenderer playerCharacterSpriteRenderer;

    [SerializeField]
    Color newColor;

    [SerializeField]
    Color defaultColor;

    void Start() {
        playerCharacterSpriteRenderer = GetComponent<SpriteRenderer>();
    
        defaultColor = Color.black;
    }

    void Update() {
        if (Input.GetKeyUp(KeyCode.J)){
            ChangeColour();
        }
    }

    void ChangeColour(){

        // Sherlock Comment: Store texture of actual sprite, loop through pixels in texture, check if each pixel actually contains a color, 
        // and if it's black, change; if new color, revert back to black

        Texture2D StickmanTexture = playerCharacterSpriteRenderer.sprite.texture;

        for(int y = 0; y < StickmanTexture.height; y++) {
            for (int x = 0; x < StickmanTexture.width; x++) {
                    if(StickmanTexture.GetPixel(x, y) != new Color(0.000f, 0.000f, 0.000f, 0.000f)) {
                        if(StickmanTexture.GetPixel(x, y) == defaultColor) {
                        StickmanTexture.SetPixel(x, y, newColor);
                    } else {
                        StickmanTexture.SetPixel(x, y, defaultColor);
                    }
                }
            }      
        }

        StickmanTexture.Apply();
    }
}
