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
            updateSnakePosition();
            checkEatingApple();

            View.update();
        }

        private void spawnApple()
        {
            Mod.TheApple.place.Coordinates = randomAvaliablePosition();
        }

        private Position randomAvaliablePosition()
        {
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

        private void updateSnakePosition()
        {
            for (int i = Mod.TheSnake.Segments.Count - 1; i > 0; i--)
            {
                Mod.TheSnake.Segments[i].Coordinates = Mod.TheSnake.Segments[i - 1].Coordinates;
            }

            Position pos = Mod.TheSnake.Segments[0].Coordinates;
            switch (Mod.TheSnake.movementDirection)
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
            Mod.TheSnake.Segments[0].Coordinates = pos;
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
