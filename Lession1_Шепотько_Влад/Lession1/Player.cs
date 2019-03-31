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
        public static int cooldownHeal = 0;
        public static int cooldownDmg = 0;


        protected static Image player;
        protected static List<Image> playerAnim = new List<Image> {Image.FromFile("Player/player1.png"), Image.FromFile("Player/player2.png"),
                                                                   Image.FromFile("Player/player3.png"),Image.FromFile("Player/player4.png") };
        protected static List<Image> attackAnim = new List<Image> {Image.FromFile("Player/attack.png"), Image.FromFile("Player/attack2.png"),
                                                                   Image.FromFile("Player/attack3.png"),Image.FromFile("Player/attack4.png") };

        public static Point playerPos;
        public static int playerPosY = 300;
        public static bool isTakeDmg = false; // переменная для проверки получения хила
        public static bool isTakeHealth = false; // переменная для проверки получения урона
        public static bool isShooting = false; // переменная на проберку был ли сделан выстрел

        public Player(Point pos, Point dir, Size size) : base(pos, dir, size) { }


        public static void HealthBar() // создает  хилбар чтобы прибавлялось и уменьшалось хп игрока
        {
            if (cooldownHeal != 0) cooldownHeal--;
            if (cooldownHeal == 0) cooldownHeal = 0;
            if (cooldownDmg != 0) cooldownDmg--;
            if (cooldownDmg == 0) cooldownDmg = 0;

            if (isTakeDmg && cooldownDmg == 0) // Следим за потерей хп 
            {
                isTakeDmg = false;
                cooldownDmg = 30;
                temp[n] = '⬜';
                if (n != 0) n--;
                healthBar = new string(temp);
            }
            if (isTakeHealth && cooldownHeal == 0) // Проверяем получили ли хил с кексика
            {
                isTakeHealth = false;
                cooldownHeal = 30;
                temp[n] = '⬛';
                if (n != temp.Length-1) n++;
                healthBar = new string(temp);
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