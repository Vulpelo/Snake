using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public class PlayerSnake
    {
        public int newSegments { get; set; } = 0;
        public List<Place> Segments { get; }

        private Direction movementDirection = Direction.Right;
        public Direction MovementDirection {
            get {
                return movementDirection;
            }            
            set {
                if ( (value == Direction.Up && LastMovementDirection == Direction.Down)
                        || (value == Direction.Left && LastMovementDirection == Direction.Right)
                        || (LastMovementDirection == Direction.Up && value == Direction.Down)
                        || (LastMovementDirection == Direction.Left && value == Direction.Right) )
                {
                    MovementDirection = LastMovementDirection;
                }
                else
                {
                    movementDirection = value;
                }
            }
        } 

        private Direction LastMovementDirection { get; set; }

        public PlayerSnake(int startLength)
        {
            LastMovementDirection = movementDirection;
            Segments = new List<Place>();
            for(int i= startLength + 2; i > 2 ; i--)
            {
                Segments.Add(new Place(new Position(i, 2)));
            }
        }

        public void updateLastMovementDirection()
        {
            LastMovementDirection = movementDirection;
        }

        public void addSegment()
        {
            newSegments++;
        }

        ~PlayerSnake()
        {
            Segments.Clear();
        }

    }
}
