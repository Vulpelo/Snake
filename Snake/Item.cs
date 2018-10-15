using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{

    public abstract class Item
    {
        public Place place { get; set; }

        public Item() {
            place = new Place();
            place.Coordinates = new Position(0, 0);
            place.Has = PlaceHas.Apple;
            place.Obstacle = false;
        }
    }
}
