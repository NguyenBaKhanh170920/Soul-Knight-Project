using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject GameOverMenuUI;
    void Start()
    {
        
        TimeController.endTime = Time.time;
        Debug.Log(TimeController.endTime);
        StartCoroutine(LoadPanelAfterDelay());
    }

    // Update is called once per frame
    IEnumerator LoadPanelAfterDelay()
    {
        yield return new WaitForSeconds(2.5f);
        GameOverMenuUI.SetActive(true);
    }
}
