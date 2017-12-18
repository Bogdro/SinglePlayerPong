using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour {

    //Skrypt zarządzający ruchem piłki.

    private Rigidbody rb;
    //Początkowa prędkość piłki
    private int ballStartSpeed = 8;
    //Początkowy kierunek piłki losowany przy starcie
    public int whatDirectionX, whatDirectionY;
    //Aktualna prędkość piłki
    public float actuallSpeed = 5;
    GameManager gameManager;
    //Zmienna sprawdzająca czy piłka już wystartowała
    private bool ballStarted = false;
    private AudioSource ballSound;
    public Canvas tutorial;

    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        ballSound = GetComponentInChildren<AudioSource>();
        gameManager = FindObjectOfType<GameManager>();
        if (!gameManager.IsTutorialDone())
        {
            tutorial.gameObject.SetActive(true);
        }
    }

    //Funkcja rozpoczynająca ruch piłki. Na początku losuje kierunek w którym piłka ma zacząć się poruszac po czym nadaje jej prędkość.
    void StartBall()
    {
        int tempX = UnityEngine.Random.Range(0, 2);
        if (tempX == 0) tempX = 1;
        else if (tempX == 1) tempX = -1;
        int tempY = UnityEngine.Random.Range(0, 2);
        if (tempY == 0) tempY = 1;
        else if (tempY == 1) tempY = -1;
        rb.velocity = new Vector3(tempX * ballStartSpeed, tempY * ballStartSpeed, 0f);
        actuallSpeed = Math.Abs(tempX * ballStartSpeed);
        tutorial.gameObject.SetActive(false);
        gameManager.TutorialDone();
    }

	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !ballStarted)
        {
            StartBall();
            ballStarted = true;
        }
        if (rb.velocity.x > 0) rb.velocity = new Vector3(actuallSpeed, rb.velocity.y, 0f);
        else { rb.velocity = new Vector3(-actuallSpeed, rb.velocity.y, 0f); }
	}
    
    //Funkcja po odbiciu piłki dodaje jej 1/100 prędkości początkowej, po czym sprawdza czy odbiła się od gracza i jeśli tak, dodaje mu punkt.
    private void OnCollisionExit(Collision collision)
    {
        actuallSpeed+= ballStartSpeed/100.0f;
        if (collision.gameObject.tag == "Player")
        {
            gameManager.AddPoint();
        }
        ballSound.Play();
    }

}
