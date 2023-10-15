using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    //private bool gamePaused = false;
    [SerializeField]
    Transform pauseScreen;

    [SerializeField]
    Transform creditsScreen;

    [SerializeField]
    Transform tutorialScreen;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PauseGame()
    {
        //gamePaused = true;
        creditsScreen.gameObject.SetActive(false);
        pauseScreen.gameObject.SetActive(true);
        tutorialScreen.gameObject.SetActive(false);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        //gamePaused = false;
        creditsScreen.gameObject.SetActive(false);
        pauseScreen.gameObject.SetActive(false);
        tutorialScreen.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public void DemoCredits()
    {
        //gamePaused = true;
        creditsScreen.gameObject.SetActive(true);
        pauseScreen.gameObject.SetActive(false);
        tutorialScreen.gameObject.SetActive(false);
        Time.timeScale = 0f;
    }

    public void HowToPlay()
    {
        //gamePaused = true;
        tutorialScreen.gameObject.SetActive(true);
        pauseScreen.gameObject.SetActive(false);
        creditsScreen.gameObject.SetActive(false);
        Time.timeScale = 0f;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
