using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace Lession1
{
    class Game : Settings
    {
        public static Settings[] _objs;

        public Game(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }

        public static void Play()
        {
            Load();
            Ticker();
        }

        public static void Load()
        {
            _objs = new Settings[170];
            for (int i = 0; i < 50; i++)
            {
                int lStarPlaceX = rng.Next(0, 80) * 10;
                int lStarPlaceY = rng.Next(0, 60) * 10;
                _objs[i] = new LittleStars(new Point(lStarPlaceX, lStarPlaceY), new Point(2), new Size(1, 1));
            }
            for (int i = 50; i < 150; i++)
            {
                int starSize = rng.Next(1, 5) * 5;
                int starPlaceX = rng.Next(0, 80) * 10;
                int starPlaceY = rng.Next(0, 60) * 10;
                _objs[i] = new Stars(new Point(starPlaceX, starPlaceY), new Point(2), new Size(starSize, starSize));
            }
            for (int i = 150; i < 170; i++)
            {
                int astSpeed = rng.Next(5, 15);
                int astSize = rng.Next(2, 11) * 5;
                int astPlaceY = rng.Next(0, 7) * 100;
                _objs[i] = new Asteroids(new Point(800, astPlaceY), new Point(astSpeed), new Size(astSize, astSize));
            }
        }
        public static void Drawler()
        {
            Buffer.Graphics.Clear(Color.Black);
            foreach (Settings obj in _objs) obj.Draw();

            try
            {
                Buffer.Render();
            }
            catch
            {
                Environment.Exit(0);
            }

            foreach (Settings obj in _objs) obj.Update();
        }
        public static void Ticker()
        {
            Timer timer = new Timer { Interval = 60 };
            timer.Start();
            timer.Tick += Timer_Tick;
        }
        private static void Timer_Tick(object sender, EventArgs e)
        {
            Drawler();
        }
    }
}
