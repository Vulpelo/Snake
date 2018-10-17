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
        public PlayerSnake TheSnake { get; set; }
        public int PlayRows { get; set; } = 10;
        public int PlayColumns { get; set; } = 10;
        public Place[,] gameMap;
        public Apple TheApple { get; set; }
        public bool StartButtonClicked { get; set; } = false;

        public Model()
        {
            defaultValues();
        }

        public void defaultValues()
        {
            TheSnake = new PlayerSnake(3);
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
                    TheSnake.movementDirection = Direction.Left;
                    break;
                case 'd':
                    TheSnake.movementDirection = Direction.Right;
                    break;
                case 'w':
                    TheSnake.movementDirection = Direction.Up;
                    break;
                case 's':
                    TheSnake.movementDirection = Direction.Down;
                    break;
            }
        }

        
    }
}
