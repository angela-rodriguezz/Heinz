using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewStoryScene", menuName = "Data/New Story Scene")]
[System.Serializable]
public class Scenes : GameScene
{
    public string sceneName;
    public List<Sentence> sentences;
    public Sprite background;
    public GameScene nextScene;

    [System.Serializable]
    public struct Sentence
    {
        public string text;
        public Speakers speaker;
        public AudioClip voiceline;
        public Sprite BG;
    }
}

public class GameScene : ScriptableObject
{

}

