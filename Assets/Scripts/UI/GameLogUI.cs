using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLogUI : MonoBehaviour
{
    public string sceneName;
    [SerializeField]
    public GameObject scorePanel;

    void Start()
    {
    }
    public void StartGame()
    {
        TimeController.startTime = Time.time;
        Debug.Log(TimeController.startTime);
        PlayerController._currentGold = 0;
        SceneManager.LoadScene(sceneName);
    }

    public void ExitGame()
    {

        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

    public void ToggleScore()
    {
        if (scorePanel != null && !scorePanel.activeSelf)
        {
            scorePanel.SetActive(true);
            Text txtScore = scorePanel.transform.Find("txtScore").GetComponent<Text>();
            txtScore.text = "Best score: " + PlayerPrefs.GetInt("BestScore", 0).ToString();
            //txtScore.text = "Best score: " + (TimeController.endTime - TimeController.startTime).ToString("F2") + "s";
        }
        else
        {
            scorePanel.SetActive(false);
        }
    }
}
