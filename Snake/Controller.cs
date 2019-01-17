using System;
using System.Windows.Forms;

namespace Snake
{
    class Controller
    {
        Timer myTimer = new Timer();
        bool playing = false;
        Random rand = new Random();
        private IView View { get; set; }
        private Model Mod { get; set; }

        private Controller()
        {
            View.update();
        }
        public Controller(IView nView, Model nModel)
        {
            this.View = nView;
            this.Mod = nModel;

            // Starting coroutine
            myTimer.Tick += new EventHandler(TimerEventProcessor);

            // Sets the timer interval to 0.5 seconds.
            myTimer.Interval = 500;
            myTimer.Start();

            View.update();
        }

        private void TimerEventProcessor(Object obj, EventArgs args)
        {
            if (playing)
            {
                if (isSnakeColliding())
                {
                    gameEnded();
                }
                else
                {
                    updateSnakeLength();
                    updateSnakePosition();

                    checkEatingApple();
                }
            }
            else if(Mod.StartButtonClicked)
            {
                playing = true;
                Mod.defaultValues();
                spawnApple();
            }

            View.update();
        }

        private void updateSnakeLength()
        {
            if (Mod.TheSnake.newSegments > 0)
            {
                Mod.TheSnake.Segments.Add(new Place(Mod.TheSnake.Segments[Mod.TheSnake.Segments.Count - 1].Coordinates));
                Mod.TheSnake.newSegments--;
            }
        }

        private void gameEnded()
        {
            View.setGameState(GameState.EndGame);
            playing = false;
        }

        private void spawnApple()
        {
            Position nPos;
            if (randomAvaliablePosition(out nPos))
            {
                if (Mod.TheApple == null) {
                    Mod.TheApple = new Apple();
                }
                Mod.TheApple.place.Coordinates = nPos;
            }
            else
            {
                Mod.TheApple = null;
                gameEnded();
            }
        }

        /// <summary>
        /// true if snake will be colliding when position gonna be updated
        /// </summary>
        /// <returns></returns>
        private bool isSnakeColliding()
        {
            Position head = Mod.TheSnake.Segments[0].Coordinates;
            head = changePosDependingOnDir(head, Mod.TheSnake.MovementDirection);

            // if out of bounds
            if (head.X >= Mod.PlayColumns || head.Y >= Mod.PlayRows || head.X < 0 || head.Y < 0)
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
        
        /// <summary>
        /// Gives random avaliable position on map
        /// </summary>
        /// <param name="nPos"></param>
        /// <returns>true if avaliable position was found</returns>
        private bool randomAvaliablePosition(out Position nPos)
        {
            nPos = new Position(0,0);
            // all avaliable places
            int size = Mod.PlayColumns * Mod.PlayRows - Mod.TheSnake.Segments.Count;

            if (size <= 0) {
                return false;
            }

            int randPosNumber = rand.Next(0, size);

            int i = 0;
            int actualChosenPos = 0;

            // sprawdz wszystkie pola po kolei
            for(i=0; i < Mod.PlayColumns * Mod.PlayRows; i++)
            {
                bool found = false;
                foreach (Place p in Mod.TheSnake.Segments)
                {
                    int tmp = p.Coordinates.Y * Mod.PlayColumns + p.Coordinates.X;
                    // if position is not empty skip rest of loop body
                    if (tmp == i)
                    {
                        found = true;
                        break;
                    }
                }
                if (found)
                    continue;
                if(actualChosenPos == randPosNumber) {
                    break;
                }
                actualChosenPos++;
            }
            nPos.X = i % Mod.PlayColumns;
            nPos.Y = i / Mod.PlayColumns;
            return true;
        }

        private void checkEatingApple()
        {
            // if head is in apple position
            if(Mod.TheApple != null && Mod.TheSnake.Segments[0].Coordinates.Equals(Mod.TheApple.place.Coordinates))
            {
                // create new segment and add it to snake. Segment position is equal to last one
                Mod.TheSnake.addSegment();
                Mod.applesEaten++;

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
                Mod.TheSnake.MovementDirection);
        }

    }
}
