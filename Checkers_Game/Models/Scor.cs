using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.IO;
using Checkers_Game.Services;

namespace Checkers_Game.Models
{
    public class Scor : INotifyPropertyChanged
    {
        
        public Scor()
        {

        }

        private int scorJucatorAlb;
        public int ScorAlb
        {
            get
            {
                return scorJucatorAlb;
            }
            set
            {
                scorJucatorAlb = value;
                StreamWriter sw = new StreamWriter("../../Resources/scor.txt");
                string text;
                text = scorJucatorAlb.ToString() + "\n" + scorJucatorRosu.ToString();
                sw.WriteLine(text);
                sw.Close();

                NotifyPropertyChanged("ScorAlb");
            }
        }

        private int scorJucatorRosu;

        public int ScorRosu
        {
            get
            {
                return scorJucatorRosu;
            }
            set
            {
                scorJucatorRosu = value;
                StreamWriter sw = new StreamWriter("../../Resources/scor.txt");
                string text;
                text = scorJucatorAlb.ToString() + "\n" + scorJucatorRosu.ToString();
                sw.WriteLine(text);
                sw.Close();

                NotifyPropertyChanged("ScorRosu");
            }
        }

        public string SA
        {
            get
            {
               
                return scorJucatorAlb.ToString();
            }
        }

        public string SR
        {
            get
            {
                
                return scorJucatorRosu.ToString();
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
