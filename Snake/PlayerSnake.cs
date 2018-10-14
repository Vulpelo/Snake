﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public class PlayerSnake
    {
        public List<Place> Segments { get; }

        public Direction movementDirection { get; set; } = Direction.Right;

        public PlayerSnake(int startLength)
        {
            Segments = new List<Place>();
            for(int i= startLength + 2; i > 2 ; i--)
            {
                Segments.Add(new Place(new Position(i, 2)));
            }
        }

    }
}