using Checkers_Game.Models;
using Checkers_Game.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Serialization;
using System.Text;
using System.IO;
using System.Threading;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using Checkers_Game.Views;

namespace Checkers_Game.Services
{
    [Serializable]
     public class GameBusinessLogic: INotifyPropertyChanged
    {
        private ObservableCollection<ObservableCollection<Cell>> cells;

        [XmlArrayItem]
        public ObservableCollection<ObservableCollection<Cell>> Cells
        {
            get
            {
                return cells;
            }
            set
            {
                cells = value;
                NotifyPropertyChanged("Cells");
            }
        }
       
        private Jucator jucatorAlb;

        [XmlElement]
        public Jucator JucatorAlb
        {
            get
            {
                return jucatorAlb;
            }
            set
            {
                
                jucatorAlb = value;
                NotifyPropertyChanged("JucatorAlb");
            }
        }

       
        private Jucator jucatorRosu;

        [XmlElement]
        public Jucator JucatorRosu
        {
            get
            {
                return jucatorRosu;
            }
            set
            {
                jucatorRosu = value;
                NotifyPropertyChanged("JucatorRosu");
            }
        }

       
        private bool piesaSelectata;

       
        private Scor scor;

        public Scor Scor
        {
            get
            {
                return scor;
            }
        }

        private bool saritura;

        [XmlElement]
        public bool Saritura
        {
            set
            {
                saritura = value;
            }
            get
            {
                return saritura;
            }
        }

        private bool sarituraInCurs;

        public GameBusinessLogic(ref ObservableCollection<ObservableCollection<Cell>> cells, ref Jucator jucatorAlb,ref Jucator jucatorRosu)
        {
            this.Cells = cells;
            this.jucatorAlb = jucatorAlb;
            this.jucatorRosu = jucatorRosu;
            this.scor = Helper.GetScor();
        }


        public GameBusinessLogic()
        {
      
        }
        public GameBusinessLogic(ObservableCollection<ObservableCollection<Cell>> cells)
        {
            jucatorAlb = new Jucator("Player2", false, false);
            jucatorRosu = new Jucator("Player1", true, true);
            this.cells = cells;
            piesaSelectata = false;
            JucatorCurent();
            this.Saritura = false;
            sarituraInCurs = false;
            this.scor = Helper.GetScor();
        }



        private void PozitiiMutare(Jucator jucator)
        {
            jucator.Destinatie = MutareDiagonalaSusDreapta(jucator,1);
            ColoreazaOptiuni(jucator, jucator.Destinatie);

            jucator.Destinatie = MutareDiagonalaSusStanga(jucator,1);
            ColoreazaOptiuni(jucator, jucator.Destinatie);
            
            jucator.Destinatie = MutareDiagonalaJosDreapta(jucator,1);
            ColoreazaOptiuni(jucator, jucator.Destinatie);

            jucator.Destinatie = MutareDiagonalaJosStanga(jucator,1);
            ColoreazaOptiuni(jucator, jucator.Destinatie);
        }

