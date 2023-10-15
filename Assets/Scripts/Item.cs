using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform player;
    private Vector2 target;
    [SerializeField] private float damage = 5;
    [SerializeField] private GameManager doofHealth;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(player.position.x, player.position.y + 4);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (transform.position.x == target.x && transform.position.y == target.y)
        {
            doofHealth.curHealth -= damage;
            DestroyItem();
        }
    }

    #region On Contact With Player

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DestroyItem();
        }
    }

    void DestroyItem()
    {
        doofHealth.curHealth -= damage;
        doofHealth.SetHealth(doofHealth.curHealth);
        Destroy(gameObject);
    }

    #endregion
}
