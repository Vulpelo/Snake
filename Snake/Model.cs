using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public class Model
    {
        public PlayerSnake TheSnake { get; set; }
        public int PlayRows { get; set; }
        public int PlayColumns { get; set; }
        public Place[,] gameMap;


        public Model()
        {
            TheSnake = new PlayerSnake(3);
            PlayRows = 10;
            PlayColumns = 10;
            gameMap = new Place[PlayRows, PlayColumns];
            for(int i=0; i< PlayColumns; i++)
            {
                for(int j=0; j< PlayRows; j++)
                {
                    gameMap[i, j] = new Place(new Position(i, j));
                }
            }
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
