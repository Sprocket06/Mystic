using System;
using System.Numerics;
using Chroma.Graphics;
using System.Collections.Generic;
using Chroma.Diagnostics.Logging;

namespace Mystic.Entities
{
    class Player
    {
        public Vector2 Position;
        public float speed { get; private set; }
        private Vector2 startPosition;
        private Vector2 goalPosition;
        private float travelTime;
        public float timePassed;

        public Log log = LogManager.GetForCurrentAssembly();
        public Player(Vector2 pos)
        {
            Position = pos;
            goalPosition = pos;
            speed = 100;
        }

        public void Draw(RenderContext ctx)
        {
            ctx.Circle(ShapeMode.Fill, Position, 20, Color.White);
        }

        public void Update(float dt)
        {
            if (goalPosition != Position)
            {
                timePassed += dt;
                float p = Math.Min(timePassed / travelTime, 1.0f);
                Position = Vector2.Lerp(startPosition, goalPosition, p);
            }
        }

        public void MoveTo(Vector2 pos)
        {
            travelTime = Vector2.Distance(Position, pos) / speed; //travel time in seconds
            startPosition = Position;
            goalPosition = pos;
            timePassed = 0f;
        }
    }
}
