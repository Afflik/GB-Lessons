using System;
using System.Windows.Forms;
using System.Drawing;

namespace Lession1
{
    abstract class Settings
    {
        public static Random rng = new Random();

        protected Point Pos;
        protected Point Dir;
        protected Size Size;

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

        public static void Init(Form form)
        {          
            Graphics g;
            _context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();
            Width = form.ClientSize.Width;
            Height = form.ClientSize.Height;
            Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));
        }

        public abstract void Draw();
        public abstract void Update();
    }
}
