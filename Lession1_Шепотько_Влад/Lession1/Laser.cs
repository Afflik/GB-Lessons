using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace Lession1
{
    class Laser : Player
    {
        bool fireOn = false;
        Point _pos;
        Size _size;
        int firstOff;

        protected Image laser = Image.FromFile("Player/laser.png");

        public Laser(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }
        public override void Draw(bool shooted)
        {
            if (shooted == true) fireOn = shooted;
            if (!fireOn)
            {
                _pos.X = playerPos.X + 300;
                _pos.Y = playerPos.Y + 80;
                _size.Width = 1;
                _size.Height = 1;
            }
            else
            {
                _size.Width = 25;
                _size.Height = 10;
            }
        }
        public override void Draw()
        {
            firstOff++;
            if(firstOff == 1)
            {
                _pos.X = 1000;
                _pos.Y = 1000;
            }
            Buffer.Graphics.DrawImage(laser, _pos.X, _pos.Y, _size.Width, _size.Height);
        }
        public override void Update()
        {
            _pos.X += 10;
            if (_pos.X > 100) fireOn = false;
        }

    }
}