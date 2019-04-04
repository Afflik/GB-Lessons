using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace Lession1
{
    class Laser : Player
    {
        public static string laserBar = "|██████████|";
        protected static char[] temp = laserBar.ToCharArray(); // Создаю массив строчки чтобы менять состояния индикатора
        protected static int n = temp.Length-1;
        public static int laserTime = 40; // Длительнось испускания лазера
        public static double counter = laserTime; // Счетчик 

        // laserTimePoint и laserTimeStep переменные для реализации индикатора в LaserBar
        static int laserTimePoint = laserTime;
        static int laserTimeStep = laserTime / (temp.Length - 1 );

        protected static Size _size;
        
        protected static Point _pos;

        protected static int speed;

        GameInterface gInt = new GameInterface();

        protected Image laser = Image.FromFile("Player/laser.png");
        public Laser(Point pos, Point dir, Size size) : base(pos, dir, size) { }

        // LaserBar рисует индикатор состояния лазера
        public static void LaserBar()
        {
            if (isShooting) // Индикатор пустеет и после кончается лазер 
            {
                counter--;
                if (counter <= laserTimePoint - laserTimeStep && n > 1)
                {
                    n--;
                    temp[n] = '░';
                    laserTimePoint = Convert.ToInt32(counter);
                    laserBar = new string(temp);
                }
                if (counter == 0)
                {
                    counter = 0;
                    laserTimePoint = Convert.ToInt32(counter);
                    isShooting = false;
                }
            }
            else if(counter < laserTime) // Заряд лазера восстанавливается
            {
                counter ++;
                if (counter >= laserTimePoint + laserTimeStep && n < temp.Length-1)
                {
                    temp[n] = '█';
                    laserTimePoint = Convert.ToInt32(counter);
                    laserBar = new string(temp);
                    n++;
                }
                if(counter == 40)
                {
                    laserTimePoint = laserTime;
                    laserTimeStep = laserTime / (temp.Length - 1 );
                }
            }
        }
        public override void Draw()
        {
            _size = Size;
            LaserBar();
            if (!isShooting)
            {
                _pos = new Point(1000, 1000);
            }
            Buffer.Graphics.DrawImage(laser, _pos.X, _pos.Y, Size.Width, Size.Height);
        }
        public override void Update()
        {
            _size = Size;
            _pos = new Point(playerPos.X + 300 + speed, playerPos.Y + 85);

            if (Size.Width < 700 && isShooting) // прорисовка дальности лазера
            {
                Size.Width += 40;
                speed -= 3;
            }
            if (Size.Width > 0 && !isShooting) // возврат лазера
            {
                Size.Width -= 40;
                speed += 3;
            }
        }

        public override Rectangle Rect => new Rectangle(_pos, _size);
        public override bool Collision(ICollision o) => o.Rect.IntersectsWith(this.Rect); 
    }
}