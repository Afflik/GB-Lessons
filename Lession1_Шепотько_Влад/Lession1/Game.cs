using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;
using System.Diagnostics;

namespace Lession1
{
    class Game : Settings
    {
        public static Settings player;
        public static Settings laser;

        public static List<Settings> allStars = new List<Settings>(); // лист для объединения обычных и маленьких звезд
        public static List<Settings> asteroids = new List<Settings>();
        public static List<Settings> littleStars = new List<Settings>();
        public static List<Settings> cakes = new List<Settings>();
        public static List<Settings> stars = new List<Settings>();

        public Game(Point pos, Point dir, Size size) : base(pos, dir, size) { }

        public static void Play() // основная функция для запуска игры
        {
            Load();
            Timer(Drawler,30,true);
        } 

        public static void PlayerSet() // Игрок
        {
            player = new Player(new Point(-100, 300), new Point(0), new Size(350, 175));
        }

        public static void CreateLaser() // Лазер
        {
            laser = new Laser(new Point(-100, 300), new Point(0), new Size(30, 10));
        }

        // AddGameObject создает листы объектов по нужным параметрам, добавил туда кастомные исключения
        public static void AddGameObject(int numObj,ref List<Settings> objs, int lenght, int minX, int maxX, int factorX,
                                      int minY, int maxY, int factorY, int speedMin, int speedMax,
                                      int factorSpeed, int sizeMin, int sizeMax, int factorSize)
        {
            for (int i = 0; i < lenght; i++)
            {
                if(speedMax > 15 || factorSpeed > 3) throw new CustomException($"Задана слишком большая скорость для объекта!");
                if (speedMax < 0 ) throw new CustomException($"Скорость объекта не может быть меньше нуля!");
                if (sizeMax > 20 || factorSize > 15) throw new CustomException($"Задан слишком большой размер для объекта!");
                if (sizeMin < 1) throw new CustomException($"Размер объекта не может быть меньше единицы");
                int cordX = rng.Next(minX, maxX) * factorX;
                int cordY = rng.Next(minY, maxY) * factorY;
                int speed = rng.Next(speedMin, speedMax) * factorSpeed;
                int size = rng.Next(sizeMin, sizeMax) * factorSize;

                if (numObj == 1)
                {
                    objs.Add(new LittleStars(new Point(cordX, cordY), new Point(speed), new Size(size, size)));
                }
                if (numObj == 2)
                {
                    objs.Add(new Stars(new Point(cordX, cordY), new Point(speed), new Size(size, size)));
                }
                if (numObj == 3)
                {
                    objs.Add(new Asteroids(new Point(cordX, cordY), new Point(speed), new Size(size, size)));
                }
                if (numObj == 4)
                {
                    objs.Add(new Cakes(new Point(cordX, cordY), new Point(speed), new Size(size, size)));
                }
            }
        }

        //Load используется как место для создания объектов
        public static void Load()
        {
            try
            {
                AddGameObject(1,ref littleStars, 50, 0, 160, 5, 0, 120, 5, 2, 3, 1, 1, 2, 1);
                AddGameObject(2,ref stars, 50, 0, 160, 5, 0, 120, 5, 2, 3, 1, 1, 5, 5);
                AddGameObject(3,ref asteroids, 20, 800, 801, 1, 1, 20, 30, 4, 8, 1, 10, 16, 5);
                AddGameObject(4,ref cakes, 2, 750, 850, 1, 1, 701, 1, 8, 9, 1, 14, 15, 5);
                for (int i = 0; i < littleStars.Count; i++)
                {
                    allStars.Add(littleStars[i]);
                }
                for (int i = 0; i < stars.Count; i++)
                {
                    allStars.Add(stars[i]);
                }
                PlayerSet();
                CreateLaser();
            }
            catch(CustomException error)
            {
                MessageBox.Show(error.Message);
                Environment.Exit(0);
            }
        }
        //Render рисует картинку если открыта игра и закрывает приложение если закрыть игру
        public static void Render()
        {
            _time = DateTime.Now.ToLongTimeString(); // Системное время
            if (Form.ActiveForm == SplashScreen.gameFormActive) Buffer.Render();
            else Environment.Exit(0);
        }
        //Drawler рисует все объекты и обновляет их местоположение
        private static void Drawler(object sender, EventArgs e)
        {
            Buffer.Graphics.Clear(Color.Black);
            if (!Player.isDied)
            {
                foreach (Settings obj in allStars) obj.Draw();
                laser.Draw();
                if (!Player.isDeath) player.Draw();
                foreach (Settings obj in cakes) obj.Draw();
                foreach (Settings obj in asteroids) obj.Draw();
                GameInterface.Load(); //Служит для загрузки интерфейса
                if (Player.isDeath) player.Draw();

                Render();

                foreach (Settings obj in allStars) obj.Update();
                player.Update();
                for (int i = 0; i < cakes.Count; i++) // здесь хилки проверяют столковения с игроком
                {
                    cakes[i].Update();
                    if (player.Collision(cakes[i]))
                    {
                        Player.cooldownHeal++;
                        if (Player.cooldownHeal == 1) Player.isTakeHealth = true;
                        cakes[i].Update(true);
                    }
                    if (laser.Collision(cakes[i]))
                    {
                        cakes[i].Update(true);
                    }
                }
                laser.Update();
                for (int i = 0; i < asteroids.Count; i++) // здесь имбирные астероиды проверяют столкновения с хилками, игроком и лазером
                {
                    asteroids[i].Update();
                    if (laser.Collision(asteroids[i]) && Player.isShooting) // Проверяем активность и взаимодействие лазера с астероидом
                    {
                        asteroids[i].Update(true);
                    }
                    if (player.Collision(asteroids[i]))
                    {
                        Player.cooldownDmg++;
                        if (Player.cooldownDmg == 1) Player.isTakeDmg = true;
                        asteroids[i].Update(true);
                    }
                    for (int j = 0; j < cakes.Count; j++)
                    {
                        if (cakes[j].Collision(asteroids[i])) cakes[j].Update(true);
                    }
                }
            }
            else
            {
                player.Draw();
                Render();
                player.Update();
            }
        }


        public override void Draw() { }

        public override void Update() { }
        
    }
}
