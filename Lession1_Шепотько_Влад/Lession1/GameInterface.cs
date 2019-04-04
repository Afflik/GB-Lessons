using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Lession1
{
    class GameInterface : Settings
    {
        public static int score = 0;
        // основная функция для вывода текста на экран
        public static void Text(string _text,int size, int _x,int _y, SolidBrush _brush)
        {
            Buffer.Graphics.DrawString(_text, new Font("Arial", size), _brush, _x, _y);
        }

        public static void Load()
        {
            Text("HEALTH: ", 15, 0, 7, new SolidBrush(Color.WhiteSmoke));
            if (Player.n == 3) Text(Player.healthBar, 25, 85, 0, new SolidBrush(Color.LightSkyBlue));
            else Text(Player.healthBar, 25, 85, 0, new SolidBrush(Color.LightSkyBlue));

            Text("LASER: ",15, 10, 38, new SolidBrush(Color.WhiteSmoke));
            if(Laser.counter == 40) Text(Laser.laserBar, 12, 85, 40, new SolidBrush(Color.Red));
            else Text(Laser.laserBar, 12, 85, 40, new SolidBrush(Color.DarkRed));

            Text("SCORE: ", 16, 0, 69, new SolidBrush(Color.WhiteSmoke));
            Text(score.ToString(), 15, 85, 69, new SolidBrush(Asteroids.color));
        }

        public override void Update() { }
        public override void Draw() { }
    }
}
