﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public enum Direction
    {
        Up,
        Left,
        Right,
        Down
    }
    public enum PlaceHas
    {
        None,
        Obstacle,
        Apple,
        Snake
    }
}