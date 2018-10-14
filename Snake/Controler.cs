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
            View.update();
        }

        void TimerEventProcessor(Object obj, EventArgs args)
        {
            for(int i = Mod.TheSnake.Segments.Count - 1; i > 0; i--)
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


            //lastPos = pos;
            //pos++;
            //if (pos >= Mod.PlayRows)
            //    pos = 0;
            //Mod.gameMap[1, lastPos].Has = PlaceHas.None;
            //Mod.gameMap[1, pos].Has = PlaceHas.Apple;
            View.update();
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

            View.update();
        }

        public void startGame()
        {
            throw new NotImplementedException();
        }
    }
}
