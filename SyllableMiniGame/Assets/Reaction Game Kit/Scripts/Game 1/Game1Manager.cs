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
using FileReader;

public class Game1Manager : MonoBehaviour
{
    [Header("Startscreen Ui")]
    public GameObject startscreen;

    [Header("Game UI")]
    public GameObject gameui;
    public Text scoreText;
    public Image colorMainImage;
    public Image colorOneImage;
    public Image colorTwoImage;
    public Image colorThreeImage;
    public Image colorFourImage;
    public GameObject preventActions;
    public GameObject category;
    public GameObject answer;
    public GameObject text_1;
    public GameObject text_2;
    public GameObject text_3;
    public GameObject text_4;
    public GameObject button_1;
    public GameObject button_2;
    public GameObject button_3;
    public GameObject button_4;
    public Text time;

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
    private string[] wordLisgByCategory;
    private string theWord;
    private string[] scrambledWord;
    private string[] guessedWord;
    private float currentScore;
    private float currentTime;
    private float timeSinceLastAwnser;
    private float elaspedTime;
    private int randomColorRight;
    private bool roundOver;

    private void Start()
    {
        gameOverUI.SetActive(false);
        gameui.SetActive(false);
        startscreen.SetActive(true);
    }

    private void Update()
    {
        if (gameStarted == true)
        {
            scoreText.text = currentScore.ToString();
            Timer();
            DelayTimer();
            int aika = (int)currentTime;
            time.GetComponent<UnityEngine.UI.Text>().text = aika.ToString();
        }
    }

    public void StartGame()
    {
        gameOverUI.SetActive(false);
        startscreen.SetActive(false);
        gameui.SetActive(true);
        currentScore = 0;
        timeSinceLastAwnser = 0;
        roundOver = false;
        gameStarted = true;
        currentTime = maxTime;
        NextRound();
    }

    private void NextRound()
    {
        answer.GetComponent<UnityEngine.UI.Text>().text = "";
        var words = new FileReader.Word();
        words.setListOfWords(words.readFile());
        elaspedTime = 0;
        wordLisgByCategory = words.getListByCategory(Random.Range(0, 4));
        do
        {
            theWord = wordLisgByCategory[Random.Range(1, wordLisgByCategory.Length)];
        } while (theWord == "tyhjä" || theWord.Split(' ').Length > 4);

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
        button_1.SetActive(true);
        button_2.SetActive(true);
        button_3.SetActive(true);
        button_4.SetActive(true);
        colorOneImage.color = randomColors[0];
        colorTwoImage.color = randomColors[1];
        colorThreeImage.color = randomColors[2];
        colorFourImage.color = randomColors[3];
        category.GetComponent<UnityEngine.UI.Text>().text = wordLisgByCategory[0];
        string[] theWordInList = theWord.Split(' ');
        guessedWord = new string[theWordInList.Length];
        for (int t = 0; t < theWordInList.Length; t++)
        {
            string tmp = theWordInList[t];
            int r = Random.Range(t, theWordInList.Length);
            theWordInList[t] = theWordInList[r];
            theWordInList[r] = tmp;
        }

        scrambledWord = new string[theWordInList.Length];
        scrambledWord = theWordInList;
        text_1.GetComponent<UnityEngine.UI.Text>().text = theWordInList[0];
        text_2.GetComponent<UnityEngine.UI.Text>().text = theWordInList[1];
        if (theWordInList.Length > 2)
        {
            text_3.GetComponent<UnityEngine.UI.Text>().text = theWordInList[2];
        }
        else
        {
            text_3.GetComponent<UnityEngine.UI.Text>().text = " ";
            button_3.SetActive(false);

        }

        if (theWordInList.Length > 3)
        {
            text_4.GetComponent<UnityEngine.UI.Text>().text = theWordInList[3];
        }
        else
        {
            text_4.GetComponent<UnityEngine.UI.Text>().text = " ";
            button_4.SetActive(false);
        }


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
    public void backToMainMenu()
    {
        Start();
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
        int add = guessedWord.Length;
        answer.GetComponent<UnityEngine.UI.Text>().text += scrambledWord[thisAwnser];
        if (theWord.Replace(" ", "").Length <= answer.GetComponent<UnityEngine.UI.Text>().text.Length)
            if (theWord.Replace(" ", "") == answer.GetComponent<UnityEngine.UI.Text>().text)
            {
                currentTime += 5;
                roundOver = true;
            }
            else
            {
                currentTime -= 5;
                roundOver = true;
            }
    }
    public void deleteAwnser()
    {
        answer.GetComponent<UnityEngine.UI.Text>().text = "";
        Debug.Log(scrambledWord.Length);
        button_1.SetActive(true);
        button_2.SetActive(true);
        if (scrambledWord.Length > 2)
        {
            button_3.SetActive(true);

        }
        if (scrambledWord.Length > 3)
        {
            button_4.SetActive(true);

        }
    }

    private void GameOver()
    {
        gameStarted = false;
        roundOver = false;
        gameOverUI.SetActive(true);
        gameOverScore.text = "Pisteet: " + currentScore.ToString();
    }
}