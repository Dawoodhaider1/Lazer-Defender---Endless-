using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float width = 5f;
    public float height = 5f;
    public float speed = 10.0f;
    public float padding = 1f;
    public float SpawnDelay = 1f;


    float left;
    float right;

    private bool EnemyMovingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
        left = leftmost.x + padding;
        right = rightmost.x - padding;
        SpawnUntilFull();


    }
    void SpawnEnemies()
    {
        foreach (Transform child in transform)
        {
            GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = child;
        }
    }

    void SpawnUntilFull()
    {
        Transform freePosition = NextFreePosition();
        if(freePosition)
        {
            GameObject enemy = Instantiate(enemyPrefab, freePosition.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = freePosition;
        }
        if(NextFreePosition())
        {
            Invoke("SpawnUntilFull", SpawnDelay);

        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
    }
    // Update is called once per frame
    void Update()
    {
        if (EnemyMovingRight)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        else
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }

        float rightofformation = transform.position.x + (0.1f * width);
        float leftofformation = transform.position.x - (0.1f * width);
        if (rightofformation < left)
        {
            EnemyMovingRight = true;
        }
        else if (leftofformation > right)
        {
            EnemyMovingRight = false;
        }

        if(AllMembersAreDead())
        {
            //call to respawn enemies
            SpawnUntilFull();
            Debug.Log("No Enemy left!");
        }


        bool AllMembersAreDead()
        {
            foreach(Transform childPositionGameObject in transform)
            {
                if(childPositionGameObject.childCount > 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
    Transform NextFreePosition()
    {
        foreach (Transform childPositionGameObject in transform)
        {
            if (childPositionGameObject.childCount == 0)
            {
                return childPositionGameObject;
            }
        }
        return null;
    }
}
