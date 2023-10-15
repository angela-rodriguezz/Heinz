using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Image chargeBar;
    [SerializeField] private float maxCharge;
    private float chargeLevel = 0;
    [SerializeField] private float addedCharge = 1;
    [SerializeField] private float chargeLoss = (float)0.2;
    private bool canCharge;

    public float curHealth;
    [SerializeField] private float maxHealth;
    [SerializeField] private Image healthBar;
    [SerializeField] private float healthIncrease = (float)0.1;

    // Start is called before the first frame update
    void Start()
    {
        curHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (curHealth < 100)
        {
            curHealth += healthIncrease * Time.deltaTime;
            SetHealth(curHealth);
        }
        if (Input.GetKeyDown(KeyCode.Q) && canCharge == false)
        {
            canCharge = true;
        }
        else if (Input.GetKeyDown(KeyCode.Q) && canCharge == true)
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
        chargeBar.color = Color.Lerp(new Color(1.0f, 0.64f, 0.0f), Color.yellow, chargeLevel / maxCharge);
    }

    public void SetHealth(float health)
    {
        healthBar.fillAmount = Math.Clamp(health / maxHealth, 0, 1);
        healthBar.color = Color.Lerp(Color.red, Color.green, health / maxHealth);

    }
}
