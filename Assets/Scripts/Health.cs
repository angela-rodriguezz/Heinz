using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float curHealth;
    [SerializeField] private float maxHealth;
    [SerializeField] private Image healthBar;
    [SerializeField] private float healthIncrease = (float) 0.1;
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
    }

    public void SetHealth(float health)
    {
        healthBar.fillAmount = Math.Clamp(health / maxHealth, 0, 1);
        healthBar.color = Color.Lerp(Color.red, Color.green, health / maxHealth);

    }

}
