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
    public partial class GameScene : Form
    {
        public GameScene()
        {
            InitializeComponent();
            KeyDown += Form1_KeyDown;
        }

        // задаем настройки управления персонажем
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    Player.playerPosY -= 10;
                    break;
                case Keys.S:
                    Player.playerPosY += 10;
                    break;
                case Keys.Space:
                    if (Laser.counter == Laser.laserTime)
                    {
                        Player.isShooting = true;
                    }
                    break;
            }
        }
    }
}
