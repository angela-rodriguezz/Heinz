using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;


public class PlayDialogue : MonoBehaviour
{
    [SerializeField] private GameScene currentScene;
    [SerializeField] private TextMeshProUGUI barText, personNameText;
    [SerializeField] private Scenes currScene;
    [SerializeField] private AudioSource voice;
    [SerializeField] private Animator animator;
    private int sentenceIndex = -1;
    private StateTwo state2 = StateTwo.COMPLETED;
    private IEnumerator lineAppear;
    public bool finished;
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
            PlayScene(storyScene);
            StartImage(storyScene.background);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentScene is not ChooseScene && gameOver != true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (IsCompleted() && IsLastSentence() && IsFinalScene())
                {
                    StartCoroutine(EnterLoad());
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
                else if (IsCompleted() && !voice.isPlaying)
                {
                    if (state == State.IDLE && IsLastSentence())
                    {
                        PlayScene((currentScene as Scenes).nextScene);
                    }
                    else
                    {
                        PlayNextSentence();
                    }
                }
                else
                {
                    finished = true;
                    voice.Stop();
                    FinishSentence();
                }

            }
        }
    }

    #region Decides Scene Type

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
            if (CheckImage(storyScene.background) && IsLastSentence())
            {
                SwitchImage(storyScene.background);
                yield return new WaitForSeconds(1f);
            }

            PlayScene(storyScene);
            state = State.IDLE;
        }
        else if (scene is ChooseScene)
        {
            state = State.CHOOSE;
            chooseController.SetupChoose(scene as ChooseScene);
        }
    }

    #endregion

    #region Shows Dialogue

    private enum StateTwo
    {
        PLAYING, COMPLETED
    }

    public void ClearText()
    {
        barText.text = "";
    }

    // plays the current scene and starts playing the next sentence
    // starts negative since we are adding each time
    public void PlayScene(Scenes scene)
    {
        currentScene = scene;
        currScene = scene;
        sentenceIndex = -1;
        PlayNextSentence();
    }

    public bool IsCompleted()
    {
        return state2 == StateTwo.COMPLETED;
    }

    public bool IsLastSentence()
    {
        Debug.Log(sentenceIndex);
        Debug.Log(currScene.sentences.Count);
        Debug.Log(finished);
        return sentenceIndex + 1 == currScene.sentences.Count;
    }

    public bool IsFinalScene()
    {
        return currScene.nextScene == null;
    }

    // If the player double clicks, the state is now complete, the coroutine stops, and now the text immediately shows the sentence immediately
    public void FinishSentence()
    {
        state2 = StateTwo.COMPLETED;
        StopCoroutine(lineAppear);
        finished = false;
        barText.text = currScene.sentences[sentenceIndex].text;

    }

    // unless there is no next sentence in the scene and the text is completed, this function plays the next sentence
    // we get one of the sentences in the list from the current sentence index and enter it into the coroutine
    // change the player name or color if the speaker changes
    public void PlayNextSentence()
    {
        
        if (!IsLastSentence() && finished == false)
        {
            lineAppear = TypeText(currScene.sentences[++sentenceIndex].text);
            if (currScene.sentences[sentenceIndex].voiceline != null)
            {
                voice.clip = currScene.sentences[sentenceIndex].voiceline;
                voice.Play();
            }
            if (CheckImage(currScene.sentences[sentenceIndex].BG))
            {
                SetImage(currScene.sentences[sentenceIndex].BG);
            }
            StartCoroutine(lineAppear);
            personNameText.text = currScene.sentences[sentenceIndex].speaker.speakerName;
            personNameText.color = currScene.sentences[sentenceIndex].speaker.textColor;
        }
    }

    // designates that the dialogue is playing and sets the text to empty 
    // starts the index at 0 and continues until the state is complete
    // adds each letter into the text box from the intended string to be returned
    // yield return changes timing of when text appears
    // if the index ever equals the text length, the state is now completed and the enumerator ends
    private IEnumerator TypeText(string text)
    {
        barText.text = "";
        state2 = StateTwo.PLAYING;
        int wordIndex = 0;

        while (state2 != StateTwo.COMPLETED)
        {
            barText.text += text[wordIndex];
            yield return new WaitForSeconds(0.05f);
            if (++wordIndex == text.Length)
            {
                state2 = StateTwo.COMPLETED;
                break;
            }
        }
    }

    #endregion

    #region Transitions Backgrounds
    public bool isSwitched = false;
    public Image background1;

    // checks if the current scene's background isn't null
    public bool CheckImage(Sprite sprite)
    {
        return sprite != null;
    }
    // starts changing the backgrounds
    public void SwitchImage(Sprite sprite)
    {
        SetImage(sprite);
    }

    public void StartImage(Sprite sprite)
    {
        background1.sprite = sprite;
    }
    // if the background isn't switched...
    // the background sprite is changed to the new background, the fading animation begins, and is now switched
    // if the background is switched
    // change back to the previous background, reverse fading animation begins, and is now not switched
    public void SetImage(Sprite sprite)
    {
            background1.sprite = sprite;
            //animator.SetTrigger("BGShow");
            //isSwitched = !isSwitched;
        
    }
    #endregion
}
