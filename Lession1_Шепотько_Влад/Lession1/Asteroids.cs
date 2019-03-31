using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace Lession1
{
    class Asteroids: Settings
    {
        protected static Size _size;
        protected int animCount = 0;
        protected Image cookie;

        protected List<Image> cookies = new List<Image> {Image.FromFile("Cookies/cookie1.png"), Image.FromFile("Cookies/cookie2.png"),
                                                         Image.FromFile("Cookies/cookie3.png"),Image.FromFile("Cookies/cookie4.png") };

        protected List<Image> explosion = new List<Image> {Image.FromFile("Cookies/boom/boom1.png"), Image.FromFile("Cookies/boom/boom2.png"),
                                                           Image.FromFile("Cookies/boom/boom3.png"),Image.FromFile("Cookies/boom/boom4.png"),
                                                           Image.FromFile("Cookies/boom/boom5.png"),Image.FromFile("Cookies/boom/boom6.png") };

        protected bool isCrashed = false; //проверка имбирного астероида, был ли он взорван

        int cookieNum = rng.Next(0, 4);  // рандомный выбор одного из вариантов астероида
        int waitTime = rng.Next(1, 5) * 200; // переменная для задержки респавна астероида

        public Asteroids(Point pos, Point dir, Size size) : base(pos, dir, size) { }

        public override void Draw()
        {
            if (isCrashed) cookie = Animator(explosion, 3); // Запускает анимацию взрыва при получении урона
            else cookie = cookies[cookieNum];
            _size = Size;
            Buffer.Graphics.DrawImage(cookie, Pos.X + waitTime, Pos.Y, Size.Width, Size.Height);
        }
        public override void Update()
        {
            if (isCrashed == false)
            {
                animCount++;
                if (animCount % 5 == 4) cookie.RotateFlip(RotateFlipType.Rotate90FlipXY);
                Pos.X -= Dir.X;
            }
            if (Pos.X + waitTime < -40) // обычный спавн
            {
                Pos.X = Width + 10;
                Dir.X = rng.Next(5, 15);
                Pos.Y = rng.Next(0, 7) * 100;
            }
            if(cookie == explosion[explosion.Count - 1]) // спавнит заново метеор после взрыва
            {
                isCrashed = false;
                Pos.X = Width + waitTime;
                Dir.X = rng.Next(5, 15);
                Pos.Y = rng.Next(0, 7) * 100;
            }
        }
        public override void Update(bool _bool)
        {
            isCrashed = _bool;
        }

        public override Rectangle Rect => new Rectangle(new Point(Pos.X + waitTime,Pos.Y), _size);
        public override bool Collision(ICollision o) => o.Rect.IntersectsWith(this.Rect);
    }
}
