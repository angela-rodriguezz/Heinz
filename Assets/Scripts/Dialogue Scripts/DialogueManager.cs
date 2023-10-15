using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI barText, personNameText;
    [SerializeField] private Scenes currentScene;
    private int sentenceIndex = -1;
    private State state = State.COMPLETED;
    private IEnumerator lineAppear;
    public bool finished;

    private enum State
    {
        PLAYING, COMPLETED
    }

    private void Start()
    {

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
        sentenceIndex = -1;
        PlayNextSentence();
    }

    public bool IsCompleted()
    {
        return state == State.COMPLETED;
    }

    public bool IsLastSentence()
    {
        return sentenceIndex + 1 == currentScene.sentences.Count;
    }

    public bool IsFinalScene()
    {
        return currentScene.nextScene == null;
    }

    // If the player double clicks, the state is now complete, the coroutine stops, and now the text immediately shows the sentence immediately
    public void FinishSentence()
    {
        state = State.COMPLETED;
        StopCoroutine(lineAppear);
        finished = false;
        barText.text = currentScene.sentences[sentenceIndex].text;

    }

    // unless there is no next sentence in the scene and the text is completed, this function plays the next sentence
    // we get one of the sentences in the list from the current sentence index and enter it into the coroutine
    // change the player name or color if the speaker changes
    public void PlayNextSentence()
    {
        if (!IsLastSentence() && !finished)
        {
            lineAppear = TypeText(currentScene.sentences[++sentenceIndex].text);
            StartCoroutine(lineAppear);
            personNameText.text = currentScene.sentences[sentenceIndex].speaker.speakerName;
            personNameText.color = currentScene.sentences[sentenceIndex].speaker.textColor;
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
        state = State.PLAYING;
        int wordIndex = 0;

        while (state != State.COMPLETED)
        {
            barText.text += text[wordIndex];
            yield return new WaitForSeconds(0.05f);
            if (++wordIndex == text.Length)
            {
                state = State.COMPLETED;
                break;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
