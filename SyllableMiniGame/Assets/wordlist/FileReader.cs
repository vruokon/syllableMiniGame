using System;
using System.IO;

namespace FileReader
{
    class Filereader
    {
        static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines("C:\\Users\\Omistaja\\Documents\\Koulu\\MiniGame\\syllableMiniGame\\SyllableMiniGame\\Assets\\wordlist\\KategoriatJaSanat.csv;");

            foreach (string line in lines)
                Console.WriteLine(line);
        }
    }
}