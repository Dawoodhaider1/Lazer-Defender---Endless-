using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private PlayerController1 paddle;
    private Vector3 PaddletoBallVector;
    private bool BallMoving = false;

    public AudioSource aud;

    // Start is called before the first frame update
    void Start()
    {
        paddle = GameObject.FindObjectOfType<PlayerController1>();
        PaddletoBallVector = this.transform.position - paddle.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!BallMoving)
        {
            this.transform.position = paddle.transform.position + PaddletoBallVector;
            if (Input.GetMouseButtonDown(0))
            {
                print("Mouse button clicked !");
                BallMoving = true;
                this.transform.GetComponent<Rigidbody2D>().velocity = new Vector2(-2f, 10f);
            }
        }
        
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 tweak = new Vector2(Random.Range(0f, 0.2f), Random.Range(0f, 0.2f));
        if (BallMoving)
        {
                         //both ways of using an audio is right !!
            aud.Play();  //for this we have used a public method named aud
                         // GetComponent<AudioSource>().Play();
            GetComponent<Rigidbody2D>().velocity += tweak;
        }
    }
}
