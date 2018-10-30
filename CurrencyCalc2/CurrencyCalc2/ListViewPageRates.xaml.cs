using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

            Items = Valuta;
           
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

          


        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            await DisplayAlert("Item Tapped", "An item was tapped.", "OK");

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }
    }
}
