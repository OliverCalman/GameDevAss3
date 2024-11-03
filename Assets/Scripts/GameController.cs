using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private GameObject scoreUI;
    private int score = 0;
    [SerializeField] private GameObject fearTimerUI;
    [SerializeField] private GameObject gameTimerUI;
    private float gameTime;
    private float fearTime = 11f;
    private int lives = 3;
    [SerializeField] private GameObject health1;
    [SerializeField] private GameObject health2;
    [SerializeField] private GameObject health3;
    public bool scaredState {get; private set;} 
    public bool recoveryState {get; private set;} 
    public int pelletCount = 224;
    // Start is called before the first frame update
    void Start()
    {
        //nullify the scared timer to remove it
        fearTimerUI.GetComponent<Text>().text = null;
        audioManager.GetComponent<AudioManager>();
    }
    // Update is called once per frame
    void Update()
    {
        gameTime = gameTime += Time.deltaTime;

        if (scaredState == true && fearTime > 0)
        {
            int fminutes = Mathf.FloorToInt(fearTime / 60F);
            int fseconds = Mathf.FloorToInt(fearTime - fminutes * 60);
            string fdisplayTime = string.Format("{0:0}:{1:00}", fminutes, fseconds);
            fearTimerUI.GetComponent<Text>().text = "Scared: " + fdisplayTime;
            fearTime = fearTime -= Time.deltaTime;
        }

        int minutes = Mathf.FloorToInt(gameTime / 60F);
        int seconds = Mathf.FloorToInt(gameTime - minutes * 60);
        string displayTime = string.Format("{0:0}:{1:00}", minutes, seconds);
        gameTimerUI.GetComponent<Text>().text = "Time: " + displayTime;

        //check if game is over
        GameOver();
    }
    public void KeepScore(int addScore)
    {
        //update current score
        score += addScore;
        //display on UI
        scoreUI.GetComponent<Text>().text = "Score: " + score;
    }
    public void RemovePellet()
    {
        pelletCount--;
    }
    public void RemoveLife()
    {
        lives--;
        switch (lives)
        {
            //double destruction just to be safe...
            case 2:
                // set health3 to invisible
                Destroy(health3);
                break;
            case 1: 
                //set health3 and health2 to invisible
                Destroy(health3);
                Destroy(health2);
                break;
            case 0:
                //set health3, health2, and health1 to invsible
                Destroy(health3);
                Destroy(health2);
                Destroy(health1);
                break;
        }

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
       scaredState = true;
    }
    IEnumerator ScaredTimer()
    {
        audioManager.ScareGhosts();
        //set scared state for 10 seconds
        Debug.Log("scared state");
        scaredState = true;
        yield return new WaitForSeconds(7f);
        //start recovery state
        recoveryState = true;
        yield return new WaitForSeconds(3f);
        recoveryState = true;        
        scaredState = false;
        //remove text
        fearTimerUI.GetComponent<Text>().text = null;
        //reset timer
        fearTime = 10f;
    }
    public void GameOver()
    {
        if (pelletCount == 0)
        {
            //end game
        }
        if (lives <= 0)
        {
            //end game if lives == 0
        }
    }
}