        private bool PozitiiSaritura(Jucator jucator)
        {
            jucator.Destinatie = MutareDiagonalaSusDreapta(jucator, 2);
            jucator.PiesaCapturata = MutareDiagonalaSusDreapta(jucator, 1);
            ColoreazaOptiuniSaritura(jucator, jucator.Destinatie,jucator.PiesaCapturata);
            
            jucator.Destinatie = MutareDiagonalaSusStanga(jucator, 2);
            jucator.PiesaCapturata = MutareDiagonalaSusStanga(jucator, 1);
            ColoreazaOptiuniSaritura(jucator, jucator.Destinatie, jucator.PiesaCapturata);
            
            jucator.Destinatie = MutareDiagonalaJosDreapta(jucator, 2);
            jucator.PiesaCapturata = MutareDiagonalaJosDreapta(jucator, 1);
            ColoreazaOptiuniSaritura(jucator, jucator.Destinatie, jucator.PiesaCapturata);
            
            jucator.Destinatie = MutareDiagonalaJosStanga(jucator, 2);
            jucator.PiesaCapturata = MutareDiagonalaJosStanga(jucator, 1);
            ColoreazaOptiuniSaritura(jucator, jucator.Destinatie, jucator.PiesaCapturata);
            if (jucator.Mutari.Count != 0)
                return true;
            else
                return false;
        }
        private void ColoreazaOptiuni(Jucator jucator, Cell cell)
        {
            if (cell!=null && jucator.PiesaMutata.Mutare(cell))
            {
                jucator.Mutari.Add(cell);
                jucator.Destinatie = cell;
                jucator.Destinatie.Selectat = true;
            }
        }
        private void ColoreazaOptiuniSaritura(Jucator jucator, Cell cell, Cell capturat)
        {
            if (cell != null && capturat != null && cell.Piesa == null
                       && capturat.Piesa != null && capturat.Piesa.Culoare != jucator.PiesaMutata.Piesa.Culoare)
            {
                if (jucator.PiesaMutata.Saritura(cell, capturat))
                {
                    jucator.Mutari.Add(cell);
                    jucator.Destinatie = cell;
                    jucator.Destinatie.Selectat = true;
                }
            }
        }
        private Cell MutareDiagonalaSusDreapta(Jucator jucator, int mutare_saritura)
        {
            if (jucator.PiesaMutata.X - mutare_saritura >= 0 && jucator.PiesaMutata.Y + mutare_saritura < 8)
                return cells[jucator.PiesaMutata.X - mutare_saritura][jucator.PiesaMutata.Y + mutare_saritura];
            return null;
        }
        private Cell MutareDiagonalaSusStanga(Jucator jucator,int mutare_saritura)
        {
            if (jucator.PiesaMutata.X - mutare_saritura >= 0 && jucator.PiesaMutata.Y - mutare_saritura >= 0)
                return cells[jucator.PiesaMutata.X - mutare_saritura][jucator.PiesaMutata.Y - mutare_saritura];
            return null;
        }
        private Cell MutareDiagonalaJosDreapta(Jucator jucator, int mutare_saritura)
        {
            if (jucator.PiesaMutata.X + mutare_saritura < 8 && jucator.PiesaMutata.Y + mutare_saritura < 8)
                return cells[jucator.PiesaMutata.X + mutare_saritura][jucator.PiesaMutata.Y + mutare_saritura];
            return null;
        }
        private Cell MutareDiagonalaJosStanga(Jucator jucator, int mutare_saritura)
        {
            if (jucator.PiesaMutata.X + mutare_saritura < 8 && jucator.PiesaMutata.Y - mutare_saritura >= 0)
                return cells[jucator.PiesaMutata.X + mutare_saritura][jucator.PiesaMutata.Y - mutare_saritura];
            return null;
        }
        private bool Mutare(Jucator jucator, Cell currentCell)
        {
            if (jucator.MutarePosibila(currentCell))
            {
                jucator.PiesaCapturata = PiesaCapturata(jucator, currentCell);
                cells[jucator.PiesaMutata.X][jucator.PiesaMutata.Y].Schimb(cells[currentCell.X][currentCell.Y]);
                if (jucator.PiesaCapturata != currentCell)
                {
                    cells[jucator.PiesaCapturata.X][jucator.PiesaCapturata.Y].Piesa.DisplayedImage = null;
                    cells[jucator.PiesaCapturata.X][jucator.PiesaCapturata.Y].Piesa = null;
                    jucator.PiesaMutata.Selectat = false;
                    jucator.PiesaMutata = cells[currentCell.X][currentCell.Y];
                    Castig(jucator);
                }
                else
                {
                    jucator.PiesaMutata.Selectat = false;
                    jucator.PiesaMutata = null;
                    piesaSelectata = false;
                }
                return true;
            }
            return false;
        }
        private Cell PiesaCapturata(Jucator jucator, Cell currentCell)
        {
            jucator.PiesaCapturata = null;
            if (jucator.PiesaMutata.X > currentCell.X)
            {
                if (jucator.PiesaMutata.Y > currentCell.Y)
                    jucator.PiesaCapturata = cells[jucator.PiesaMutata.X - 1][jucator.PiesaMutata.Y - 1];
                else
                    jucator.PiesaCapturata = cells[jucator.PiesaMutata.X - 1][jucator.PiesaMutata.Y + 1];
            }
            else
            {
                if (jucator.PiesaMutata.Y > currentCell.Y)
                    jucator.PiesaCapturata = cells[jucator.PiesaMutata.X + 1][jucator.PiesaMutata.Y - 1];
                else
                    jucator.PiesaCapturata = cells[jucator.PiesaMutata.X + 1][jucator.PiesaMutata.Y + 1];
            }
            return jucator.PiesaCapturata;
        }
        public void Move(Jucator jucator,Cell currentCell)
        {
            if (cells[currentCell.X][currentCell.Y].Piesa != null && piesaSelectata == false && cells[currentCell.X][currentCell.Y].Piesa.Culoare==jucator.Culoare)
            {
                jucator.PiesaMutata = currentCell;
                jucator.PiesaMutata.Selectat = true;
                piesaSelectata = true;
                if (!PozitiiSaritura(jucator))
                    PozitiiMutare(jucator);   
            }
            else
            if (piesaSelectata == true && currentCell!=jucator.PiesaMutata)
            {
                if (sarituraInCurs==false && cells[currentCell.X][currentCell.Y].Piesa != null && cells[currentCell.X][currentCell.Y].Piesa.Culoare == jucator.Culoare)
                {
                    jucator.PiesaMutata.Selectat = false;
                    StergereMutariPosibile(jucator);
                    jucator.PiesaMutata = currentCell;
                    jucator.PiesaMutata.Selectat = true;
                    if (!PozitiiSaritura(jucator))
                        PozitiiMutare(jucator);
                }
                else
                 if (Mutare(jucator, currentCell))
                {
                    StergereMutariPosibile(jucator);
                    VerificareRand(jucator);
                }
                
            }
            
            JucatorAlb.NrPiese = jucatorAlb.NrPiese;
            JucatorRosu.NrPiese = jucatorRosu.NrPiese;

            NotifyPropertyChanged("JucatorAlb");
            NotifyPropertyChanged("JucatorRosu");

        }
        private void StergereMutariPosibile(Jucator jucator)
        {
            for (int i = 0; i < jucator.Mutari.Count; ++i)
            {
                jucator.Mutari[i].Selectat = false;
            }
            jucator.Mutari.Clear();
        }
        private void VerificareRand(Jucator jucator)
        {
            jucator.PiesaCapturata = null;
            jucator.Destinatie = null;

            if (jucator.PiesaMutata != null && saritura == true)
            {
                jucator.PiesaMutata.Selectat = true;
                sarituraInCurs = true;
                if (!PozitiiSaritura(jucator))
                {
                    jucator.PiesaMutata.Selectat = false;
                    jucator.PiesaMutata = null;
                    piesaSelectata = false;
                    SchimbTura();
                    sarituraInCurs = false;
                }
            }
            else
            {
                SchimbTura();
                sarituraInCurs = false;
                jucator.PiesaMutata = null;
                piesaSelectata = false;
            }
        }

