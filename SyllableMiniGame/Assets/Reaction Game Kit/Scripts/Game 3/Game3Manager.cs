//Poylgon Planet - Contact. https://polygonplanet.com/contact/
//Copyright © 2016-2018 Polygon Planet. All rights reserved. https://polygonplanet.com/privacy-policy/
//This source file is subject to Unity Technologies Asset Store Terms of Service. https://unity3d.com/legal/as_terms

#pragma warning disable 0168 //Variable declared, but not used.
#pragma warning disable 0219 //Variable assigned, but not used.
#pragma warning disable 0414 //Private field assigned, but not used.
#pragma warning disable 0649 //Variable asisgned to, and will always have default value.

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game3Manager : MonoBehaviour
{
    [Header("Game UI")]
    public Text scoreText;
    public List<Image> colorImages;
    public GameObject preventActions;

    [Header("Game Over UI")]
    public GameObject gameOverUI;
    public Text gameOverScore;

    [Header("Game Variables")]
    public float scoreToAdd;
    public float delayBetweenRounds;
    public float maxTime;
    public AudioSource click;

    //Variables
    private bool gameStarted;
    private float currentScore;
    private float currentTime;
    private float timeSinceLastAwnser;
    private float elaspedTime;
    private int randomColorRight;
    private bool roundOver;

    private void Start()
    {
        StartGame();
    }

    private void Update()
    {
        if (gameStarted == true)
        {
            scoreText.text = currentScore.ToString();
            Timer();
            DelayTimer();
        }
    }

    public void StartGame()
    {
        gameOverUI.SetActive(false);
        currentScore = 0;
        timeSinceLastAwnser = 0;
        roundOver = false;
        gameStarted = true;
        NextRound();
    }

    private void NextRound()
    {
        foreach (Image i in colorImages)
            i.color = Color.black;

        elaspedTime = 0;
        currentTime = maxTime;
        Color randomColor = UnityEngine.Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f, 1f, 1f);
        randomColorRight = Random.Range(0, 16);
        colorImages[randomColorRight].color = randomColor;
    }

    private void Timer()
    {
        if (roundOver == false && gameStarted == true)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
                GameOver();
        }
    }

    private void DelayTimer()
    {
        if (roundOver == true && gameStarted == true)
        {
            elaspedTime += Time.deltaTime;
            preventActions.SetActive(true);
            if (elaspedTime >= delayBetweenRounds)
            {
                currentScore += scoreToAdd;
                preventActions.SetActive(false);
                roundOver = false;
                NextRound();
            }
        }
    }

    public void Awnser(int thisAwnser)
    {
        click.Play();
        if (thisAwnser == randomColorRight)
            roundOver = true;
        else
            GameOver();
    }

    private void GameOver()
    {
        gameStarted = false;
        roundOver = false;
        gameOverUI.SetActive(true);
        gameOverScore.text = "Score: " + currentScore.ToString();
    }
}