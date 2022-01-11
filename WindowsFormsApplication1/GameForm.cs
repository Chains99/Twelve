using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TwelveLogic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;


namespace Visual
{
    public partial class Twelve : Form
    {
        TwelveLogic.Twelve board;
        int pos_x = -1;
        int pos_y = -1;
       

        public Twelve()
        {
            
            InitializeComponent();
            
            board = TwelveLogic.Twelve.LoadGame();

            label8.Text = board.best.ToString();

            progressBar1.Maximum = 18;
            if (board.modo) { progressBar1.Show();
                progressBar1.Value = board.count;
            }
            else
                progressBar1.Value = 0;
        }
      

        private void pictureBoxTablero_Paint(object sender, PaintEventArgs e)
        {
            Graphics graf = e.Graphics;
           // SolidBrush brush = new SolidBrush(Color.FromArgb(255, 204, 192, 179));
           // graf.FillRectangle(brush, e.ClipRectangle);

            StringFormat middle = new StringFormat();
            middle.LineAlignment = StringAlignment.Center;
            middle.Alignment = StringAlignment.Center;

            float heigth = (float)pictureBoxTablero.Height / board.heigth;
            float width = (float)pictureBoxTablero.Width / board.width;

            for (int i = 0; i < board.heigth; i++)
                for (int j = 0; j < board.width; j++)
                {
                    float x = j * width;
                    float y = i * heigth;
                   RectangleF rect = new RectangleF(x, y, width, heigth);
                    if (board.board[i, j] != 0 && board.board[i, j] <= 9)

                    {

                        int r = 238;
                        int g = 100;

                        if (i == pos_x && j == pos_y)
                        {
                            r = 100;
                            g = 50;
                        }

                        graf.FillRectangle(new SolidBrush(Color.FromArgb(200, r, g, 80)), rect);

                        graf.DrawString(board.board[i, j].ToString(), new Font("Comic Sans MS", (Width / board.width / board.heigth)), new SolidBrush(Color.White), rect, middle);
                    }

                    else if(board.board[i,j]>=10)
                    {
                        int r = 238;
                        int g = 100;

                        if (i == pos_x && j == pos_y)
                        {
                            r = 100;
                            g = 50;
                        }
                        graf.FillRectangle(new SolidBrush(Color.FromArgb(200, r, g, 80)), rect);
                        graf.DrawString(board.board[i, j].ToString(), new Font("Comic Sans MS", ((Width / board.width) / 5)), new SolidBrush(Color.White), rect, middle);
                    
                    }



                }
            Pen pencil = new Pen(Color.White,2);//FromArgb(200, 200, 0,250), 7);
            for (int i = 0; i < board.heigth +width; i++)

                for (int j = 0; j < board.width+heigth; j++)
                {

                    graf.DrawLine(pencil, i * width , 0, i * width , pictureBoxTablero.Height);
                    graf.DrawLine(pencil, 0, j * heigth, pictureBoxTablero.Height, j * heigth);
                }
        }

        
        private void pictureBoxTablero_MouseClick(object sender, MouseEventArgs e)
        {      
            // Esto es para guardar las posiciones en las que se dio click                    
            int x = e.Y * (board.heigth)/pictureBoxTablero.Height;
            int y = e.X*(board.width)/pictureBoxTablero.Width;

            pos_x = x;
            pos_y = y;
            if (board.modo == true)
            {
                board.Agressive_Mode(x, y);
               
            }
            if (board.modo == false)
            {
                board.count = 0;
                board.Click(x, y);
            }
            if (!board.isFirstClick)
            {
                pos_x = -1;
                pos_y = -1;
                pictureBoxTablero.Refresh();
            }
            progressBar1.Value = board.count;

            if (board.gamewon() && board.extended == false)
            {
                if (MessageBox.Show("Has ganado,desea continuar", "You Won", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    board = new TwelveLogic.Twelve(4, 4, false);
                    board.modo = false;
                    progressBar1.Hide();
                }
                else board.extended = true;
                
            }
            if (board.gameover())
            {
                MessageBox.Show("Has Pertido", "Game Over", MessageBoxButtons.OK);
                if (board.score > board.best)
                {
                    board.best = board.score;
                    label8.Text = board.score.ToString();

                }
                board = new TwelveLogic.Twelve(4, 4, false);
                board.best = int.Parse(label8.Text);
                progressBar1.Hide();
                
            }
           
           
                

            
            pictureBoxTablero.Refresh();
               
                
            
            label7.Text = ""+board.score+"";
        }

        private void label2_Click(object sender, EventArgs e)
        {
            int filas = (int)numericUpDown1.Value;
            int columnas = (int)numericUpDown2.Value;
            bool ver = chkmode.Checked;
            chkmode.Checked = false;
            board = new TwelveLogic.Twelve(columnas, filas, ver);
            if (board.modo == true)
                progressBar1.Show();
            else
                progressBar1.Hide();
            progressBar1.Value = 0;
            pictureBoxTablero.Refresh(); 
            
        }

        private void label1_Click(object sender, EventArgs e)
        {
           board.modo = !board.modo;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            board.SaveGame();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
    
}
