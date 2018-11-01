using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
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

        public ListViewPageRates()
        {
            InitializeComponent();

            Items = _valutes;
           
            //Items = new ObservableCollection<string>
            //{
            //    "Item 1",
            //    "Item 2",
            //    "Item 3",
            //    "Item 4",
            //    "Item 5"
            //};



            MyListView.ItemsSource = Items;
            MyListView.BindingContext = new Binding("MyListView");
            
            //for (int i = 0; i <= Items.Count; i++)
            //{ 
            //    Items.ElementAt(i).Value = CalculateItog(Items.ElementAt(i).Value);
            //}

            

            headerLabel.Text = "Курс за " + Items[pickersID[0]].Nominal + " " + Items[pickersID[0]].CharCode;


        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            await DisplayAlert("Item Tapped", "An item was tapped.", "OK");

            Debug.WriteLine(sender.ToString());

            //Deselect Item
            ((ListView)sender).SelectedItem = null;


        }
    }
}
