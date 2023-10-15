using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Perry : MonoBehaviour
{
    [SerializeField] private Transform drDoof;
    [SerializeField] private GameObject item;

    #region Shooting Mechanic Variables
    private float timetoShoot;
    public float startTimeShot;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        drDoof = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (timetoShoot <= 0)
        {
            GameObject projectile = Instantiate(item, transform.position, Quaternion.identity);
            projectile.SetActive(true);
            timetoShoot = startTimeShot;
        } else
        {
            timetoShoot -= Time.deltaTime;
        }
    }
}
