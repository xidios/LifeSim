using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LifeS
{
    public partial class Form1 : Form
    {

        private Graphics graphics;
        private int resolution;
        private Map gameEngine;
        private Human observedHuman = null;

        public Form1()
        {
            InitializeComponent();

        }
        private void StartGame()
        {
            
            buttonStart.Text = "RESTART";
            buttonPause.Text = "Pause";
            humanSatiety.Text = "Satiety of human";

            resolution = (int)Resolution.Value;//присваиваем значение в инт

            gameEngine = new Map(
                rows: pictureBox1.Height / resolution,
                cols: pictureBox1.Width / resolution,
                density: (int)Density.Value //(int)Density.Minimum + (int)Density.Maximum - (int)Density.Value
                );


            Text = $"Generation {gameEngine.CurrentGeneration}";

            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);//создаем битмап. Новую картинку
            graphics = Graphics.FromImage(pictureBox1.Image);//передали картинку из прошлой строчки
            timer1.Start();
        }
        private void DrawGeneration()
        {
            graphics.Clear(Color.Black);//очищаем игровое поле
            var field = gameEngine.NextGeneration();

            for (int x = 0; x < field.GetLength(0); x++)
            {
                for (int y = 0; y < field.GetLength(1); y++)
                {
                    if (field[x, y].plants.Count() == 1 && field[x, y].plants[0].alive)
                    {
                        graphics.FillRectangle(Brushes.Green, x * resolution, y * resolution, resolution, resolution);

                    }
                    if (field[x, y].humans.Count() > 0 && field[x, y].humans[0].gender == Gender.female)
                    {
                        graphics.FillRectangle(Brushes.IndianRed, x * resolution, y * resolution, resolution, resolution);
                    }
                    if (field[x, y].humans.Count() > 0 && field[x, y].humans[0].gender==Gender.male)
                    {
                        graphics.FillRectangle(Brushes.Moccasin, x * resolution, y * resolution, resolution, resolution);
                        
                    }
                    if(observedHuman != null)
                    {
                        graphics.FillRectangle(Brushes.Blue, observedHuman.x * resolution, observedHuman.y * resolution, resolution, resolution);
                    }
                    
                }
            }
            if (observedHuman != null)
            {
                humanSatiety.Text = $"Satiety: {observedHuman.satiety}";
                status.Text = $"Status: {((observedHuman.satiety == 0) ? "Dead" : "Alive")}";
                labelTimeChild.Text = $"Time from last child: {observedHuman.timeLastChild}";
            }
            else
            {
                humanSatiety.Text = "There is no human here";
            }

            Text = $"Generation {gameEngine.CurrentGeneration}";
            TotalHuman.Text = $"Total of humans: {gameEngine.TotalOfHumans}";
           

            pictureBox1.Refresh();
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            DrawGeneration();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            StartGame();
        }

       

        private void buttonPause_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {                
                timer1.Stop();
                buttonPause.Text = "Continue";
            }
            else if(!timer1.Enabled)
            {               
                timer1.Start();
                buttonPause.Text = "Pause";
            }           
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int x = e.Location.X /resolution;
                int y = e.Location.Y /resolution;
                observedHuman = gameEngine.GetHuman(x, y);
                if (observedHuman != null) {
                    humanSatiety.Text = $"Satiety: {observedHuman.satiety}";
                    status.Text = $"Status: {((observedHuman.satiety == 0)?"Dead":"Alive")}";
                    labelGender.Text = $"Status: {((observedHuman.gender == Gender.male) ? "Male" : "Female")}";
                    labelTimeChild.Text = $"Time from last child: {observedHuman.timeLastChild}";

                    
                    graphics.FillRectangle(Brushes.Blue, observedHuman.x * resolution, observedHuman.y * resolution, resolution, resolution);
                }
                else
                {
                    humanSatiety.Text = "There is no human here";
                    status.Text = null;
                }
            }
        }
    }

}



