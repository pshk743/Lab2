using System;
using System.IO;

namespace CatAndMouseGame
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputFilePath = "2.ChaseData.txt";
            string outputFilePath = "2.PursuitLog.txt";

            string[] commands = File.ReadAllLines(inputFilePath);
            int gridSize = int.Parse(commands[0].Trim());
            Game game = new Game(gridSize);

            using (StreamWriter writer = new StreamWriter(outputFilePath))
            {
                writer.WriteLine("Cat and Mouse\n\n");
                writer.WriteLine($"{"Cat",-10} {"Mouse",-10} {"Distance",-10}");
                writer.WriteLine(new string('-', 30));

                for (int i = 1; i < commands.Length; i++)
                {
                    string command = commands[i].Trim();
                    if (string.IsNullOrEmpty(command)) continue;

                    string[] parts = command.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length < 1) continue;

                    char player = parts[0][0];

                    if (player == 'P')
                    {
                        game.PrintState(writer);
                    }
                    else if (parts.Length > 1 && int.TryParse(parts[1], out int step))
                    {
                        game.ProcessMove(player, step);
                        if (game.IsMouseCaught()) break;
                    }
                }
                game.EndGame(writer);
            }  
        }
    }
}
