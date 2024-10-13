using System;
using System.IO;

namespace CatAndMouseGame
{
    enum GameState
    {
        Start,
        End
    }

    class Game
    {
        private Player cat;
        private Player mouse;
        private int gridSize;
        private GameState currentState;
        public Game()
        {
            cat = new Player("Cat");
            mouse = new Player("Mouse");
            currentState = GameState.Start;
        }
        public void StartGame(string inputFilePath, string outputFilePath)
        {
            using StreamReader reader = new StreamReader(inputFilePath);
            using StreamWriter writer = new StreamWriter(outputFilePath);

            string firstLine = reader.ReadLine();
            gridSize = int.Parse(firstLine.Trim());
            currentState = GameState.Start;

            writer.WriteLine("Cat and Mouse\n\n");
            writer.WriteLine($"{"Cat",-10} {"Mouse",-10} {"Distance",-10}");
            writer.WriteLine(new string('-', 30));

            string[] lines = File.ReadAllLines(inputFilePath);

            for (int i = 1; i < lines.Length; i++) 
            {
                string line = lines[i].Trim();
                if (string.IsNullOrEmpty(line)) continue;

                string[] parts = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length < 1) continue;

                char player = parts[0][0];

                if (player == 'P')
                {
                    PrintState(writer);
                }
                else if (parts.Length > 1 && int.TryParse(parts[1], out int step))
                {
                    ProcessMove(player, step);
                    if (IsMouseCaught())
                    {
                        currentState = GameState.End;
                        break;
                    }
                }
            }
            EndGame(writer);
        }
        public void ProcessMove(char playerType, int step)
        {
            switch (playerType)
            {
                case 'C':
                    if (cat.state == PlayerState.NotInGame)
                        cat.SetPosition(step);
                    else
                        cat.Move(step, gridSize);
                    break;

                case 'M':
                    if (mouse.state == PlayerState.NotInGame)
                        mouse.SetPosition(step);
                    else
                        mouse.Move(step, gridSize);
                    break;
            }
        }
        public void PrintState(StreamWriter writer)
        {
            int distance = CalculateDistance();
            string catPosition = cat.state == PlayerState.NotInGame ? "??" : cat.position.ToString();
            string mousePosition = mouse.state == PlayerState.NotInGame ? "??" : mouse.position.ToString();
            string distanceOutput = distance > 0 ? distance.ToString() : "";

            writer.WriteLine($" {catPosition,-12} {mousePosition,-13} {distanceOutput,-9}");
        }
        private int CalculateDistance()
        {
            if (cat.state == PlayerState.Playing && mouse.state == PlayerState.Playing)
            {
                return Math.Abs(cat.position - mouse.position);
            }
            return 0;
        }
        public bool IsMouseCaught()
        {
            return cat.position == mouse.position && cat.state == PlayerState.Playing && mouse.state == PlayerState.Playing;
        }
        public void EndGame(StreamWriter writer)
        {
            writer.WriteLine($"{new string('-', 30)}\n\n\n");
            writer.WriteLine($"{"Distance traveled:",-23} {"Mouse",-10} {"Cat",-10}");
            writer.WriteLine($"{"",-26} {mouse.distanceTraveled,-8} {cat.distanceTraveled,-12}\n");

            if (IsMouseCaught())
            {
                writer.WriteLine($"Mouse caught at: {cat.position}");
            }
            else
            {
                writer.WriteLine("Mouse evaded Cat");
            }
        }
    }
}