using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;

namespace Lession1
{
    class Game : Settings
    {
        public static int laserNum;
        public static int laserLast;

        public static Settings player;
        public static Settings[] laser;

        public static Settings[] allStars;
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

        public static void PlayerSet()
        {
            player = new Player(new Point(-40, 300), new Point(0), new Size(350, 175));
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
                if (numObj == 4)
                {
                    objs[i] = new Laser(new Point(cordX, cordY), new Point(speed), new Size(25, size));
                }
            }
        }
        public static void Load()
        {
            AddGameObject(1, ref littleStars, 50, 0, 160, 5, 0, 120, 5, 2, 3, 1, 1, 2, 1);
            AddGameObject(2, ref stars, 50, 0, 160, 5, 0, 120, 5, 2, 3, 1, 1, 5, 5);
            AddGameObject(3, ref asteroids, 20, 800, 801, 1, 1, 20, 30, 5, 11, 1, 8, 17, 5);
            AddGameObject(4, ref laser, 20, 1200, 1201, 1, 1200, 1201, 1, 10, 11, 1, 10, 11, 1);

            allStars = littleStars.Concat(stars).ToArray();
            PlayerSet();
        }
        public static void Drawler()
        {
            Buffer.Graphics.Clear(Color.Black);
            foreach (Settings obj in allStars) obj.Draw();

            if(Player.isShooting)
            {
                player.Draw();
                foreach (Settings obj in laser) obj.Draw();
                ShootCheck();
                player.Draw(Player.isShooting);
            }
            else
            {
                ShootCheck();
                foreach (Settings obj in laser) obj.Draw();
                player.Draw();
                player.Draw(Player.isShooting);
            }
            foreach (Settings obj in asteroids) obj.Draw();
            try
            {
                Buffer.Render();
            }
            catch
            {
                Environment.Exit(0);
            }
            foreach (Settings obj in allStars) obj.Update();
            foreach (Settings obj in laser) obj.Update();
            player.Update();
            foreach (Settings obj in asteroids) obj.Update();
        }
        public static void ShootCheck()
        {
            laser[laserNum].Draw(Player.isShooting);
            if (Player.isShooting)
            {
                if (laserNum == 19) laserNum = 0;
                laserNum++;
                Player.isShooting = false;
            }
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
