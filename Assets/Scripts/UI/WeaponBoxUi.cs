using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WeaponBoxUi : MonoBehaviour
{
    public Image image;
    public List<Sprite> sprites;
    private int currentSprites;
    public KeyCode keyCode;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyCode))
        {
            NextSprites();
        }
    }
    void NextSprites()
    {
        currentSprites++;
        if (currentSprites >= sprites.Count)
        {
            currentSprites = 0;
        }
        image.sprite = sprites[currentSprites];
    }
}
