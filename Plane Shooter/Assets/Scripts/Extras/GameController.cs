using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject pauseButton;
    public GameObject gameOver;
    public GameObject levelCompletePanel;
    public GameObject endText;

    private GameObject levelLoader;


    private void Awake() 
    {
        // levelLoader = GameObject.FindWithTag("LevelLoader");
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        pauseButton.SetActive(true);
        gameOver.SetActive(false);
        levelCompletePanel.SetActive(false);
        endText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        pauseButton.SetActive(false);
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        pauseButton.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GameOver()
    {
        gameOver.SetActive(true);
        pauseButton.SetActive(false);
    }

    public IEnumerator LevelComplete()
    {
        yield return new WaitForSeconds(2f);
        endText.SetActive(true);
        yield return new WaitForSeconds(3f);
        Time.timeScale = 0f;
        levelCompletePanel.SetActive(true);
        pauseButton.SetActive(false);
        Time.timeScale = 1f;
    }
}
