using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] Text livesText;
    [SerializeField] Text scoreText;
    [SerializeField] int score = 0;
    public int amount = 100;
    private void Awake()
    {
        int numGameSession = FindObjectsOfType<GameSession>().Length;
        if(numGameSession> 1)
        {
            Destroy(gameObject);

        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    void Start()
    {
        livesText.text = playerLives.ToString();
        scoreText.text = score.ToString();
      
    }
    public void ProcessPlayerDeath()
    {
        if(playerLives > 1)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();
        }
    }

    private void ResetGameSession()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
    private void TakeLife()
    {
        playerLives -- ;
        var currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
        livesText.text = playerLives.ToString();
    }
    public void DispayScore(int amount)
    {
        score += amount;

        scoreText.text = score.ToString();
    }
}
