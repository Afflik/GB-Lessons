using System;
using System.Collections.Generic;
using System.Drawing;

namespace Lession1
{
    class Stars : Settings
    {
        protected Image star1 = Image.FromFile("star1.png");
        protected Image star2 = Image.FromFile("star2.png");
        protected Image star3 = Image.FromFile("star3.png");
        protected Image star4 = Image.FromFile("star4.png");

        protected Image img;

        public Stars(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }
        public override void Draw()
        {
            int star = rng.Next(1, 5);

            if (star == 1) img = star1;
            if (star == 2) img = star2;
            if (star == 3) img = star3;
            if (star == 4) img = star4;

            Buffer.Graphics.DrawImage(img, Pos.X, Pos.Y, Size.Width, Size.Height);
            //Game.Buffer.Graphics.DrawLine(Pens.White, Pos.X, Pos.Y, Pos.X + Size.Width, Pos.Y + Size.Height);
            //Game.Buffer.Graphics.DrawLine(Pens.White, Pos.X + Size.Width, Pos.Y, Pos.X, Pos.Y + Size.Height);
        }
        public override void Update()
        {
            Pos.X = Pos.X - Dir.X;
            if (Pos.X < -20)
            {
                Pos.X = Width + 10;
                Pos.Y = rng.Next(0, 7) * 100;

            }
            if (Pos.X > Width + 100) Dir.X = -Dir.X;
        }
    }
}
