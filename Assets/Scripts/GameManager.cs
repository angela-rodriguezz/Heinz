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
    [SerializeField] private float addedCharge = 2;
    [SerializeField] private float chargeLoss = (float)0.05;
    
    #endregion

    #region health
    public float curHealth;
    [SerializeField] private float maxHealth;
    [SerializeField] private Image healthBar;
    [SerializeField] private float healthIncrease = (float)0.1;
    #endregion

    #region bools/timers
    public bool canCharge;
    //[SerializeField] Animator iconAnim;
    [SerializeField] Sprite defIcon;
    [SerializeField] Sprite switchingIcon;
    [SerializeField] Sprite chargeIcon;
    [SerializeField] Image canChargeIcon;

    protected bool isSwitching = false;
    private float switchTimer = 0;
    [SerializeField] float switchTime = 2f;
    private float chargeTimer = 0;
    [SerializeField] float chargeTime = 0.25f;

    #endregion

    #region player stuffs
    [SerializeField] Animator playerAnim;
    [SerializeField] Animator enemyAnim;
    [SerializeField] private Perry perry;
    #endregion



    // Start is called before the first frame update
    void Start()
    {
        canCharge = false;
        curHealth = maxHealth;
        UpdateCanCharge();
    }

    // Update is called once per frame
    void Update()
    {
        //checks
        if (curHealth < 100)
        {
            curHealth += healthIncrease * Time.deltaTime;
            SetHealth(curHealth);
        }
        if (curHealth <= 0)
        {
            Die();
        }
        if (chargeLevel >= maxCharge)
        {
            Win();
        }


        //input
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Switch();
        }
        if (Input.GetKeyDown(KeyCode.Space) && canCharge)
        {
            
            Charge();
        }
        if (chargeLevel > 0)
        {
            chargeLevel -= chargeLoss * Time.deltaTime;
            AddBarCharge();
        }

        if (switchTimer > 0)
        {
            switchTimer -= Time.deltaTime;
        }
        if (chargeTimer > 0)
        {
            chargeTimer -= Time.deltaTime;
        }
        UpdateCanCharge();
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

    void UpdateCanCharge()
    {
        
        if (canChargeIcon != null)
        {
            if (isSwitching == true)
            {
                canChargeIcon.sprite = switchingIcon;
                return;
            }
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

    void Charge()
    {
        if (chargeTimer <= 0)
        {
            Debug.Log("CHARGE");
            chargeTimer = chargeTime;
            StartCoroutine("ChargeRoutine");
            
        }
    }

    void Switch()
    {
        if (switchTimer <= 0)
        {
            isSwitching = true;
            Debug.Log("SWITCH");
            UpdateCanCharge();
            StartCoroutine("SwitchRoutine");
            switchTimer = switchTime;
            isSwitching = false;
            UpdateCanCharge();
        }
    }

    public IEnumerator ChargeRoutine()
    {

        // anim
        if (playerAnim != null)
        {
            playerAnim.SetTrigger("Crank");
        }
        // sound
        //wait
        yield return new WaitForSeconds(chargeTime);

        // action
        chargeLevel += addedCharge;
        AddBarCharge();
        // wait seconds
        
        yield return null;
    }

    public IEnumerator SwitchRoutine()
    {
        Debug.Log("SWITCH ROUTINE");
        // anim
        if (playerAnim != null)
        {
            playerAnim.SetBool("Walking", true);
        }
        // sound

        // action
        UpdateCanCharge();
        if (canCharge == false)
        {
            canCharge = true;
            if (playerAnim != null)
            {
                playerAnim.SetFloat("WalkDir", -1);
            }
            // wait seconds
            yield return new WaitForSeconds(0.1f);
            if (playerAnim != null)
            {
                playerAnim.SetBool("CanCharge", true);
                playerAnim.SetBool("Walking", false);
            }
        }
        else if (canCharge == true)
        {
            canCharge = false;
            if (playerAnim != null)
            {
                playerAnim.SetFloat("WalkDir", 1);
            }
            // wait seconds
            yield return new WaitForSeconds(0.1f);
            if (playerAnim != null)
            {
                playerAnim.SetBool("CanCharge", false);
                playerAnim.SetBool("Walking", false);
            }
        }

        yield return null;
    }

    //straight up lose.
    public void Die()
    {
        //WIP
        return;
    }

    //starts the final ending sequence.
    public void Win()
    {
        //WIP
        Debug.Log("FULLY CHARGED");
        return;
    }

    //starts the tutorial sequence.
    void TutorialCards()
    {

    }

    #region Perry Animations

    public IEnumerator AnimActivate()
    {
        yield return new WaitForSeconds(0.1f);

    }

    public void ThrowBomb()
    {
        if (perry.timetoShoot > 0)
        {
            enemyAnim.SetBool("Throwing", false);
        }
        else
        {
            enemyAnim.SetBool("Throwing", true);
            AnimActivate();
        }
    }

    void AgentHurt()
    {

    }
    #endregion
}
