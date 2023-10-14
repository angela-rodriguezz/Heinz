using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaserCharge : MonoBehaviour
{
    [SerializeField] private Image chargeBar;
    [SerializeField] private float maxCharge;
    private float chargeLevel = 0;
    [SerializeField] private float addedCharge = 1;
    [SerializeField] private float chargeLoss = (float) 0.2;
    private bool canCharge;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && canCharge == false)
        {
            canCharge = true;
        } else if (Input.GetKeyDown(KeyCode.Q) && canCharge == true)
        {
            canCharge = false;
        }
        if (Input.GetKeyDown(KeyCode.Space) && canCharge)
        {
            chargeLevel += addedCharge;
            AddBarCharge();
        }
        if (chargeLevel > 0)
        {
            chargeLevel -= chargeLoss * Time.deltaTime;
            AddBarCharge();
        }
    }

    void AddBarCharge()
    {
        chargeBar.fillAmount = Math.Clamp(chargeLevel / maxCharge, 0, 1);
        chargeBar.color = Color.Lerp(Color.red, Color.yellow, chargeLevel / maxCharge);
    }
}
