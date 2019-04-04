using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;

namespace Lession1
{
    class Player : Settings
    {
        public static string healthBar = "⬛⬛⬛⬛";
        public static char[] temp = healthBar.ToCharArray(); // Создаю массив строчки чтобы менять состояния индикатора
        public static int n = temp.Length-1;
        //cooldownHeal и cooldownDmg нужны для создания кулдауна в промежутках получения урона или лечения
        public static int cooldownHeal = 0; // переменная для проверки получения хила
        public static int cooldownDmg = 0; // переменная для проверки получения урона
        public static int deathCount = 4;

        protected static Image player;
        protected static List<Image> playerAnim = new List<Image> {Image.FromFile("Player/player1.png"), Image.FromFile("Player/player2.png"),
                                                                   Image.FromFile("Player/player3.png"), Image.FromFile("Player/player4.png") };

        protected static List<Image> attackAnim = new List<Image> {Image.FromFile("Player/attack.png"),  Image.FromFile("Player/attack2.png"),
                                                                   Image.FromFile("Player/attack3.png"), Image.FromFile("Player/attack4.png") };

        protected static List<Image> death = new List<Image> {Image.FromFile("Player/Death/death1.png"), Image.FromFile("Player/Death/death2.png"),
                                                              Image.FromFile("Player/Death/death5.png"), Image.FromFile("Player/Death/death6.png"),
                                                              Image.FromFile("Player/Death/death7.png"), Image.FromFile("Player/Death/death9.png"),
                                                              Image.FromFile("Player/Death/death10.png"),Image.FromFile("Player/Death/death11.png"),
                                                              Image.FromFile("Player/Death/death12.png"),Image.FromFile("Player/Death/death13.png"),
                                                              Image.FromFile("Player/Death/death14.png"),Image.FromFile("Player/Death/death15.png"),
                                                              Image.FromFile("Player/Death/death16.png"),Image.FromFile("Player/Death/death17.png")};
        public static Point playerPos;
        public static int playerPosY = 300;

        protected static int startDebug;

        public static bool isDeath = false;
        public static bool isDied = false;
        public static bool openEndScene = false;

        public static bool isTakeDmg = false;
        public static bool isTakeHealth = false;
        public static bool isShooting = false; // переменная на проберку был ли сделан выстрел

        public Player(Point pos, Point dir, Size size) : base(pos, dir, size) { }


        public static void HealthBar() // создает  хилбар чтобы прибавлялось и уменьшалось хп игрока
        {
            if (n == -1) isDeath = true;
            if (isTakeDmg && n >= 0) // Следим за потерей хп 
            {
                temp[n] = '⬜';
                healthBar = new string(temp);
                isTakeDmg = false;
                n--;
                if (n == -1) isDeath = true;
            }
            if (isTakeHealth && n < temp.Length) // Проверяем получили ли хил с кексика
            {
                if (n < temp.Length - 1) n++;
                temp[n] = '⬛';
                healthBar = new string(temp);
                isTakeHealth = false;
            }
        }

        public void Dead()
        {
            player = Animator(death, 5);
            if (deathCount == 4)
            {
                playerPos.X += 80;
                playerPosY -= 20;
                deathCount += 1;
            }
            if (deathCount < death.Count && player == death[deathCount])
            {
                if (deathCount > 4)
                {
                    playerPos.X = 0;
                    playerPosY = 0;
                }
                Size = new Size(800, 600);
                if (deathCount == 8) isDied = true;
                if (deathCount == death.Count - 1) openEndScene = true;
                 deathCount++;
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
        public static void Debugger() //Вывод лога в консоль и запись в файл
        {
            using (StreamWriter log = File.AppendText("Logs/log.ini"))
            {
                string _start = "\nНачинается игра!\n";
                string _heal = $"{_time}: " + ((n == 2) ? "Кексик полностью залечил лицо котику." : "Кексик подлатал личико котика на одно HP.");
                string _dmg = (n == 0) ? "\nИгра закончена!\n" : $"{_time}: Печенька врезалась в котика и снесла ему четверть лица, осталось HP: {Player.n}";
                string _score = $"\nСчет за игру: {GameInterface.score}.";

                Action score = () => { Debug.WriteLine(_score); log.WriteLine(_score); };
                Action start = () => { Debug.WriteLine(_start); log.WriteLine(_start); };
                Action damage = () => { Debug.WriteLine(_dmg); log.WriteLine(_dmg); };
                Action heal = () => { Debug.WriteLine(_heal); log.WriteLine(_heal); };

                if (isTakeDmg && n == 0)
                {
                    Records.AddRecord("Logs/Records.ini", GameInterface.score);
                    score();
                }
                if (startDebug == 0)
                {
                    startDebug++;
                    start();
                }
                if (isTakeDmg && n >= 0)
                {
                    damage();
                }
                if (isTakeHealth && n < temp.Length-1)
                {
                    heal();
                }
            }

        }
        public override void Draw()
        {
            if(isDeath)Dead();
            else
            {
                Debugger(); // Запись лога
                HealthBar(); // Вывод хилбара
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