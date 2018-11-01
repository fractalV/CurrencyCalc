using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CurrencyCalc2
{

    // пытался избавиться от моргания imageLabelTwo
    public class ImagesLabel : INotifyPropertyChanged
    {
        private object img;

        public object ImageOne
        {
            get { return img; }
            set
            {
                if (img != value)
                {
                    img = value;
                    OnPropertyChanged("ImageOne");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }

}
