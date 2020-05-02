using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnakeGame
{
    public partial class Interface : Form
    {
        private List<Circle> Snake = new List<Circle>();
        private Circle Food = new Circle();
        public Interface()
        {
            InitializeComponent();

            new Settings();

            GameTimer.Interval = 1000 / Settings.Speed;
            GameTimer.Tick += UpdateScreen;
            GameTimer.Start();

            StartGame();
        }

        private void UpdateScreen(object sender, EventArgs e)
        {
            if (Settings.GameOver == true)
            {
                if (Input.KeyPress(Keys.Enter))
                {
                    StartGame();
                }
            }

            else
            {
                if (Input.KeyPress(Keys.Right) && Settings.Direction != Direction.Left)
                {
                    Settings.Direction = Direction.Right;
                }

                else if (Input.KeyPress(Keys.Left) && Settings.Direction != Direction.Right)
                {
                    Settings.Direction = Direction.Left;
                }

                else if (Input.KeyPress(Keys.Up) && Settings.Direction != Direction.Down)
                {
                    Settings.Direction = Direction.Up;
                }

                else if (Input.KeyPress(Keys.Down) && Settings.Direction != Direction.Up)
                {
                    Settings.Direction = Direction.Down;
                }

                MovePlayer();
            }

            Canvas.Invalidate();
        }

        private void MovePlayer()
        {
            for (int i = Snake.Count - 1; i >= 0; i--)
            {
                if (i == 0)
                {
                    switch (Settings.Direction)
                    {
                        case Direction.Right:
                            Snake[i].X++;
                            break;

                        case Direction.Left:
                            Snake[i].X--;
                            break;

                        case Direction.Up:
                            Snake[i].Y--;
                            break;

                        case Direction.Down:
                            Snake[i].Y++;
                            break;
                    }

                    int MaxPositionX = Canvas.Size.Width / Settings.Width;
                    int MaxPositionY = Canvas.Size.Height / Settings.Height;

                    if (Snake[i].X < 0 || Snake[i].Y < 0 || Snake[i].X > MaxPositionX || Snake[i].Y > MaxPositionY)
                    {
                        EndGame();
                    }

                    for (int x = 1; x < Snake.Count; x++)
                    {
                        if (Snake[i].X == Snake[x].X && Snake[i].Y == Snake[x].Y)
                        {
                            EndGame();
                        }
                    }

                    if (Snake[0].X == Food.X && Snake[0].Y == Food.Y)
                    {
                        EatFood();
                    }
                }

                else
                {
                    Snake[i].X = Snake[i - 1].X;
                    Snake[i].Y = Snake[i - 1].Y;
                }
            }
        }

        private void KeyisUp(object sender, KeyEventArgs e)
        {
            Input.ChangeState(e.KeyCode, false);
        }

        private void KeyisDown(object sender, KeyEventArgs e)
        {
            Input.ChangeState(e.KeyCode, true);
        }

        private void UpdateGraphics(object sender, PaintEventArgs e)
        {
            Graphics Canvas = e.Graphics;

            if (Settings.GameOver == false)
            {
                Brush SnakeColor;

                for (int i = 0; i < Snake.Count; i++)
                {
                    if (i == 0)
                    {
                        SnakeColor = Brushes.Red;
                    }

                    else
                    {
                        SnakeColor = Brushes.Black;
                    }

                    Canvas.FillEllipse(SnakeColor, new Rectangle(Snake[i].X * Settings.Width, Snake[i].Y * Settings.Height, Settings.Width, Settings.Height));

                    Canvas.FillEllipse(Brushes.Green, new Rectangle(Food.X * Settings.Width, Food.Y * Settings.Height, Settings.Width, Settings.Height));
                }
            }

            else
            {
                string GameOver = "Game Over!\n" + "Final Score: " + Settings.Score + "\nPress 'Enter' to Restart.";
                label3.Text = GameOver;
                label3.Visible = true;
            }
        }

        private void StartGame()
        {
            label3.Visible = false;
            new Settings();
            Snake.Clear();

            Circle Head = new Circle
            {
                X = 10,
                Y = 5
            };

            Snake.Add(Head);
            label2.Text = Settings.Score.ToString();
            GenerateFood();
        }

        private void GenerateFood()
        {
            int MaxPositionX = Canvas.Size.Width / Settings.Width;
            int MaxPositionY = Canvas.Size.Height / Settings.Height;
            Random random = new Random();

            Food = new Circle
            {
                X = random.Next(0, MaxPositionX),
                Y = random.Next(0, MaxPositionY)
            };
        }

        private void EatFood()
        {
            Circle Food = new Circle
            {
                X = Snake[Snake.Count - 1].X,
                Y = Snake[Snake.Count - 1].Y
            };

            Snake.Add(Food);
            Settings.Score += Settings.Points;
            label2.Text = Settings.Score.ToString();
            GenerateFood();
        }

        private void EndGame()
        {
            Settings.GameOver = true;
        }

        private void Interface_Load(object sender, EventArgs e)
        {

        }
    }
}
