﻿using Checkers_Game.Models;
using Checkers_Game.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Checkers_Game.Commands;

namespace Checkers_Game.ViewModels
{
    class CellVM : BaseNotification
    {
        GameBusinessLogic bl;
        public CellVM(Cell cell, GameBusinessLogic bl)
        {
            SimpleCell = cell;
            this.bl = bl;
        }


        private Cell simpleCell;
        public Cell SimpleCell
        {
            get { return simpleCell; }
            set
            {
                simpleCell = value;
                NotifyPropertyChanged("SimpleCell");
            }
        }

        private Piesa piesa;
        public Piesa Piesa
        {
            set
            {
                piesa = value;
                NotifyPropertyChanged("Piesa");
            }
            get
            {
                return piesa;
            }
        }
        private ICommand clickCommand;
        public ICommand ClickCommand
        {
            get
            {
                if (clickCommand == null)
                {
                    clickCommand = new RelayCommand<Cell>(bl.ClickAction);
                }
                return clickCommand;
            }
        }
    }
}
