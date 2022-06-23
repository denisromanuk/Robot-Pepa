using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace robot_pepa
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        private MediaPlayer levelmusic = new MediaPlayer();

        public MainWindow()
        {
            InitializeComponent();

            levelmusic.Open(new Uri(string.Format("{0}\\levelmusic.mp3", AppDomain.CurrentDomain.BaseDirectory)));
            levelmusic.Play();
            levelmusic.MediaEnded += new EventHandler(levelmusic_ended);
            levelmusic.Volume = 0.1;
        }

        private void levelmusic_ended(object sender, EventArgs e)
        {
            levelmusic.Position = TimeSpan.Zero;
            levelmusic.Play();
        }

        int pozicex;
        int pozicey;
        private void okno_Loaded(object sender, RoutedEventArgs e)
        {
            Random r = new Random();
            int[] player_spawn = new int[] { 0, 9 };

            int x1 = r.Next(0, 2); //spawn hráče
            int y1 = r.Next(0, 2);
            int x2 = r.Next(2, 8); //spawn zekeho
            int y2 = r.Next(1, 9);
            int x3 = r.Next(1, 9); //spawn meče
            int y3 = r.Next(1, 9);

            pozicex = player_spawn[x1];
            pozicey = player_spawn[y1];

            Grid.SetColumn(player, pozicex);
            Grid.SetRow(player, pozicey);
            Grid.SetColumn(zeke, y2);
            Grid.SetRow(zeke, x2);
            Grid.SetColumn(meč, y3);
            Grid.SetRow(meč, x3);
        }

        private void move(object sender, RoutedEventArgs e)
        {
            seznam.Items.Add("move();");
        }

        private void TurnRight(object sender, RoutedEventArgs e)
        {
            seznam.Items.Add("TurnRight();");
        }

        private void TurnLeft(object sender, RoutedEventArgs e)
        {
            seznam.Items.Add("TurnLeft();");
        }

        private void PickUp(object sender, RoutedEventArgs e)
        {
            seznam.Items.Add("PickUp();");
        }

        private void Slash(object sender, RoutedEventArgs e)
        {
            seznam.Items.Add("Slash();");
        }

        int stupen = 0;
        int stav = 1;
        int má_meč = 0;
        private void run(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < seznam.Items.Count; i++)
            {
                switch (seznam.Items[i].ToString())
                {
                    case "move();":
                        {
                            switch (stav)
                            {
                                case 1:
                                    {
                                        pozicex += 1;
                                        Grid.SetColumn(player, pozicex);
                                        break;
                                    }
                                case 2:
                                    {
                                        pozicey -= 1;
                                        Grid.SetRow(player, pozicey);
                                        break;
                                    }
                                case 3:
                                    {
                                        pozicex -= 1;
                                        Grid.SetColumn(player, pozicex);
                                        break;
                                    }
                                case 4:
                                    {
                                        pozicey += 1;
                                        Grid.SetRow(player, pozicey);
                                        break;
                                    }
                            }
                            break;
                        }
                    case "TurnRight();":
                        {
                            stupen += 90;
                            stav -= 1;
                            if (stav < 1)
                            {
                                stav = 4;
                            }

                            RotateTransform rotace = new RotateTransform();
                            rotace.Angle = stupen;
                            rotace.CenterX = 0.5;
                            rotace.CenterY = 0.5;
                            levi.RelativeTransform = rotace;

                            break;
                        }
                    case "TurnLeft();":
                        {
                            stupen -= 90;
                            stav += 1;
                            if (stav > 4)
                            {
                                stav = 1;
                            }

                            RotateTransform rotace = new RotateTransform();
                            rotace.Angle = stupen;
                            rotace.CenterX = 0.5;
                            rotace.CenterY = 0.5;
                            levi.RelativeTransform = rotace;

                            break;
                        }
                    case "PickUp();":
                        {
                            int player_row = Convert.ToInt32(player.GetValue(Grid.RowProperty));
                            int player_column = Convert.ToInt32(player.GetValue(Grid.ColumnProperty));
                            int meč_row = Convert.ToInt32(meč.GetValue(Grid.RowProperty));
                            int meč_column = Convert.ToInt32(meč.GetValue(Grid.ColumnProperty));

                            if (player_row == meč_row && player_column == meč_column)
                            {
                                hracipole.Children.Remove(meč);
                                má_meč = 1;
                            }
                            else
                            {
                                string title = "GAME OVER";
                                MessageBox.Show("Snažil jsi se vzít meč na špatným místě a když jsi nedával pozor tak tě Zeke sežral", title);

                                levelmusic.Close();

                                hlavnístránka menu = new hlavnístránka();
                                menu.Top = this.Top;
                                menu.Left = this.Left;
                                menu.Show();

                                Close();
                            }
                            break;
                        }
                    case "Slash();":
                        {
                            int player_row = Convert.ToInt32(player.GetValue(Grid.RowProperty));
                            int player_column = Convert.ToInt32(player.GetValue(Grid.ColumnProperty));
                            int zeke_row = Convert.ToInt32(zeke.GetValue(Grid.RowProperty));
                            int zeke_column = Convert.ToInt32(zeke.GetValue(Grid.ColumnProperty));

                            if (má_meč == 1)
                            {
                                if (player_row == zeke_row && player_column == zeke_column)
                                {
                                    string title = "VICTORY";
                                    MessageBox.Show("Gratuluji! zabil jsi Zekeho", title);

                                    levelmusic.Close();

                                    hlavnístránka menu = new hlavnístránka();
                                    menu.Top = this.Top;
                                    menu.Left = this.Left;
                                    menu.Show();

                                    Close();
                                }
                                else
                                {
                                    string title = "GAME OVER";
                                    MessageBox.Show("Netrefil si cíl a Zeke tě snědl", title);

                                    levelmusic.Close();

                                    hlavnístránka menu = new hlavnístránka();
                                    menu.Top = this.Top;
                                    menu.Left = this.Left;
                                    menu.Show();

                                    Close();
                                }
                            }
                            else
                            {
                                string title = "NEMÁŠ MEČ";
                                MessageBox.Show("Chybí ti meč, bez něj Zekeho nemůžeš zabít", title);
                            }

                            break;
                        }
                }
            }
            seznam.Items.Clear();
        }
    }
}
