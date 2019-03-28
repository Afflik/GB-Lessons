using Lession1.Properties;
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
    public partial class SplashScreen : Form
    {
        Image title = Image.FromFile("title.png");

        Image play = Image.FromFile("play.png");
        Image playOn = Image.FromFile("playON.png");

        Image cup = Image.FromFile("cup.png");
        Image cupOn = Image.FromFile("cupON.png");

        Image exit = Image.FromFile("exit.png");
        Image exitOn = Image.FromFile("exitON.png");

        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new SplashScreen());
        }
        public SplashScreen()
        {
            InitializeComponent();
            pictureBox4.Image = title;
            pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;

            pictureBox1.Image = play;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.MouseEnter += new EventHandler(pictureBox1_MouseEnter);
            pictureBox1.MouseLeave += new EventHandler(pictureBox1_MouseLeave);

            pictureBox2.Image = cup;
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.MouseEnter += new EventHandler(pictureBox2_MouseEnter);
            pictureBox2.MouseLeave += new EventHandler(pictureBox2_MouseLeave);

            pictureBox3.Image = exit;
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.MouseEnter += new EventHandler(pictureBox3_MouseEnter);
            pictureBox3.MouseLeave += new EventHandler(pictureBox3_MouseLeave);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Form game = new Form { Width = 800, Height = 600 };
            game.StartPosition = FormStartPosition.Manual;
            game.Location = this.Location;
            Settings.Init(game);
            Game.Play();
            Hide();
            game.ShowDialog();
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }


        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            pictureBox1.Image = playOn;
        }
        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Image = play;
        }

        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            pictureBox2.Image = cupOn;
        }
        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.Image = cup;
        }

        private void pictureBox3_MouseEnter(object sender, EventArgs e)
        {
            pictureBox3.Image = exitOn;
        }
        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            pictureBox3.Image = exit;
        }
    }
}
