using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public class Model
    {
        public int PlayRows { get; set; }
        public int PlayColumns { get; set; }
        public Place[,] gameMap;
        Direction movementDirection = Direction.Up;


        public Model()
        {
            PlayRows = 10;
            PlayColumns = 10;
            gameMap = new Place[PlayColumns, PlayRows];
            for(int i=0; i< PlayColumns; i++)
            {
                for(int j=0; j< PlayRows; j++)
                {
                    gameMap[i, j] = new Place(new Position(i, j));
                }
            }
        }

        void setDirection(Direction direction)
        {
            movementDirection = direction;
        }
    }
}
