using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// this script will be attached to PauseMenu panel
/// </summary>
public class PauseMenu : MonoBehaviour
{
	public static bool GameIsPaused = false;

	[SerializeField]
	private GameObject pauseMenuUI;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (GameIsPaused)
			{
				Resume();
			}
			else
			{
				Pause();
			}
		}
	}

	public void Resume()
	{
		pauseMenuUI.SetActive(false);
		Time.timeScale = 1.0f;
		GameIsPaused = false;
	}

	private void Pause()
	{
		pauseMenuUI.SetActive(true);
		Time.timeScale = 0;
		GameIsPaused = true;
	}

	public void LoadMenu()
	{
		SceneManager.LoadScene("GameMenu");
		Time.timeScale = 1.0f;
	}

	public void Restart()
	{
		Resume();
        TimeController.startTime = Time.time;
        Debug.Log(TimeController.startTime);
        PlayerController._currentGold = 0;
		ParametersScript.scoreValue = 0;
        SceneManager.LoadScene("Dungeon2");
    }
}
