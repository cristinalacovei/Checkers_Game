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
using Checkers_Game.ViewModels;

namespace Checkers_Game.Views
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        public static GameWindow CurrentInstance { get; private set; }

        public GameWindow()
        {
            InitializeComponent();
            CurrentInstance = this;
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }



        internal void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            GameWindow window = new GameWindow();
            window.Show();
            this.Close();
        }

        private void Saritura_Click(object sender, RoutedEventArgs e)
        {
            Saritura.IsEnabled = false;
        }

        internal void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            this.Close();
            window.Show();
           
        }
    }
}
