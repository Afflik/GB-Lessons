using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lession1
{
    public partial class EndGame : Form
    {

        int x;
        int y;
        public EndGame()
        {
            InitializeComponent();
            pictureBox1.Location = new Point(-800, 206);
            pictureBox2.Visible = false;
            x = pictureBox1.Location.X;
            y = pictureBox1.Location.Y;
            Settings.Timer(CatAnim, 30, true);
        }

        public void CatAnim(object sender, EventArgs e)
        {
            if (GameScene.over)
            {
                if (x != -160)
                {
                    x += 5;
                }
                else
                {
                    pictureBox2.Visible = true;
                    GameScene.over = false;
                }
                pictureBox1.Location = new Point(x, y);
            }
        }
    }
}
