using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lession1
{
    public partial class SplashScreen : Form
    {
        public static GameScene gameFormActive;
        
        SoundPlayer menuSong = new SoundPlayer("music/menu.wav");

        Image play = Image.FromFile("Menu/play.png");
        Image playOn = Image.FromFile("Menu/playON.png");

        Image cup = Image.FromFile("Menu/cup.png");
        Image cupOn = Image.FromFile("Menu/cupON.png");

        Image exit = Image.FromFile("Menu/exit.png");
        Image exitOn = Image.FromFile("Menu/exitON.png");

        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new SplashScreen());
        }
        // Кроме инициализации компонентов также настраивает кнопки меню на смену изображение при наведении
        public SplashScreen()
        {
            menuSong.PlayLooping();
            InitializeComponent();
            MaximizeBox = false;
            MinimizeBox = false;
            label1.Parent = pictureBox4;
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;

            pictureBox1.Image = play;
            pictureBox1.Parent = pictureBox4;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.MouseEnter += new EventHandler(pictureBox1_MouseEnter);
            pictureBox1.MouseLeave += new EventHandler(pictureBox1_MouseLeave);

            pictureBox2.Image = cup;
            pictureBox2.Parent = pictureBox4;
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.MouseEnter += new EventHandler(pictureBox2_MouseEnter);
            pictureBox2.MouseLeave += new EventHandler(pictureBox2_MouseLeave);

            pictureBox3.Image = exit;
            pictureBox3.Parent = pictureBox4;
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.MouseEnter += new EventHandler(pictureBox3_MouseEnter);
            pictureBox3.MouseLeave += new EventHandler(pictureBox3_MouseLeave);
        }
        // Вызов формы с игрой
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            try
            {
                Hide();
                GameSceneStart(800, 600, FormStartPosition.CenterScreen);
            }

            catch (ArgumentOutOfRangeException outRange) 
            {
                MessageBox.Show(outRange.Message);
                Environment.Exit(0);
            }
        }
        //Создание игровой сцены c проверкой на разрешение окна
        public void GameSceneStart(int width, int height, FormStartPosition stPos)
        {
            if (width != 800) throw new ArgumentOutOfRangeException("width", "Ширина окна должна быть строго 800 пикселей!");
            else if (height != 600) throw new ArgumentOutOfRangeException("height", "Высота окна должна быть строго 600 пикселей!");
            else
            {
                menuSong.Stop();
                GameScene game = new GameScene();
                gameFormActive = game;
                game.Width = width;
                game.Height = height;
                game.StartPosition = stPos;
                Settings.Init(game);
                Game.Play();
                game.ShowDialog();
                GameInterface.Load();
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Records.WriteRecords("Logs/records.ini");
            RecordsScreen rs = new RecordsScreen();
            Hide();
            rs.Show();
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
