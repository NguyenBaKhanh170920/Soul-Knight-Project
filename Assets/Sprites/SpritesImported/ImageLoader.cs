using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageLoader : MonoBehaviour
{
    public Image imageContainer; // Reference to the UI Image component
    public Sprite[] images;      // Array to hold the images
    public float interval = 2f;  // Time interval between image changes

    private int currentIndex = 0;

    void Start()
    {
        if (images.Length > 0)
        {
            StartCoroutine(LoadImages());
        }
    }

    IEnumerator LoadImages()
    {
        while (true)
        {
            imageContainer.sprite = images[currentIndex];
            currentIndex = (currentIndex + 1) % images.Length;
            yield return new WaitForSeconds(interval);
        }
    }
}
