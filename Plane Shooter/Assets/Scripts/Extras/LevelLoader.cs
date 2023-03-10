using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    public GameObject loadingCanvas;
    //public GameObject pausePanel;
    //public GameObject pauseButton;
    public Animator transition;
    public float transitionTime = 1f;
    int currentIndex;

    private void Awake() 
    {
        loadingCanvas.SetActive(false);
       // pausePanel.SetActive(false);
        //pauseButton.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        currentIndex = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {
        // if(Input.GetMouseButtonDown(0))
        // {
        //     NextLevel();
        // }
    }

    public void Reload()
    {
        loadingCanvas.SetActive(true);
        SceneManager.LoadScene(currentIndex);
    }

    public void QuitGame()
    {
        loadingCanvas.SetActive(true);
        Application.Quit();
    }

    public void NextLevel()
    {
        // SceneManager.LoadScene(currentIndex + 1);
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void MainMenu()
    {
        // SceneManager.LoadScene(currentIndex + 1);
        loadingCanvas.SetActive(true);
        StartCoroutine(LoadLevel(0));
    }
    

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");
        Time.timeScale = 1f;
        loadingCanvas.SetActive(true);
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }

    public void ResumeGame()
    {
        //pausePanel.SetActive(false);
        Time.timeScale = 1f;
        //pauseButton.SetActive(false);
    }
}
