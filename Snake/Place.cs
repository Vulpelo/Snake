using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public struct Position
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Position(int nx, int ny)
        {
            X = nx;
            Y = ny;
        }
    }
    public class Place
    {
        /// <summary>
        /// If place is an obstacle, its game over if snake touch it
        /// </summary>
        public bool Obstacle { get; set; }
        public Position Coordinates { get; set; }
        public Place()
        {
            Position pos = new Position(1,1);

            this.Coordinates = new Position(0,0);
            Obstacle = false;

        }
        public Place(Position npos)
        {
            this.Coordinates = npos;
            Obstacle = false;
        }
    }
}
