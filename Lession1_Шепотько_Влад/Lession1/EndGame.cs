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


            label3.MouseEnter += new EventHandler(label3_MouseEnter);
            label3.MouseLeave += new EventHandler(label3_MouseLeave);

            label4.MouseEnter += new EventHandler(label4_MouseEnter);
            label4.MouseLeave += new EventHandler(label4_MouseLeave);

            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;

            pictureBox1.Location = new Point(-800, 206);
            pictureBox2.Visible = false;
            x = pictureBox1.Location.X;
            y = pictureBox1.Location.Y;
            Settings.Timer(CatAnim, 30);
        }

        public void CatAnim(object sender, EventArgs e)
        {
            if (GameScene.over)
            {
                if (x != -160)
                {
                    x += 4;
                }
                else
                {
                    label1.Visible = true;
                    label2.Visible = true;
                    label3.Visible = true;
                    label4.Visible = true;
                    label5.Visible = true;
                    pictureBox2.Visible = true;

                    label2.Text = GameInterface.score.ToString();
                    GameScene.over = false;
                }
                pictureBox1.Location = new Point(x, y);
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        
        private void label3_MouseEnter(object sender, EventArgs e)
        {
            label3.ForeColor = Color.DarkOrange;
        }
        private void label3_MouseLeave(object sender, EventArgs e)
        {
            label3.ForeColor = Color.Khaki;
        }

        private void label4_MouseEnter(object sender, EventArgs e)
        {
            label4.ForeColor = Color.DarkOrange;
        }
        private void label4_MouseLeave(object sender, EventArgs e)
        {
            label4.ForeColor = Color.Khaki;
        }
    }
}
