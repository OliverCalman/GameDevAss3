using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject scoreUI;
    private int score = 0;
    [SerializeField] private GameObject fearTimerUI;
    [SerializeField] private GameObject gameTimerUI;
    private float gameTime;
    [SerializeField] private GameObject health1;
    [SerializeField] private GameObject health2;
    [SerializeField] private GameObject health3;
    public bool scaredState { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        //nullify the scared timer to remove it
        gameTimerUI.GetComponent<Text>().text = null;
    }

    // Update is called once per frame
    void Update()
    {
        gameTime = gameTime += Time.deltaTime;

        int minutes = Mathf.FloorToInt(gameTime / 60F);
        int seconds = Mathf.FloorToInt(gameTime - minutes * 60);
        string displayTime = string.Format("{0:0}:{1:00}", minutes, seconds);
        gameTimerUI.GetComponent<Text>().text = "Time: " + displayTime;
    }

    public void KeepScore(int addScore)
    {
        //update current score
        score += addScore;
        //display on UI
        scoreUI.GetComponent<Text>().text = "Score: " + score;
    }
    public void StartGameTimer()
    {
        Time.timeScale = 1.0f;
    }
    public void PauseGameTimer()
    {
        Time.timeScale = 0.0f;
    }
    public void ScareGhosts()
    {
        StartCoroutine(ScaredTimer());
    }
    IEnumerator ScaredTimer()
    {
        Debug.Log("scared state");
        //make sure that setting the animation and state here does NOT affect dead ghosts
        //if ghost == alive only

        scaredState = true;
        //start 10 second timer and display UI component

        //scared for 7 seconds
        yield return new WaitForSeconds(10f);
        scaredState = false;
    }
}
