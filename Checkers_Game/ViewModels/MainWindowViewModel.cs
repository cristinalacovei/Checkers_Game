using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Checkers_Game.Views;

namespace Checkers_Game.ViewModels
{
    public class MainWindowViewModel
    {
        public ICommand GameCommand { get; }
        public ICommand AboutCommand { get; }
        public ICommand ExitCommand { get; }

        public MainWindowViewModel()
        {
            GameCommand = new RelayCommand(ExecuteGameCommand);
            AboutCommand = new RelayCommand(ExecuteAboutCommand);
            ExitCommand = new RelayCommand(ExecuteExitCommand);
        }

        private void ExecuteGameCommand(object parameter)
        {
            // Deschideți fereastra GameWindow
            GameWindow gameWindow = new GameWindow();
            gameWindow.Show();

            // Închideți fereastra MainWindow
            CloseMainWindow();
        }

        private void ExecuteAboutCommand(object parameter)
        {
            // Deschideți fereastra AboutGameWindow
            AboutGameWindow aboutWindow = new AboutGameWindow();
            aboutWindow.Show();

            // Închideți fereastra MainWindow
            CloseMainWindow();
        }
        private void ExecuteExitCommand(object parameter)
        {
            CloseMainWindow();
        }

        private void CloseMainWindow()
        {
            // Obțineți referința la MainWindow și închideți-o
            foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
            {
                if (window.GetType() == typeof(MainWindow))
                {
                    window.Close();
                    break;
                }
            }
        }
    }
}
