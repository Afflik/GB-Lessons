using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

namespace Lession1
{

    abstract class Settings: ICollision
    {

        protected static string _time; // Системное время

        protected static bool isCookie = false;
        protected static bool isCake = false;

        public static Random rng = new Random();

        protected Point Pos;
        protected Point Dir;
        protected Size Size;

        public Settings() { }
        public Settings(Point pos, Point dir, Size size)
        {
            Pos = pos;
            Dir = dir;
            Size = size;
        }

        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;

        public static int Width { get; set; }
        public static int Height { get; set; }

        public virtual int time { get; set; }  // таймер для анимации
        public virtual int anim { get; set; }  // обозначает кадр анимации 

        public static void Timer(EventHandler method, int interval, bool isActive) // Таймер
        {
            Timer timer = new Timer { Interval = interval };
            timer.Start();
            timer.Tick += method;
            timer.Enabled = true;
        }

        public Image Animator(List<Image> imgArray, int slowFactory) // создания анимации из списка изображений
        {
            time++;
            if (time % slowFactory == 0) anim++;
            if (anim == imgArray.Count) anim = 0;
            return imgArray[anim];
        }

        public static void Init(Form form)
        {          
            Graphics g;
            _context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();
            Width = form.ClientSize.Width;
            Height = form.ClientSize.Height;
            Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));
        }

        public virtual Rectangle Rect => new Rectangle(Pos,Size);
        public virtual bool Collision(ICollision o) => o.Rect.IntersectsWith(this.Rect);

        public virtual void Draw(bool _bool) { }
        public abstract void Draw();
        public abstract void Update();
        public virtual void Update(bool _bool) { }
    }
}
