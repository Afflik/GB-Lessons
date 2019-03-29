using System;
using System.Collections.Generic;
using System.Drawing;

namespace Lession1
{
    class LittleStars : Settings
    {
        public LittleStars(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }
        public override void Draw()
        {
            Buffer.Graphics.DrawEllipse(Pens.White, Pos.X, Pos.Y, Size.Width, Size.Height);
        }
        public override void Update()
        {
            Pos.X -= Dir.X;
            if (Pos.X < -20)
            {
                Pos.X = Width + 10;
                Pos.Y = rng.Next(0, 160) * 5;

            }
        }
    }
}
