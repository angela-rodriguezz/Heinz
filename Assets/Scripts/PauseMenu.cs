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

    // Start is called before the first frame update
    void Start()
    {
        
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
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        //gamePaused = false;
        creditsScreen.gameObject.SetActive(false);
        pauseScreen.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public void DemoCredits()
    {
        //gamePaused = true;
        creditsScreen.gameObject.SetActive(true);
        pauseScreen.gameObject.SetActive(false);
        Time.timeScale = 0f;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
