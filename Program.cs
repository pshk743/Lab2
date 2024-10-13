using System;
using System.IO;

namespace CatAndMouseGame
{
    class Program
    {
        static void Main(string[] args)
        {
            int count_file = 3;
            string inputFilePath = "2.ChaseData.txt";
            string outputFilePath = "2.PursuitLog.txt";

            Game game = new Game();
            game.StartGame(inputFilePath, outputFilePath);
        }
    }
}

