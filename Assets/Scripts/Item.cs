using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using Random = UnityEngine.Random;

public class Item : MonoBehaviour
{
    public GameObject explosionPrefab;
    [SerializeField] private float speed;
    [SerializeField] private Transform player;
    private Vector2 target;
    [SerializeField] private float damage = 20;
    [SerializeField] private GameManager doofHealth;
    [SerializeField] private AudioSource perrysfx;
    [SerializeField] private AudioClip audio1;
    [SerializeField] private AudioClip audio2;
    private int rand_num;
    // Start is called before the first frame update




    //physics vars
    public Rigidbody2D rb2D;
    private float horizontalBound = 5f;
    private float verticalBound = 5f;
    private float rotationBound = 0f;
    [SerializeField]
    [Tooltip("Control thrust")]
    private float thrust = 750f;
    private float thrustMultiplier = 0.6f;

    void Start()
    {
        //set a random velocity in the direction of the player
        //transform.position = new Vector2(Random.Range(-horizontalBound, horizontalBound), Random.Range(-verticalBound, -6.0f));
        transform.Rotate(0.0f, 0.0f, Random.Range(90, rotationBound), Space.Self);
        Vector2 force = transform.up * (thrust * Random.Range(thrustMultiplier, 2 * thrustMultiplier));
        rb2D.AddForce(force);
        doofHealth = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        
    }

    #region On Contact With Player

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("FLOOR"))
        {
            DestroySelf();
        }
        if (other.CompareTag("Player"))
        {
            rand_num = Random.Range(0, 2);
            AudioChoose(rand_num);
            DamagePlayer();
        }
    }

    void AudioChoose(int number)
    {
        if (number == 1)
        {
            perrysfx.clip = audio1;
            perrysfx.Play();
        }
        else if (number == 2)
        {
            perrysfx.clip = audio2;
            perrysfx.Play();
        }
    }

    void DamagePlayer()
    {
        doofHealth.curHealth -= damage;
        doofHealth.SetHealth(doofHealth.curHealth);
        DestroySelf();
    }

    void DestroySelf()
    {
        //instantiate explosion
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        //destroy self
        Destroy(gameObject);
    }

    #endregion
}
