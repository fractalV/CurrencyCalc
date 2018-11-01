using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CurrencyCalc2
{
    public struct Currency
    {
        public string CharCode { get; set; }           
        public string Nominal { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string ForPicker { get; set; }
        public string Symbol { get; set; }
        public ImageSource Img { get; }
        

        public Currency(string chcode, string nominal, string name, string value, string symbol)
        {
            CharCode = chcode;
            Nominal = nominal;
            Name = name;
            Value = value;
            ForPicker = name;
            Symbol = symbol;
            Img = ImageSource.FromResource(string.Format("CurrencyCalc2.images.{0}.png", chcode.Remove(chcode.Length - 1).ToLower()));
        }

        
        public  ImageSource GetImg(string chcode)
        {
            string Curr_image(string CurrencyProperty)
            {
                return string.Format("CurrencyCalc2.images.{0}.png", CurrencyProperty.ToLower());
            }

            return ImageSource.FromResource(Curr_image(chcode.Remove(chcode.Length - 1)));
        }

    }
}
