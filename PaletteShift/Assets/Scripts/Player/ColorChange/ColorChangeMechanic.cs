using UnityEngine;

public class ColorChangeMechanic : MonoBehaviour {
    SpriteRenderer PlayerCharacterSpriteRenderer;

    void Start() {
        PlayerCharacterSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ChangeColor(Color TargetColor) {

        // Sherlock Comment: Store texture of actual sprite, loop through pixels in texture, check if each pixel actually contains a color, 
        // and if it's black, change; if new color, revert back to black

        Texture2D StickmanTexture = PlayerCharacterSpriteRenderer.sprite.texture;

        for (int y = 0; y < StickmanTexture.height; y++) {
            for (int x = 0; x < StickmanTexture.width; x++) {
                if (
                    StickmanTexture.GetPixel(x, y).r != 0f ||
                    StickmanTexture.GetPixel(x, y).g != 0f ||
                    StickmanTexture.GetPixel(x, y).b != 0f ||
                    StickmanTexture.GetPixel(x, y).a != 0f
                ) {
                    if (StickmanTexture.GetPixel(x, y) != new Color(0.000f, 0.000f, 0.000f, 0.000f)) {
                        StickmanTexture.SetPixel(x, y, TargetColor);
                    }
                }
            }
        }

        StickmanTexture.Apply();
    }
}
