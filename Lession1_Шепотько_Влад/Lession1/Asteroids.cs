using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace Lession1
{
    class Asteroids: Settings
    {
        protected int animCount = 0;
        protected Image cookie;
        protected Image[] cookies = new Image[] {Image.FromFile("Cookies/cookie1.png"), Image.FromFile("Cookies/cookie2.png"),
                                                Image.FromFile("Cookies/cookie3.png"),Image.FromFile("Cookies/cookie4.png") };


        int cookieNum = rng.Next(0, 4);
        int waitTime = rng.Next(1, 5) * 200;

        public Asteroids(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }

        public override void Draw()
        {
            cookie = cookies[cookieNum];

            //Game.Buffer.Graphics.DrawEllipse(Pens.White, Pos.X, Pos.Y, Size.Width, Size.Height);
            Buffer.Graphics.DrawImage(cookie, Pos.X + waitTime, Pos.Y, Size.Width, Size.Height);
        }
        public override void Update()
        {
            animCount++;
            if (animCount%5 == 4) cookie.RotateFlip(RotateFlipType.Rotate90FlipXY);
            Pos.X -= Dir.X;
            if (Pos.X + waitTime < -40)
            {
                Pos.X = Width + 10;
                Dir.X = rng.Next(5, 15);
                Pos.Y = rng.Next(0, 7) * 100;
            }
        }
    }
}
