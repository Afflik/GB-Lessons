using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;

namespace Lession1
{
    class Game : Settings
    {
        public static Settings[] allObjs;
        public static Settings[] asteroids;
        public static Settings[] littleStars;
        public static Settings[] stars;

        public Game(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }

        public static void Play()
        {
            Load();
            Ticker();
        }

        public static void AddGameObject(int numObj,ref Settings[] objs, int lenght, int minX, int maxX, int factorX,
                                      int minY, int maxY, int factorY, int speedMin, int speedMax,
                                      int factorSpeed, int sizeMin, int sizeMax, int factorSize)
        {
            objs = new Settings[lenght];

            for (int i = 0; i < objs.Length; i++)
            {
                int cordX = rng.Next(minX, maxX)* factorX;
                int cordY = rng.Next(minY, maxY) * factorY;
                int speed = rng.Next(speedMin, speedMax) * factorSpeed;
                int size = rng.Next(sizeMin, sizeMax) * factorSize;

                if (numObj == 1)
                {
                    objs[i] = new LittleStars(new Point(cordX, cordY), new Point(speed), new Size(size, size));
                }
                if (numObj == 2)
                {
                    objs[i] = new Stars(new Point(cordX, cordY), new Point(speed), new Size(size, size));
                }
                if (numObj == 3)
                {
                    objs[i] = new Asteroids(new Point(cordX, cordY), new Point(speed), new Size(size, size));
                }
            }
        }
        public static void Load()
        {
            AddGameObject(1, ref littleStars, 50, 0, 160, 5, 0, 120, 5, 2, 3, 1, 1, 2, 1);
            AddGameObject(2, ref stars, 50, 0, 160, 5, 0, 120, 5, 2, 3, 1, 1, 5, 5);
            AddGameObject(3, ref asteroids, 20, 800, 801, 1, 0, 14, 50, 3, 11, 1, 3, 15, 5);

            allObjs = littleStars.Concat(stars).Concat(asteroids).ToArray();
        }
        public static void Drawler()
        {
            Buffer.Graphics.Clear(Color.Black);
            foreach (Settings obj in allObjs) obj.Draw();
            try
            {
                Buffer.Render();
            }
            catch
            {
                Environment.Exit(0);
            }
            foreach (Settings obj in allObjs) obj.Update();
        }
        public static void Ticker()
        {
            Timer timer = new Timer { Interval = 30 };
            timer.Start();
            timer.Tick += Timer_Tick;
        }
        private static void Timer_Tick(object sender, EventArgs e)
        {
            Drawler();
        }

        public override void Draw() { }

        public override void Update() { }
    }
}
