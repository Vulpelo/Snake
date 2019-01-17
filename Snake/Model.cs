using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public class Model
    {
        public int applesEaten { get; set; }
        public PlayerSnake TheSnake { get; private set; }
        public int PlayRows { get; } = 10;
        public int PlayColumns { get; } = 10;
        public Place[,] gameMap;
        public Apple TheApple { get; set; }
        public bool StartButtonClicked { get; set; } = false;

        public Model()
        {
            defaultValues();
        }

        public void defaultValues()
        {
            TheSnake = new PlayerSnake(1);
            TheApple = new Apple();
            
            gameMap = new Place[PlayRows, PlayColumns];
            for (int i = 0; i < PlayColumns; i++)
            {
                for (int j = 0; j < PlayRows; j++)
                {
                    gameMap[i, j] = new Place(new Position(i, j));
                }
            }
            StartButtonClicked = false;
            applesEaten = 0;
        }

        public void changeDirection(char key)
        {
            switch (key)
            {
                case 'a':
                    TheSnake.MovementDirection = Direction.Left;
                    break;
                case 'd':
                    TheSnake.MovementDirection = Direction.Right;
                    break;
                case 'w':
                    TheSnake.MovementDirection = Direction.Up;
                    break;
                case 's':
                    TheSnake.MovementDirection = Direction.Down;
                    break;
            }
        }
    }
}
