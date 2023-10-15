using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region charge
    [SerializeField] private Image chargeBar;
    [SerializeField] private float maxCharge;
    private float chargeLevel = 0;
    [SerializeField] private float addedCharge = 1;
    [SerializeField] private float chargeLoss = (float)0.2;
    
    #endregion

    #region health
    public float curHealth;
    [SerializeField] private float maxHealth;
    [SerializeField] private Image healthBar;
    [SerializeField] private float healthIncrease = (float)0.1;
    #endregion

    #region bools
    [SerializeField] public bool canCharge;
    //[SerializeField] Animator iconAnim;
    [SerializeField] Sprite defIcon;
    [SerializeField] Sprite chargeIcon;
    [SerializeField] Image canChargeIcon;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        canCharge = false;
        curHealth = maxHealth;
        UpdateCanChargeIcon();
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
            UpdateCanChargeIcon();
        }
        else if (Input.GetKeyDown(KeyCode.Q) && canCharge == true)
        {
            canCharge = false;
            UpdateCanChargeIcon();
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

    void UpdateCanChargeIcon()
    {
        if (canChargeIcon != null)
        {
            if (canCharge == false)
            {
                canChargeIcon.sprite = defIcon;
            }
            if (canCharge == true)
            {
                canChargeIcon.sprite = chargeIcon;
            }
        }
        
    }
}
