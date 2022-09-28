using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Text mytext = GetComponent<Text>();
        mytext.text = ScoreKeeper.score.ToString();
        ScoreKeeper.Reset();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
