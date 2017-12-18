using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HighScores : MonoBehaviour
{
    //Skrypt zarządzający najwyższymi wynikami. Jeśli wynik gracza jest wyższy niż dotychczasowy rekord zostaje wpisany nowy.
    public int scoreFromLastGame;
    public Text firstScore, message;

    void Start ()
    {
        scoreFromLastGame = FindObjectOfType<GameManager>().GetScore();
        Destroy(FindObjectOfType<GameManager>().gameObject);
        AddOrChangeScores();
        firstScore.text = PlayerPrefs.GetInt("HighScore").ToString();

    }
    public void AddOrChangeScores()
    {
        if (scoreFromLastGame > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", scoreFromLastGame);
            message.gameObject.SetActive(true);
            message.GetComponentInChildren<AudioSource>().Play();

        }
    }

    private void Update()
    {
        firstScore.text = PlayerPrefs.GetInt("HighScore").ToString();

    }
    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }


}
