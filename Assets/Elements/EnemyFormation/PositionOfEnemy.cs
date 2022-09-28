using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionOfEnemy : MonoBehaviour
{
    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 1);
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
    }
}
