using System;
using System.IO;

namespace CatAndMouseGame
{
    class Program
    {
        static void Main(string[] args)
        {
            int count_file = 3; 

            for (int i = 0; i < count_file; i++)
            {
                Game game = new Game();
                game.StartGame($"{i + 1}.ChaseData.txt", $"{i + 1}.PursuitLog.txt");
            }
        }
    }
}

