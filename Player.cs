using System;
namespace CatAndMouseGame
{
    public enum PlayerState
    {
        NotInGame,
        Playing,
        Winner,
        Loser
    }
    public class Player
    {
        private string name;
        public int position;
        public PlayerState state;
        public int distanceTraveled;

        public Player(string name)
        {
            this.name = name;
            state = PlayerState.NotInGame;
            position = -1;
            distanceTraveled = 0;
        }

        public void SetPosition(int position)
        {
            this.position = position;
            state = PlayerState.Playing;
        }

        public void Move(int step, int gridSize)
        {
            if (state == PlayerState.Playing)
            {
                int newPosition = (position + step) % gridSize;
                if (newPosition < 0)
                    newPosition += gridSize;

                distanceTraveled += Math.Abs(step);
                position = newPosition;
            }
        }

        public void Catch()
        {
            state = PlayerState.Winner;
        }

        public void Lose()
        {
            state = PlayerState.Loser;
        }
    }
}
