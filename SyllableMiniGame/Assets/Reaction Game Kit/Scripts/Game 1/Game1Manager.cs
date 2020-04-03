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

public class Game1Manager : MonoBehaviour
{
    [Header("Game UI")]
    public static string currentWord;
    public Transform spelledWord;
    public Text scoreText;
    public Image colorMainImage;
    public Image colorOneImage;
    public Image colorTwoImage;
    public Image colorThreeImage;
    public Image colorFourImage;
    public GameObject preventActions;

    [Header("Game Over UI")]
    public GameObject gameOverUI;
    public Text gameOverScore;

    [Header("Game Variables")]
    public float scoreToAdd;
    public float delayBetweenRounds;
    public List<Color> gameColors;
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
            spelledWord.GetComponent<Text>().text = currentWord;
            // Need to bring concatenated word up front on game ui while playing
            // If you know how to do it, please change the statement under this, it didn't work
            // spelledWord.SetAsFirstSibling();
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
        elaspedTime = 0;

        currentTime = maxTime;

        List<Color> randomColors = new List<Color>();
        randomColors = gameColors;

        System.Random rnd = new System.Random();
        for (int i = 1; i < randomColors.Count; i++)
        {
            int pos = rnd.Next(i + 1);
            var x = randomColors[i];
            randomColors[i] = randomColors[pos];
            randomColors[pos] = x;
        }

        randomColorRight = Random.Range(0, 4);

        colorMainImage.color = randomColors[randomColorRight];
        colorOneImage.color = randomColors[0];
        colorTwoImage.color = randomColors[1];
        colorThreeImage.color = randomColors[2];
        colorFourImage.color = randomColors[3];
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