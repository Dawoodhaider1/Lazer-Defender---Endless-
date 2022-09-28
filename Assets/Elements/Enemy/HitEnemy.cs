using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEnemy : MonoBehaviour
{
    public GameObject projectile;
    public float Health = 200f;
    public float projectilespeed = 10f;
    public float firingrate = 0.5f;
    public float ShotsPerSecond = 0.5f;
    public int scoreValue = 200;
    public AudioClip AudioFire;
    public AudioClip AudioDeath;


    private ScoreKeeper scoreKeeper;
    private void Start()
    {
        scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
    }

    private void Update()
    {
        float FireRate = Time.deltaTime * ShotsPerSecond;
        if(Random.value < FireRate)
        {
            EnemyFire();
        }
    }

    void EnemyFire()
    {
        Vector3 StartPosition = transform.position + new Vector3(0, -1, 0);
        GameObject EnemyLaser = Instantiate(projectile, StartPosition, Quaternion.identity) as GameObject;
        EnemyLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectilespeed);
        AudioSource.PlayClipAtPoint(AudioFire, transform.position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        projectile missile = collision.gameObject.GetComponent<projectile>();
        
        if(missile)
        {
            Health -= missile.GetDamage();
            missile.Hit();
            if (Health <= 0)
            {
                Destroy(gameObject);
                scoreKeeper.Score(scoreValue);
                AudioSource.PlayClipAtPoint(AudioDeath, transform.position);
            }
            Debug.Log("Bullet hits the Enemy");
        }
    }
}
