using UnityEngine;

public class ColorMatchMechanic : MonoBehaviour
{
    SpriteRenderer PlayerCharacterSpriteRenderer;

    void Start()
    {
        PlayerCharacterSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    public bool CompareColorWithObstacle(Color TargetColor)
    {
        Texture2D StickmanTexture = PlayerCharacterSpriteRenderer.sprite.texture;

        for (int y = 0; y < StickmanTexture.height; y++)
        {
            for (int x = 0; x < StickmanTexture.width; x++)
            {
                if (
                    StickmanTexture.GetPixel(x, y).r != 0f ||
                    StickmanTexture.GetPixel(x, y).g != 0f ||
                    StickmanTexture.GetPixel(x, y).b != 0f ||
                    StickmanTexture.GetPixel(x, y).a != 0f
                )
                {
                    if (StickmanTexture.GetPixel(x, y) != TargetColor)
                    {
                        return false;
                    }
                }
            }
        }

        // return true by default, as it will return false if there is a pixel that doesn't match the target color
        return true;
    }
}
