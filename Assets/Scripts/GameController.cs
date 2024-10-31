using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private int score = 0;
    [SerializeField] private GameObject scoreUI;
    [SerializeField] private GameObject fearTimerUI;
    [SerializeField] private GameObject gameTimerUI;
    [SerializeField] private GameObject health1;
    [SerializeField] private GameObject health2;
    [SerializeField] private GameObject health3;
    private bool scaredState;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void KeepScore(int addScore)
    {
        //update current score
        score += addScore;
        //display on UI
        scoreUI.GetComponent<Text>().text = "Score:" + score;
    }
}
