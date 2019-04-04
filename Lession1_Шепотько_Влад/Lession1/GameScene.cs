using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media; 
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;

namespace Lession1
{
    public partial class GameScene : Form
    {
        MediaPlayer laserSong = new MediaPlayer();

        EndGame endGame = new EndGame();
        public static bool over = false;

        public GameScene()
        {
            InitializeComponent();
            KeyDown += Form1_KeyDown;
            MaximizeBox = false;
            MinimizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Settings.Timer(EndGameForm, 1);
            
        }


        // задаем настройки управления персонажем
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    if (!Player.isDeath)
                    {
                        Player.playerPosY -= 10;
                    }
                    break;
                case Keys.S:
                    if (!Player.isDeath)
                    {
                        Player.playerPosY += 10;
                    }
                    break;
                case Keys.Space:
                    if (Laser.counter == Laser.laserTime)
                    {
                        laserSong.Open(new Uri("music/laser.wav", UriKind.Relative));
                        laserSong.Volume = 0.2;
                        laserSong.Play();
                        Player.isShooting = true;
                    }
                    break;
            }
        }

        public void EndGameForm(object sender, EventArgs e)
        {
            if (Player.openEndScene)
            {
                over = true;
                Player.openEndScene = false;
                IsMdiContainer = true;
                endGame.MdiParent = this;
                endGame.TopLevel = false;
                endGame.FormBorderStyle = FormBorderStyle.None;
                endGame.Dock = DockStyle.Fill;
                endGame.Visible = true;
                endGame.ControlBox = false;
                endGame.Text = String.Empty;
                endGame.Show();
            }
        }
    }
}
