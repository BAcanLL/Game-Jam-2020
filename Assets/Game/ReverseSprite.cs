using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseSprite : MonoBehaviour
{
    public string spritePath;
    SpriteRenderer sprRenderer;
    Sprite[] sprites;

    // Start is called before the first frame update
    void Start()
    {
        sprRenderer = GetComponent<SpriteRenderer>();
        SetSpritePath(spritePath);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSpritePath(string path)
    {
        spritePath = path;
        sprites = Resources.LoadAll<Sprite>(spritePath);
    }

    public Sprite GetReverseSprite()
    {
        if (sprites.Length > 0)
        {
            string name = sprRenderer.sprite.name;
            int index = (int.Parse(name.Substring(name.LastIndexOf('_') + 1)) + sprites.Length / 2) % sprites.Length;
            return sprites[index];
        }
        return null;
    }

}
