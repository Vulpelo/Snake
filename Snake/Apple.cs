using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public class Apple
    {
        public Place place { get; set; }

        public Apple()
        {
            place = new Place();
            place.Coordinates = new Position(0, 0);
            place.Has = PlaceHas.Apple;
            place.Obstacle = false;
        }
    }
}
