using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace robot_pepa
{
    /// <summary>
    /// Interakční logika pro hlavnístránka.xaml
    /// </summary>
    public partial class hlavnístránka : Window
    {
        private MediaPlayer maintheme = new MediaPlayer();

        public hlavnístránka()
        {
            InitializeComponent();

            maintheme.Open(new Uri(string.Format("{0}\\backgroundmusic.mp3", AppDomain.CurrentDomain.BaseDirectory)));
            maintheme.Play();
            maintheme.MediaEnded += new EventHandler(maintheme_ended);
            maintheme.Volume = 0.1;
        }


        private void maintheme_ended(object sender, EventArgs e)
        {
            maintheme.Position = TimeSpan.Zero;
            maintheme.Play();
        }

        private void hrat(object sender, RoutedEventArgs e)
        {
            maintheme.Close();

            MainWindow hra = new MainWindow();
            hra.Top = this.Top;
            hra.Left = this.Left;
            hra.Show();

            Close();
        }

        private void close(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
