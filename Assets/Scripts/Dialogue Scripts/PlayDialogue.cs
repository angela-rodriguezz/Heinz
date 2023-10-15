using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayDialogue : MonoBehaviour
{
    [SerializeField] private GameScene currentScene;
    [SerializeField] private DialogueManager bottomBar;
    public Transition backgroundController;
    public SelectionScreen chooseController;
    public static bool gameOver = false;

    private State state = State.IDLE; // sets the current scene to a regular dialogue scene

    // idle state: the regular dialogue playing 
    // animate state: the scene transitioning to a new background
    // choose state: the scene changing to a choosing dialogue prompt
    private enum State
    {
        IDLE, ANIMATE, CHOOSE
    }
    // Start is called before the first frame update
    void Start()
    {
        if (currentScene is Scenes)
        {
            Scenes storyScene = currentScene as Scenes;
            bottomBar.PlayScene(storyScene);
            backgroundController.StartImage(storyScene.background);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentScene is not ChooseScene && gameOver != true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (bottomBar.IsCompleted() && bottomBar.IsLastSentence() && bottomBar.IsFinalScene())
                {
                    StartCoroutine(EnterLoad());
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
                else if (bottomBar.IsCompleted())
                {
                    if (state == State.IDLE && bottomBar.IsLastSentence())
                    {
                        PlayScene((currentScene as Scenes).nextScene);
                    }
                    else
                    {
                        bottomBar.PlayNextSentence();
                    }

                }
                else
                {
                    bottomBar.finished = true;
                    bottomBar.FinishSentence();
                }

            }
        }
    }

    // wait until loading next level
    private IEnumerator EnterLoad()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.05f);
            break;
        }

    }

    // starts switching to a new background scene
    public void PlayScene(GameScene scene)
    {
        StartCoroutine(SwitchScene(scene));
    }

    // sets the state to animate
    // sets the currentscene to given scene
    // if the scene is not a choose scene...
    // if the given scene's background isn't null and there is no more sentences in scene...
    // switches the backgrounds and waits until returning
    // plays the scene's dialogue and sets the state to idle
    // if the scene is a choose scene...
    // sets up the choice animations, changes the text of buttons to choices, and starts timer
    private IEnumerator SwitchScene(GameScene scene)
    {
        state = State.ANIMATE;
        currentScene = scene;
        if (scene is Scenes)
        {
            Scenes storyScene = scene as Scenes;
            if (backgroundController.CheckImage(storyScene.background) && bottomBar.IsLastSentence())
            {
                backgroundController.SwitchImage(storyScene.background);
                yield return new WaitForSeconds(1f);
            }

            bottomBar.PlayScene(storyScene);
            state = State.IDLE;
        }
        else if (scene is ChooseScene)
        {
            state = State.CHOOSE;
            chooseController.SetupChoose(scene as ChooseScene);
        }
    }
}
