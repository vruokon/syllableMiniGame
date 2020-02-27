//https://polygonplanet.com/
//Copyright © 2016 Polygon Planet. All rights reserved.
//This source file is protected under UNITYS Asset Store Terms of Service and EULA which can be viewed here https://unity3d.com/legal/as_terms

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not use

using UnityEngine;
using UnityEngine.UI;

public class Game2Manager : MonoBehaviour
{
    [Header("Game UI")]
    public Text scoreText;
    public GameObject preventActions;
    public GameObject objectTemplateLayout;
    public GameObject objectTemplate;

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
    public static bool roundOver;

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

    public void StartGame() //Starts the game.
    {
        gameOverUI.SetActive(false);
        currentScore = 0;
        timeSinceLastAwnser = 0;
        roundOver = false;
        gameStarted = true;
        NextRound();
    }

    private void NextRound() //Executes the next round.
    {
        foreach (Transform t in objectTemplateLayout.transform)
            Destroy(t.gameObject);

        elaspedTime = 0;
        currentTime = maxTime;
        int birdX = Random.Range(500, -500);
        int birdY = Random.Range(800, -800);
        GameObject g = (GameObject)Instantiate(objectTemplate);
        g.transform.SetParent(objectTemplateLayout.transform, false);
        g.GetComponent<RectTransform>().localPosition = new Vector2(birdX, birdY);
    }

    private void Timer() //Timer before the game ends.
    {
        if (roundOver == false && gameStarted == true)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
                GameOver();
        }
    }

    private void DelayTimer() //Calcualtes the score to add.
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

    private void GameOver() //Ends the game.
    {
        gameStarted = false;
        roundOver = false;
        gameOverUI.SetActive(true);
        gameOverScore.text = "Score: " + currentScore.ToString();
    }
}