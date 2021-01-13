using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScript : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite GetSprite()
    {
        return spriteRenderer.sprite;
    }
}
