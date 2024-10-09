using System;
using System.IO;

namespace CatAndMouseGame1
{
    public class Game
    {
        private Player cat;
        private Player mouse;
        private int gridSize;

        public Game(int gridSize)
        {
            this.gridSize = gridSize;
            cat = new Player("Cat");
            mouse = new Player("Mouse");
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
