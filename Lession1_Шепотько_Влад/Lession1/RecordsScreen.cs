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
    public partial class RecordsScreen : Form
    {
        public RecordsScreen()
        {
            InitializeComponent();

            MaximizeBox = false;
            MinimizeBox = false;

            label1.Parent = pictureBox1;
            label2.Parent = pictureBox1;
            label3.Parent = pictureBox1;
            label4.Parent = pictureBox1;

            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;

            label2.Text = $"{Records.date1}\n\n{Records.date2}\n\n{Records.date3}\n\n{Records.date4}"; // Загружаем данные времени игр
            label3.Text = $"{Records.score1}\n\n{Records.score2}\n\n{Records.score3}\n\n{Records.score4}";// Загружаем данные полученных очков

            label4.MouseEnter += new EventHandler(label4_MouseEnter);
            label4.MouseLeave += new EventHandler(label4_MouseLeave);

            FormBorderStyle = FormBorderStyle.None;
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void label4_MouseEnter(object sender, EventArgs e)
        {
            label4.ForeColor = Color.Chocolate;
        }
        private void label4_MouseLeave(object sender, EventArgs e)
        {
            label4.ForeColor = Color.DarkOrange;
        }
    }
}
