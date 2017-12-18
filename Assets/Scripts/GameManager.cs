using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public int score = 0;
    public Text scoreText, livesText;
    public int lives = 3;

    private void Awake()
    {

        DontDestroyOnLoad(gameObject);
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        scoreText = GameObject.Find("Score").GetComponent<Text>();
        livesText = GameObject.Find("Lives").GetComponent<Text>();
    }
    public void OnLevelFinishedLoading()
    {

    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Game")
        {
            scoreText = GameObject.Find("Score").GetComponent<Text>();
            livesText = GameObject.Find("Lives").GetComponent<Text>();
        }
    }

    
    public void AddPoint()
    {
        score++;
    }
    public int GetScore()
    {
        return score;
    }
    public int GetLives()
    {
        return lives;
    }
    public void TakeLife()
    {
        lives--;
        SceneManager.LoadScene("Game");
    }


    private void Update()
    {
        scoreText.text = "Punkty : " + score;
        livesText.text = "Życia : " + lives;
        if (GetLives() == 0)
        {
            SceneManager.LoadScene("LoseScreen");
        }
        if (Input.GetKey(KeyCode.Escape)) Application.Quit();
    }
}
