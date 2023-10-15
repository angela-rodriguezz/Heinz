using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadManager : MonoBehaviour
{
    private static LoadManager instance;
    //[SerializeField]
    //AudioManager audioManager;
    [SerializeField]
    public Animator transition;
    float transitionTime = 1f;
    float transitionTimer = 0;
    bool waiting = false;


    #region Unity_functions
    private void Start()
    {
        //transition = GetComponent<Animator>();
        //audioManager = FindObjectOfType<AudioManager>();
        transitionTimer = transitionTime;
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);

        }
        else if (instance.gameObject != gameObject)
        {
            Destroy(gameObject);
            return;
        }

    }
    #endregion

    #region Scene_transitions
    public void StartGame()
    {
        if (!waiting)
        {
            //transition.SetTrigger("Fade_Out");
            FakeWaitForSeconds();
            SceneManager.LoadScene("Intro");
        }

    }

    public void LoseGame()
    {
        if (!waiting)
        {
            //transition.SetTrigger("Fade_Out");
            FakeWaitForSeconds();
            SceneManager.LoadScene("LoseScreen");
        }
    }

    public void MainMenu()
    {
        if (!waiting)
        {
            //transition.SetTrigger("Fade_Out");
            FakeWaitForSeconds();
            //UIManager.instance.UpdateScore(0);
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void Credits()
    {
        if (!waiting)
        {
            //transition.SetTrigger("Fade_Out");
            FakeWaitForSeconds();
            SceneManager.LoadScene("Credits");
        }
    }

    public void Tutorial()
    {
        if (!waiting)
        {
            //transition.SetTrigger("Fade_Out");
            FakeWaitForSeconds();
            SceneManager.LoadScene("Tutorial");
        }
    }

    /*
    private IEnumerator transRoutine(string SceneName)
    {
        
        transition.SetTrigger("Fade_Out");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(SceneName);
    }
    */

    private void FakeWaitForSeconds()
    {
        waiting = true;
        while (transitionTimer > 0)
        {
            transitionTimer -= Time.deltaTime;
        }
        transitionTimer = transitionTime;
        waiting = false;
    }

    public void Quit()
    {
        Application.Quit();
    }
    #endregion
}
