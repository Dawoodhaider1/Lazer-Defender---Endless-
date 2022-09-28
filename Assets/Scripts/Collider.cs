using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collider : MonoBehaviour
{
    private LevelManager levelManager;
    void OnTriggerEnter2D (Collider2D trigger)
    {
        levelManager = GameObject.FindObjectOfType<LevelManager>();
        levelManager.LoadRequest("Lose");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("Collider called !");
    }
}
