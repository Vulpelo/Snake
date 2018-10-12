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
        void TimerEventProcessor(Object obj, EventArgs args)
        {
            lastPos = pos;
            pos++;
            if (pos >= Mod.PlayRows)
                pos = 0;
            Mod.gameMap[1, lastPos].Has = PlaceHas.None;
            Mod.gameMap[1, pos].Has = PlaceHas.Apple;
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
        }

        public void startGame()
        {
            throw new NotImplementedException();
        }
    }
}
