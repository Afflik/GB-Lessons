using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace Lession1
{
    class Asteroids: Settings
    {
        protected int animCount = 0;
        protected Image img = Image.FromFile("asteroid.png");

        public Asteroids(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }

        public override void Draw()
        {
            //Game.Buffer.Graphics.DrawEllipse(Pens.White, Pos.X, Pos.Y, Size.Width, Size.Height);
            Buffer.Graphics.DrawImage(img, Pos.X, Pos.Y, Size.Width, Size.Height);
        }
        public override void Update()
        {
            animCount++;
            if (animCount%5 == 4) img.RotateFlip(RotateFlipType.Rotate90FlipXY);
            Pos.X = Pos.X + Dir.X;
            if (Pos.X < -40)
            {
                Pos.X = Width + 10;
                Dir.X = rng.Next(5, 15);
                Pos.Y = rng.Next(0, 7) * 100;

            }
            if (Pos.X > Width + 100) Dir.X = -Dir.X;
        }
    }
}
