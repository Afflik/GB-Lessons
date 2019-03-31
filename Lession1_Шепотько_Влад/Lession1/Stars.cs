using System;
using System.Collections.Generic;
using System.Drawing;

namespace Lession1
{
    class Stars : Settings
    {
        protected List<Image> star = new List<Image> {Image.FromFile("Stars/star1.png"), Image.FromFile("Stars/star2.png"),
                                                      Image.FromFile("Stars/star3.png"),Image.FromFile("Stars/star4.png") };

        protected Image img;

        public Stars(Point pos, Point dir, Size size) : base(pos, dir, size) { }
        public override void Draw()
        {
            int starNum = rng.Next(0, star.Count); //анимация свечения звезд
            img = star[starNum];

            Buffer.Graphics.DrawImage(img, Pos.X, Pos.Y, Size.Width, Size.Height);
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
