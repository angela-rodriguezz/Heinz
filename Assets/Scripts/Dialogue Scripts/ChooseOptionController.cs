using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChooseOptionController : MonoBehaviour
{
    public Color defaultColor = Color.white;
    public Color hoverColor;
    private Scenes scene;
    public TextMeshProUGUI textMesh;
    public SelectionScreen controller;
    public Button btn1;
    public Button btn2;

    // Start is called before the first frame update
    void Awake()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        textMesh.color = defaultColor;
        
    }

    void Start()
    {
        btn1.onClick.AddListener(DecisionOne);
        btn2.onClick.AddListener(DecisionTwo);
    }

    public void DecisionOne()
    {
        controller.PerformChoice(0);
    }

    public void DecisionTwo()
    {
        controller.PerformChoice(1);
    }
}