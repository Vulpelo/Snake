using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake
{
    class Controler : IControler
    {
        Timer myTimer = new Timer();

        private IView View { get; set; }
        private Model Mod { get; set; }
        int pos = 0;
        int lastPos = 0;

        Controler()
        {
            spawnApple();
            View.update();
        }

        void TimerEventProcessor(Object obj, EventArgs args)
        {
            editingDirection();

            if (!isSnakeColliding())
            {
                updateSnakePosition();
                checkEatingApple();
            }
            

            View.update();
        }

        private void spawnApple()
        {
            Mod.TheApple.place.Coordinates = randomAvaliablePosition();
        }

        private bool isSnakeColliding()
            // true if snake will be colliding when position gonna be updated
        {
            Position head = Mod.TheSnake.Segments[0].Coordinates;
            head = changePosDependingOnDir(head, Mod.TheSnake.movementDirection);

            // if out of bounds
            if (head.X >= Mod.PlayColumns || head.Y >= Mod.PlayRows || head.X < 0 || head.X < 0)
            {
                return true;
            }
            else
            {
                for(int i=2; i<Mod.TheSnake.Segments.Count-1; i++)
                {
                    if (head.Equals( Mod.TheSnake.Segments[i].Coordinates ))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void editingDirection()
        {
            if ((Mod.TheSnake.movementDirection == Direction.Up && Mod.TheSnake.lastMovementDirection == Direction.Down)
                || (Mod.TheSnake.movementDirection == Direction.Left && Mod.TheSnake.lastMovementDirection == Direction.Right)
                || (Mod.TheSnake.lastMovementDirection == Direction.Up && Mod.TheSnake.movementDirection == Direction.Down)
                || (Mod.TheSnake.lastMovementDirection == Direction.Left && Mod.TheSnake.movementDirection == Direction.Right))
            {
                Mod.TheSnake.movementDirection = Mod.TheSnake.lastMovementDirection;
            }
        }
        
        private Position randomAvaliablePosition()
        {
            // TODO: Rand position needs to be checked, and case in with all places are occupied
            int size = Mod.PlayColumns * Mod.PlayRows - Mod.TheSnake.Segments.Count;

            Random rand = new Random();
            int randPosNumber = rand.Next(0, size);

            int i = 0;
            int chosenPos = 0;
            // przejscie przez cala tablice
            for(i=0; i <= size-1; i++)
            {
                foreach(Place p in Mod.TheSnake.Segments)
                {
                    int tmp = p.Coordinates.Y * p.Coordinates.X + p.Coordinates.X;
                    // if position is not empty skip rest of loop body
                    if (tmp == i)
                    {
                        i++;
                        continue;
                    }
                }
                if(chosenPos == randPosNumber)
                {
                    break;
                }
                chosenPos++;
            }
            return new Position(i % Mod.PlayColumns, i / Mod.PlayColumns);
        }

        private void checkEatingApple()
        {
            // if head is in apple position
            if(Mod.TheSnake.Segments[0].Coordinates.Equals(Mod.TheApple.place.Coordinates))
            {
                // create new segment and add it to snake. Segment position is equal to last one
                Mod.TheSnake.Segments.Add(new Place(Mod.TheSnake.Segments[Mod.TheSnake.Segments.Count-1].Coordinates));

                // set new position of apple
                spawnApple();
            }

        }

        private Position changePosDependingOnDir(Position pos, Direction dir)
        {
            switch (dir)
            {
                case Direction.Down:
                    pos.Y++;
                    break;
                case Direction.Left:
                    pos.X--;
                    break;
                case Direction.Right:
                    pos.X++;
                    break;
                case Direction.Up:
                    pos.Y--;
                    break;
            }
            return pos;
        }

        private void updateSnakePosition()
        {
            Mod.TheSnake.updateLastMovementDirection();
            for (int i = Mod.TheSnake.Segments.Count - 1; i > 0; i--)
            {
                Mod.TheSnake.Segments[i].Coordinates = Mod.TheSnake.Segments[i - 1].Coordinates;
            }
            

            Mod.TheSnake.Segments[0].Coordinates = 
                changePosDependingOnDir(Mod.TheSnake.Segments[0].Coordinates,
                Mod.TheSnake.movementDirection);

        }

        public Controler(IView nView, Model nModel)
        {
            this.View = nView;
            this.Mod = nModel;

            // Starting coroutine
            myTimer.Tick += new EventHandler(TimerEventProcessor);

            // Sets the timer interval to 0.5 seconds.
            myTimer.Interval = 500;
            myTimer.Start();

            spawnApple();

            View.update();
        }

        public void startGame()
        {
            throw new NotImplementedException();
        }
    }
}
