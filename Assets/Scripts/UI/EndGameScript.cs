using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameScript : MonoBehaviour
{
    [SerializeField]
    private GameObject endGameCanvas;
    // Start is called before the first frame update
    void Start()
    {

    }
    public void ActiveEndGameCanvas()
    {
        endGameCanvas.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
