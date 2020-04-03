using System;
using System.IO;
using UnityEngine;


namespace FileReader
{
    class Word
    {
        //Simple class that reads csw file from the file system
        protected string[] listOfWords;
        protected string fileToRead = "Assets\\Reaction Game Kit\\Scripts\\Game 1\\KategoriatJaSanat.csv";


        public string[] readFile()
        {
            string[] lines = File.ReadAllLines(this.fileToRead);
            return lines;
        }
        public void setListOfWords(string[] lines)
        {
            this.listOfWords = lines;
        }
        public string[] getListOfWords()
        {
            return this.listOfWords;
        }
        public string[] getListByCategory(int num)
        {
            string[] categoryList = new string[this.listOfWords.Length];
            for (var i = 0; i < this.listOfWords.Length; i++)
            {
                string[] words = this.listOfWords[i].Split(';');
                Debug.Log(string.Join(" ", words));
                categoryList[i] = words[num];
            }
            return categoryList;
        }
    }
}
