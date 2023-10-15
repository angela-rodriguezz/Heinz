using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Transition : MonoBehaviour
{
    public bool isSwitched = false;
    public Image background1;
    public Image background2;

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
        if (!isSwitched)
        {
            background2.sprite = sprite;
            isSwitched = !isSwitched;
        }
        else if (isSwitched)
        {
            background1.sprite = sprite;
            isSwitched = !isSwitched;
        }
    }
}

