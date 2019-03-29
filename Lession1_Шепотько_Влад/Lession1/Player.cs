using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;

namespace Lession1
{
    class Player : Settings
    {

        protected static Image player;
        protected static Image attack = Image.FromFile("Player/attack.png");
        protected static Image[] playerAnim = new Image[] {Image.FromFile("Player/player1.png"), Image.FromFile("Player/player2.png"),
                                                Image.FromFile("Player/player3.png"),Image.FromFile("Player/player4.png") };
        int star = rng.Next(1, 5);

        public static Point playerPos;
        public static int playerPosY = 300;
        public static bool isShooting = false;
        int timer;
        int anim;

        public Player(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }
        public override void Draw(bool shooted)
        {
            if (!shooted)
            {
                playerPos.X = Pos.X;
                playerPos.Y = playerPosY;
            }
        }
        public override void Draw()
        {
            if (!isShooting)
            {
                timer++;
                if (timer % 2 == 0) anim++;
                if (anim == 4) anim = 0;
                player = playerAnim[anim];
                Buffer.Graphics.DrawImage(player, playerPos.X, playerPosY, Size.Width, Size.Height);
            }
            else
            {
                player = attack;
            }
            Buffer.Graphics.DrawImage(player, playerPos.X, playerPosY, Size.Width, Size.Height);
        }
        public override void Update()
        {
            if (playerPosY > 450) playerPosY = 449;
            if (playerPosY < -40) playerPosY = -39;
        }
    }
}