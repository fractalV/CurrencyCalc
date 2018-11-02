using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static CurrencyCalc2.App;

namespace CurrencyCalc2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListViewPageRates : ContentPage
    {
        public ObservableCollection<Currency> Items { get; set; }
        public ObservableCollection<Currency> val = new ObservableCollection<Currency>();


        private double CrossRate(double currency1, int nominal1, double currency2, int nominal2)
        {


            try
            {
                if ((nominal1 != 0) && (nominal2 != 0))
                {
                    return (currency1 / nominal1 / (currency2 / nominal2));
                }
            }
            catch (Exception ex)
            {                
                DisplayAlert("Ошибка CrossRate", ex.Message, "Ок");
            }
            return 0;
        }

        public ListViewPageRates()
        {
            InitializeComponent();

            Currency y = new Currency();

            Currency entry = new Currency
            {
                CharCode = Valuta[pickersID[1]].CharCode,
                Nominal = Valuta[pickersID[1]].Nominal,
                Value = Valuta[pickersID[1]].Value
            };

            Debug.WriteLine("{0}, {1}",Valuta[pickersID[0]].CharCode, Valuta[pickersID[1]].CharCode);

            headerLabel.Text = "Курс за " + entry.Nominal + " " + entry.CharCode;

            int cross_nominal_one = Int16.Parse(entry.Nominal);
            double currency_one = double.Parse(entry.Value);

            foreach (Currency x in Valuta)
            {
                
                if (Valuta[pickersID[1]].CharCode !=  x.CharCode )
                {
                    y.CharCode = x.CharCode;
                    y.Name = x.Name;
                    y.Nominal = x.Nominal;
                    y.Img = x.Img;
                   
                   // Debug.WriteLine(string.Format("CurrencyCalc2.images.{0}.png", x.CharCode.Remove(x.CharCode.Length - 1).ToLower()));
                    
                    int cross_nominal_two = Int16.Parse(y.Nominal);                    
                    double currency_two = double.Parse(x.Value);

                    double t = CrossRate(currency_one, cross_nominal_one, currency_two, cross_nominal_two);
                    const string Format = "F3";
                    y.Value = t.ToString(Format, CultureInfo.CurrentCulture);
                    val.Add(y);
                }                
            }

            Items = val;        

            MyListView.ItemsSource = Items;
            MyListView.BindingContext = new Binding("MyListView");
            
            //for (int i = 0; i <= Items.Count; i++)
            //{ 
            //    Items.ElementAt(i).Value = CalculateItog(Items.ElementAt(i).Value);
            //}
        }


       
        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;
            
            await DisplayAlert("Валюта", "Здесь будет график изменения курса", "OK");
            

            //Deselect Item
            ((ListView)sender).SelectedItem = null;


        }
    }
}
