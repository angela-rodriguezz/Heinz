using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;


public class Perry : MonoBehaviour
{
    [SerializeField] private Transform drDoof;
    [SerializeField] private GameObject item;
    [SerializeField] private GameManager functioner;


    #region Shooting Mechanic Variables
    public float timetoShoot;
    public float startTimeShot;
    [SerializeField] float randomTime = 1.0f;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        drDoof = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        functioner.ThrowBomb();
        if (timetoShoot <= 0)
        {
            GameObject projectile = Instantiate(item, transform.position, Quaternion.identity);
            //projectile.SetActive(true);
            timetoShoot = startTimeShot + Random.Range(-randomTime/2, + 2 * randomTime);
        } else
        {
            timetoShoot -= Time.deltaTime;
        }
    }

    

    void Stun()
    {

    }

    IEnumerator StunRoutine()
    {
        yield return null;
    }
}
