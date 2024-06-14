using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers_Game.Models
{
    [Serializable]
    public class Cell : INotifyPropertyChanged
    {
        public Cell(int x, int y)
        {
            this.X = x;
            this.Y = y;
            this.Piesa = null;
            this.Selectat = false;
        }

        public Cell()
        {

        }
        private bool selectat;

        [XmlElement]
        public bool Selectat
        {
            set
            {
                if (value == true)
                {
                    this.Culoare = "#FFE0FFFF"; //culoarea mutarilor
                }
                else
                if ((x + y) % 2 == 0)
                    this.Culoare = "#e4c4ff"; //mov
                else
                    this.Culoare = "#ff89c0"; //rosu

                selectat = value;
                NotifyPropertyChanged("Culoare");
                NotifyPropertyChanged("Selectat");
            }
            get
            {
                return selectat;
            }
        }

        private string culoare;

        [XmlElement]
        public string Culoare
        {
            set
            {
                if (value == "#FFE0FFFF" || value == "#e4c4ff" || value == "#ff89c0")
                {
                    culoare = value;
                    NotifyPropertyChanged("Culoare");
                }
            }
            get
            {
                return culoare;
            }
        }

        private int x;

        [XmlElement]
        public int X
        {
            get { return x; }
            set
            {
                x = value;
                NotifyPropertyChanged("X");
            }
        }

        private int y;
        [XmlElement]
        public int Y
        {
            get { return y; }
            set
            {
                y = value;
                NotifyPropertyChanged("Y");
            }
        }

        private Piesa piesa;

        [XmlElement]
        public Piesa Piesa
        {
            get { return piesa; }
            set
            {
                piesa = value;
                NotifyPropertyChanged("Piesa");
            }
        }

        public void Rege()
        {
            if (Piesa != null)
            {
                if (piesa.Culoare == true && this.X == 0)
                {
                    piesa.DisplayedImage = "/Checkers_Game;component/Resources/purplePieceKing.png";
                    piesa.Rege = true;
                }
                if (piesa.Culoare == false && x == 7)
                {
                    piesa.DisplayedImage = "/Checkers_Game;component/Resources/redPieceKing.png";
                    piesa.Rege = true;
                }
            }
          
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool Mutare(Cell celulaDestinatie)
        {
            if (celulaDestinatie.Piesa == null)
            {
                if (this.Piesa.Rege == true || this.Piesa.Culoare == true)
                {
                    if (celulaDestinatie.X == this.X - 1)
                        if (celulaDestinatie.Y == this.Y - 1 || celulaDestinatie.Y == this.Y + 1)
                            return true;
                }
                if (this.Piesa.Rege == true || this.Piesa.Culoare == false)
                {
                    if (celulaDestinatie.X == this.X + 1)
                        if (celulaDestinatie.Y == this.Y - 1 || celulaDestinatie.Y == this.Y + 1)
                            return true;
                }
            }
            return false;
        }

        public bool Saritura(Cell celulaDestinatie, Cell piesaCapturata)
        {
            if (this.Piesa.Rege == true || this.Piesa.Culoare == true)
            {
                if (celulaDestinatie.X == this.X - 2 && piesaCapturata.X == this.X - 1)
                {
                    if (celulaDestinatie.Y == this.Y + 2 && piesaCapturata.Y == this.Y + 1)
                        return true;
                    if (celulaDestinatie.Y == this.Y - 2 && piesaCapturata.Y == this.Y - 1)
                        return true;
                }
            }
            if (this.Piesa.Rege == true || this.Piesa.Culoare == false)
            {
                if (celulaDestinatie.X == this.X + 2 && piesaCapturata.X == this.X + 1)
                {
                    if (celulaDestinatie.Y == this.Y + 2 && piesaCapturata.Y == this.Y + 1)
                        return true;
                    if (celulaDestinatie.Y == this.Y - 2 && piesaCapturata.Y == this.Y - 1)
                      return true;
                }
            }
            return false;
        }

        public void Schimb(Cell cell)
        {
            cell.Piesa = this.Piesa;
            this.Piesa = null;
            cell.Rege();
        }
    }
}
