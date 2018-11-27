using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using CurrencyCalc2.Resx;
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

        private int idxCurrency;

        private double CrossRate(double currency1, int nominal1, double currency2, int nominal2)
        {


            try
            {

                if ((nominal1 != 0) && (nominal2 != 0))
                {

                    double tmp = (currency1 / nominal1) / (currency2 / nominal2);


                    if (SourceUrl == App.ecb)
                    {
                        tmp = (currency2 / nominal2) / (currency1 / nominal1);
                    } else
                    {
                        tmp = (currency1 / nominal1) / (currency2 / nominal2);
                    }


                    return tmp;
                    
                    
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

            dateTime = DateTime.Now.ToString("G", System.Globalization.CultureInfo.CreateSpecificCulture(System.Threading.Thread.CurrentThread.CurrentUICulture.Name));

            headerLabelCenter.Text = $"{dateTime}";
            headerLabelEnd.Text = AppResources.ExchangeRateFor + $" { entry.Nominal} { entry.CharCode}";

            int cross_nominal_one = short.Parse(entry.Nominal);
            double currency_one = double.Parse(entry.Value);

            int i = -1;
            foreach (Currency x in Valuta)
            {
                i = i + 1;

                if (Valuta[pickersID[1]].CharCode != x.CharCode)
                {
                    y.CharCode = x.CharCode;
                    y.Name = x.Name;
                    y.Nominal = x.Nominal;
                    y.Img = x.Img;

                    // Debug.WriteLine(string.Format("CurrencyCalc2.images.{0}.png", x.CharCode.Remove(x.CharCode.Length - 1).ToLower()));

                    int cross_nominal_two = short.Parse(y.Nominal);
                    double currency_two = double.Parse(x.Value);

                    double t = CrossRate(currency_one, cross_nominal_one, currency_two, cross_nominal_two);
                    string Format = "F4";
                    if (y.CharCode == "IDR") { Format = "F2"; } //индонезийская рупия
                    y.Value = t.ToString(Format, CultureInfo.CurrentCulture);
                    val.Add(y);
                }
                else idxCurrency = i;
            }

            Items = val;        

            MyListView.ItemsSource = Items;
            MyListView.BindingContext = new Binding("MyListView");
        }


       
        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            Currency tappedCurrency = (Currency)((ListView)sender).SelectedItem; //Ура!!! Оказалось так просто... 

            //await DisplayAlert("Валюта", $"{tappedCurrency.CharCode}", "OK");          
            int i = -1;
            foreach (Currency x in Valuta)
            {
                i = i + 1;
                if (x.CharCode == tappedCurrency.CharCode)
                {
                    pickersID[0] = i;  //установим первый пикер
                    break;
                }
            }
            //Deselect Item
            ((ListView)sender).SelectedItem = null;
            var page = (Page)Activator.CreateInstance(typeof(CalculatorPage));
            await Navigation.PushAsync(page);
            //((MasterDetailPage)Application.Current.MainPage).IsPresented = true; //покажет меню
        }
    }
}
