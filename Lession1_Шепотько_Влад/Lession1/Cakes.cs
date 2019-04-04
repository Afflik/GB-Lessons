using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Media;
using System.IO;

namespace Lession1
{
    class Cakes : Settings
    {
        int pl;
        MediaPlayer cakeSong = new MediaPlayer();

        public static bool destroyCake = false;

        protected Image _cake;
        protected Image cake = Image.FromFile("Bonus/HealthCake.png");

        protected List<Image> explosionCake = new List<Image> {Image.FromFile("Bonus/boom/boom1.png"), Image.FromFile("Bonus/boom/boom2.png"),
                                                       Image.FromFile("Bonus/boom/boom3.png"),Image.FromFile("Bonus/boom/boom4.png"),
                                                       Image.FromFile("Bonus/boom/boom5.png"),Image.FromFile("Bonus/boom/boom6.png"),
                                                       Image.FromFile("Bonus/boom/boom7.png"),Image.FromFile("Bonus/boom/boom8.png") };

        public Cakes(Point pos, Point dir, Size size) : base(pos, dir, size) { }

        protected bool isCakeDroped = false;

        public override void Draw()
        {
            if (isCakeDroped) _cake = Animator(explosionCake, 1); // Запускает анимацию взрыва при получении урона или при лечении игрока
            else _cake = cake;
            Buffer.Graphics.DrawImage(_cake, Pos.X, Pos.Y, Size.Width, Size.Height);
        }
        public override void Update()
        {
            if (!isCakeDroped)
            {
                Pos.X -= Dir.X;
            }
            if (Pos.X < -40 - rng.Next(500)) // обычный спавн
            {
                Pos.X = rng.Next(800,1500);
                Pos.Y = rng.Next(1, 7) * 100;
            }
            if (_cake == explosionCake[explosionCake.Count - 1]) // спавнит заново кейк после взрыва
            {
                Pos.X = 1000 + rng.Next(1000);
                Pos.Y = rng.Next(1, 14) * 50;
                Player.cooldownHeal = 0;
                isCakeDroped = false;
                pl = 0;
            }
            if(_cake == explosionCake[0])
            {
                cakeSong.Open(new Uri("music/cake.wav", UriKind.Relative));
                cakeSong.Volume = 0.7;
                cakeSong.Play();
            }
        }
        public override void Update(bool _bool)
        {
            if (pl == 0 && Player.isShooting)
            {
                destroyCake = true;
                pl++;
            }
                isCakeDroped = _bool;
        }

        public override Rectangle Rect => new Rectangle(Pos, new Size(60,60));
        public override bool Collision(ICollision o) => o.Rect.IntersectsWith(this.Rect);
    }
}
