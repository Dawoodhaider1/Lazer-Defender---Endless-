using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController1 : MonoBehaviour
{
    public float speed = 10.0f;
    public float padding = 1f;
    public GameObject projectile;
    public float projectilespeed;
    public float firingrate = 0.5f;
    public float Health = 300f;
    public AudioClip PlayerFire;
    public AudioClip PlayerDeath;

    //private LevelManager levelManager;

    float left;
    float right;
    

    void Start()
    {
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
        left = leftmost.x + padding;
        right = rightmost.x - padding;

        //levelManager = GameObject.FindObjectOfType<LevelManager>();

    }

    void Fire()
    {
        Vector3 StartPosition = transform.position + new Vector3(0, 1, 0);
        GameObject laser = Instantiate(projectile, StartPosition, Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector3(0, projectilespeed, 0);
        AudioSource.PlayClipAtPoint(PlayerFire, transform.position);
    }

   
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            InvokeRepeating("Fire", 0.00001f, firingrate);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            CancelInvoke("Fire");
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //Both statements written down have the same functionality...
            //transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
            transform.position += Vector3.left * speed * Time.deltaTime;
            print("Left Arrow pressed !");
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            //Both statements written down have the same functionality...
            //transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
            transform.position += Vector3.right * speed * Time.deltaTime;
            print("Right Arrow pressed !");
        }

        float Area = Mathf.Clamp(transform.position.x, left, right);
        transform.position = new Vector3(Area, transform.position.y, transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        projectile missile = collision.gameObject.GetComponent<projectile>();
        
        if (missile)
        {
            Health -= missile.GetDamage();
            missile.Hit();
            if (Health <= 0)
            {
                Death();
            }
            Debug.Log("Bullet hits the Player");
        }
    }
    void Death()
    {
        LevelManager manager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        manager.LoadRequest("Lose");
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(PlayerDeath, transform.position);
    }
}

