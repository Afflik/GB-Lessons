using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;

namespace Lession1
{
    class Player : Settings
    {
        public static string healthBar = "⬛⬛⬛⬛";
        protected static char[] temp = healthBar.ToCharArray(); // Создаю массив строчки чтобы менять состояния индикатора
        public static int n = temp.Length-1;
        //cooldownHeal и cooldownDmg нужны для создания кулдауна в промежутках получения урона или лечения
        public static int cooldownHeal = 0; // переменная для проверки получения хила
        public static int cooldownDmg = 0; // переменная для проверки получения урона


        protected static Image player;
        protected static List<Image> playerAnim = new List<Image> {Image.FromFile("Player/player1.png"), Image.FromFile("Player/player2.png"),
                                                                   Image.FromFile("Player/player3.png"),Image.FromFile("Player/player4.png") };
        protected static List<Image> attackAnim = new List<Image> {Image.FromFile("Player/attack.png"), Image.FromFile("Player/attack2.png"),
                                                                   Image.FromFile("Player/attack3.png"),Image.FromFile("Player/attack4.png") };

        public static Point playerPos;
        public static int playerPosY = 300;
        public static bool isTakeDmg = false;
        public static bool isTakeHealth = false;
        public static bool isShooting = false; // переменная на проберку был ли сделан выстрел

        public Player(Point pos, Point dir, Size size) : base(pos, dir, size) { }


        public static void HealthBar() // создает  хилбар чтобы прибавлялось и уменьшалось хп игрока
        {
            if (isTakeDmg && n >= 0) // Следим за потерей хп 
            {
                temp[n] = '⬜';
                healthBar = new string(temp);
                isTakeDmg = false;
                if(n > 0) n--;
            }
            if (isTakeHealth && n < temp.Length) // Проверяем получили ли хил с кексика
            {
                if (n < temp.Length - 1) n++;
                temp[n] = '⬛';
                healthBar = new string(temp);
                isTakeHealth = false;
            }
        }

        public override void Draw(bool shooted) // проверяем сделан ли выстрел, чтобы при выстреле снаряд начал движение
        {
            if (!shooted)
            {
                playerPos.X = Pos.X;
                playerPos.Y = playerPosY;
            }
        }
        public override void Draw()
        {
            HealthBar();
            playerPos.X = Pos.X;
            playerPos.Y = playerPosY;
            if (!isShooting) 
            {
                player = Animator(playerAnim, 3); //функция в Settings для создания анимации
            }
            else
            {
                player = Animator(attackAnim, 3); //функция в Settings для создания анимации
            }
            Buffer.Graphics.DrawImage(player, playerPos.X, playerPosY, Size.Width, Size.Height);
        }
        public override void Update()
        {
            if (playerPosY > 450) playerPosY = 449;
            if (playerPosY < -40) playerPosY = -39;
        }

        public override Rectangle Rect => new Rectangle(new Point(playerPos.X+250,playerPos.Y+80), new Size(20,20));
        public override bool Collision(ICollision o) => o.Rect.IntersectsWith(this.Rect);
    }
}