
using Checkers_Game.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Checkers_Game.Services;
using Checkers_Game.Commands;
using Microsoft.Win32;
using System.IO;

namespace Checkers_Game.ViewModels
{

    class GameVM : BaseNotification
    {

        private ObservableCollection<ObservableCollection<CellVM>> gameBoard;

        public ObservableCollection<ObservableCollection<CellVM>> GameBoard
        {
            set
            {
                gameBoard = value;
                NotifyPropertyChanged("GameBoard");
            }
            get
            {
                return gameBoard;
            }
        }
        public GameVM()
        {

            ObservableCollection<ObservableCollection<Cell>> board = Helper.InitGameBoard();

            bl = new GameBusinessLogic(board);
            GameBoard = CellBoardToCellVMBoard(ref board);
        }

        private GameBusinessLogic bl;

        public GameBusinessLogic BL
        {
            get
            {
                return bl;
            }
            set
            {
                bl = value;
                NotifyPropertyChanged("BL");
            }
        }

        private ObservableCollection<ObservableCollection<CellVM>> CellBoardToCellVMBoard(ref ObservableCollection<ObservableCollection<Cell>> board)
        {
            ObservableCollection<ObservableCollection<CellVM>> result = new ObservableCollection<ObservableCollection<CellVM>>();
            for (int i = 0; i < board.Count; i++)
            {
                ObservableCollection<CellVM> line = new ObservableCollection<CellVM>();
                for (int j = 0; j < board[i].Count; j++)
                {
                    Cell c = board[i][j];
                    CellVM cellVM = new CellVM(c, bl);
                    line.Add(cellVM);
                }
                result.Add(line);
            }
            return result;
        }

        public void SaveToXML()
        {
            var board = CellVMBoardToCellBoard();

           
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;

               
                Helper.SerializeObjectToXML<ObservableCollection<ObservableCollection<Cell>>>(board, filePath);
                Helper.SerializeObjectToXML<Jucator>(bl.JucatorAlb, Path.ChangeExtension(filePath, ".player1.xml"));
                Helper.SerializeObjectToXML<Jucator>(bl.JucatorRosu, Path.ChangeExtension(filePath, ".player2.xml"));
            }
        }

        public void ReadFromXML()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;

                var b = Helper.DeserializeObjectToXML<ObservableCollection<ObservableCollection<Cell>>>(filePath);
                var a = Helper.DeserializeObjectToXML<Jucator>(Path.ChangeExtension(filePath, ".player1.xml"));
                var n = Helper.DeserializeObjectToXML<Jucator>(Path.ChangeExtension(filePath, ".player2.xml"));
                this.BL = new GameBusinessLogic(ref b, ref a, ref n);

                GameBoard = CellBoardToCellVMBoard(ref b);
            }
        }

        public ICommand Saritura
        {
            set
            {
                bl.Saritura = true;
            }
        }



        private ICommand saveGame;
        public ICommand SaveGame
        {
            get
            {
                if (saveGame == null)
                {
                    saveGame = new RelayCommand<Object>(o => SaveToXML());
                }
                return saveGame;
            }
        }

        private ICommand opneGame;
        public ICommand OpenGame
        {
            get
            {
                if (opneGame == null)
                {
                    opneGame = new RelayCommand<Object>(o => ReadFromXML());
                }
                return opneGame;
            }
        }

        public bool SarituraM
        {
            get
            {
                if (bl.Saritura)
                    return false;
                else
                    return true;
            }
        }
        private ObservableCollection<ObservableCollection<Cell>> CellVMBoardToCellBoard()
        {
            ObservableCollection<ObservableCollection<Cell>> result = new ObservableCollection<ObservableCollection<Cell>>();
            for (int i = 0; i < gameBoard.Count; i++)
            {
                ObservableCollection<Cell> line = new ObservableCollection<Cell>();
                for (int j = 0; j < gameBoard[i].Count; j++)
                {
                    CellVM c = gameBoard[i][j];
                    line.Add(c.SimpleCell);
                }
                result.Add(line);
            }
            return result;
        }


    }
}