        public void ClickAction(Cell obj)
        {
            if (jucatorAlb.Tura == true)
            {
                Helper.JucatorCurent = jucatorAlb.Nume;
                Move(jucatorAlb, obj);
            }
            else
            {
                Helper.JucatorCurent = jucatorRosu.Nume;
                Move(jucatorRosu, obj);
            }
        }

        private void SchimbTura()
        {
            jucatorAlb.SchimbTura();
            jucatorRosu.SchimbTura();

            JucatorCurent();
        }

        private void JucatorCurent()
        {
            if (jucatorAlb.Tura)
                Helper.JucatorCurent = "Player2";
            else
                Helper.JucatorCurent = "Player1";
            NotifyPropertyChanged("JucatorLaMutare");
        }

        [XmlIgnore]
        public string JucatorLaMutare
        {
            get
            {
                return Helper.JucatorCurent + ", it's your turn to move!";
            }
        }


        public void Castig(Jucator jucator)
        {
            if (jucator.Culoare)
                jucatorAlb.NrPiese--;
            else
                jucatorRosu.NrPiese--;

            if (jucatorRosu.NrPiese == 0)
            {
                scor.ScorAlb++;
                AfisareMesajCastigator("Player2");
            }
            if (jucatorAlb.NrPiese == 0)
            {
                scor.ScorRosu++;
                AfisareMesajCastigator("Player1"); 
            }
            NotifyPropertyChanged("JucatorLaMutare");
        }

        private void AfisareMesajCastigator(string castigator)
        {
            // Creează un mesaj personalizat cu numele jucătorului câștigător și o întrebare dacă se dorește să se joace din nou
            string message = $"{castigator} has won the game! Do you want to play again?";
            string caption = "Game Over";
            MessageBoxButton buttons = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Question;

            // Afișează mesajul și primește răspunsul utilizatorului
            MessageBoxResult result = MessageBox.Show(message, caption, buttons, icon);

            // Acționează în funcție de răspunsul utilizatorului
            if (result == MessageBoxResult.Yes)
            {
                if (GameWindow.CurrentInstance != null)
                {
                    GameWindow.CurrentInstance.MenuItem_Click(null, null);
                }
            }
            else
            {
                if (GameWindow.CurrentInstance != null)
                {
                    GameWindow.CurrentInstance.Button_Click(null, null);
                }
            }
        }


        public void Actulizare()
        {
            JucatorCurent();
            NotifyPropertyChanged("cells");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
     }
}