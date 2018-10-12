using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public enum PlaceHas
    {
        None,
        Obstacle,
        Apple,
        Snake
    }

    public struct Position
    {
        int x;
        int y;
        public Position(int nx, int ny)
        {
            x = nx;
            y = ny;
        }
    }
    public class Place
    {
        /// <summary>
        /// If place is an obstacle, its game over if snake touch it
        /// </summary>
        public bool Obstacle { get; set; }
        public PlaceHas Has { get; set; }
        public Position Coordinates { get; }
        public Place()
        {
            Position pos = new Position(1,1);

            this.Coordinates = new Position(0,0);
            Obstacle = false;
            Has = PlaceHas.None;

        }
        public Place(Position npos)
        {
            this.Coordinates = npos;
            Obstacle = false;
            Has = PlaceHas.None;
        }
    }
}
