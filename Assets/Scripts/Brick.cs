using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public Sprite[] hitSprites;
    public static int BreakableCount = 0;
    public AudioClip crack;
    public GameObject Smoke;

    private int HitNo;
    private LevelManager levelManager;
    private bool isBreakable; 
    // Start is called before the first frame update
    void Start()
    {
        isBreakable = (this.tag == "BreakableBricks");
        if (isBreakable)
        {
            BreakableCount++;
            print(BreakableCount);
        }
        HitNo = 0;
        levelManager = GameObject.FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void LoadSprites()
    {
        int SpriteHit = HitNo - 1;
        if (hitSprites[SpriteHit])
        {
            this.GetComponent<SpriteRenderer>().sprite = hitSprites[SpriteHit];
        }
    }

    void SimulateWin()
    {
        levelManager.LoadNextRequest();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        AudioSource.PlayClipAtPoint(crack, transform.position);
        if (isBreakable)
        {
            HandleCollisions();
        }
    }

    void HandleCollisions ()
    {
        HitNo++;
        int MaxHit = hitSprites.Length + 1;
        if (HitNo == MaxHit)
        {
            BreakableCount--;
            print(BreakableCount);
            levelManager.BrickDestroyed();
            Destroy(gameObject);
            GameObject SmokePuff = Instantiate(Smoke, transform.position, Quaternion.identity) as GameObject;
            GetComponent<ParticleSystem>().startColor = gameObject.GetComponent<SpriteRenderer>().color;
        }
        else
        {
            LoadSprites();
        }
    }
}