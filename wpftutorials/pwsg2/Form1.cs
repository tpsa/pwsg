using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace pwsg2
{
    public partial class Form1 : Form
    {
        Color[,] tab;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult res = MessageBox.Show("Czy na pewno chcesz wyjść z programu?", "Zamknięcie programu", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question );
            if (res != DialogResult.Yes)
                e.Cancel = true;
            
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            //MessageBox.Show("sss");
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Plansza_Enter(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
          
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult res = colorDialog1.ShowDialog();
            if (res == DialogResult.OK)
            {
                button3.BackColor = colorDialog1.Color;
                pictureBox1.Invalidate();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult res = colorDialog2.ShowDialog();
            if (res == DialogResult.OK)
            {
                button4.BackColor = colorDialog2.Color;
                pictureBox1.Invalidate();
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (tab == null) return;
            for (int i = 0; i < numericUpDown2.Value; i++)
                for (int j = 0; j < numericUpDown3.Value; j++)
                {
                    if ((i + j) % 2 == 0)
                        tab[i, j] = colorDialog1.Color;
                    else
                        tab[i, j] = colorDialog2.Color;
                }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                // Szachownica
                //pictureBox1.Image.
            }
            else
            {
                // pusta
                pictureBox1.BackColor = Color.Black;
            }
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown4.Value * numericUpDown3.Value >= 400)
            {
                numericUpDown3.Value = 400 / numericUpDown4.Value;
            }
            if (numericUpDown4.Value * numericUpDown2.Value >= 400)
            {
                numericUpDown2.Value = 400 / numericUpDown4.Value;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pictureBox1.Invalidate();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Rectangle box = new Rectangle(0, 0, 400, 400);
            Rectangle rect = new Rectangle(0,0,10,10);
            e.Graphics.FillRectangle(new SolidBrush(colorDialog2.Color), box);
            e.Graphics.FillRectangle(new SolidBrush(colorDialog1.Color), rect);
            /*
            for (int i = 0; i <numericUpDown2.Value; i++)
                for (int j = 0; j < numericUpDown3.Value; j++)
                {

                    //Rectangle r = new Rectangle();
                    Rectangle r = new Rectangle((int)i * (int)numericUpDown4.Value, (int)j * (int)numericUpDown4.Value, (int)numericUpDown4.Value, (int)numericUpDown4.Value);
                    e.Graphics.FillRectangle(new SolidBrush(tab[i, j]),r);
                }
         */    
        }
    }
}
