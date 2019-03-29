using System;
using System.Collections.Generic;
using System.Drawing;

namespace Lession1
{
    class Stars : Settings
    {
        protected Image[] star = new Image[] {Image.FromFile("Stars/star1.png"), Image.FromFile("Stars/star2.png"),
                                                Image.FromFile("Stars/star3.png"),Image.FromFile("Stars/star4.png") };

        protected Image img;

        public Stars(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }
        public override void Draw()
        {
            int starNum = rng.Next(0, 4);
            img = star[starNum];

            Buffer.Graphics.DrawImage(img, Pos.X, Pos.Y, Size.Width, Size.Height);
            //Game.Buffer.Graphics.DrawLine(Pens.White, Pos.X, Pos.Y, Pos.X + Size.Width, Pos.Y + Size.Height);
            //Game.Buffer.Graphics.DrawLine(Pens.White, Pos.X + Size.Width, Pos.Y, Pos.X, Pos.Y + Size.Height);
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
