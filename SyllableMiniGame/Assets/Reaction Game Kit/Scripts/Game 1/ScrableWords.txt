# Class variables:
# ArrayList wordList - contains the individual wordlists returned by the FileReader
# ArrayList currentList - contains the current wordlist to use in the game 

using System;
using System.Collections;

private void ChangeWordList()
    {
    
    #Selects/changes a wordlist (ie. theme) to use for the round
    # TO ADD HERE: FUNCTIONALITY TO CHECK THAT THE SAME LIST IS NOT CHECKED AGAIN? (if previous list selected, select again)

    Random rnd = new Random();
    int wordListLength = wordList.Count;
    int rndint = rnd.Next(1, wordListLength + 1) - 1;
    currentList = wordList[rndint];


    }

private string[] ScrambleSyllables(int numberOfSyllables)
    {
    #Select all syllable arrays of given (=numberOfSyllables) length from currentList
    ArrayList givenLengthWords = new ArrayList();

    # THIS FOREACH LOOPS RUNS EVERY TIME A NEW WORD IS PRESENTED TO THE PLAYER
    # DOES THIS SLOW THE GAME DOWN TOO MUCH - COULD SEPARATE THIS TO A SEPARATE METHOD
    foreach (string syllables in currentList) {
        if(syllables.Length == numberOfSyllables) {
            givenLengthWords.Add(syllables);
        }
    }

    # Choose a random word from the list
    string[] syllableArray = givenLenghtWords[Random.Range(0, givenLengthWords.Length - 1)];

    #Choose a random order for the syllables using Knuth shuffle algorithm
    for (int t = 0; t < syllableArray.Length; t++ )
        {
            string tmp = syllableArray[t];
            int r = Random.Range(t, syllableArray.Length);
            syllableArray[t] = syllableArray[r];
            syllableArray[r] = tmp;
        }


    return syllableArray;    
    }