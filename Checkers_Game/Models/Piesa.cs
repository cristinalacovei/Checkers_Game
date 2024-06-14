using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Text;
using System.Xml.Serialization;
using System.Threading.Tasks;
using Checkers_Game.Properties;

namespace Checkers_Game.Models
{
    [Serializable]
    public class Piesa : INotifyPropertyChanged
    {
        public Piesa(bool culoare)
        {
            this.Culoare = culoare;
            this.Rege = false;
            if (culoare == true)
                this.DisplayedImage = "/Checkers_Game;component/Resources/redPiece1.png";
            else
                this.DisplayedImage = "/Checkers_Game;component/Resources/purplePiece1.png";
        }

        public Piesa()
        {

        }
        private bool rege;

        [XmlElement]
        public bool Rege
        {
            get { return rege; }

            set
            {
                rege = value;
                if(value==true)
                    if (culoare == true)
                        this.DisplayedImage = "/Checkers_Game;component/Resources/redPieceKing.png";
                    else
                        this.DisplayedImage = "/Checkers_Game;component/Resources/purplePieceKing.png";
                NotifyPropertyChanged("DisplayedImage");
            }
        }

        private bool culoare;

        [XmlElement]
        public bool Culoare
        {
            get { return culoare; }
            set
            {
                culoare = value;
            }
        }

        private string displayedImage;

        [XmlElement]
        public string DisplayedImage
        {
            get { return displayedImage; }
            set
            {
                displayedImage = value;
                NotifyPropertyChanged("DisplayedImage");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}