using System;
using System.IO;

namespace FileReader
{
    class FileReader
    {
        //Simple class that reads csw file from the file system
        protected string[] listOfWords;
        static void Main(string[] args)
        {
            FileReader test = new FileReader();
            try
            {
                string[] tmpLines = test.readFile("KategoriatJaSanat.csv");
                test.setListOfWords(tmpLines);
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }

        public string[] readFile(string fileName)
        {
            string[] lines = File.ReadAllLines(fileName);
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
    }
}
