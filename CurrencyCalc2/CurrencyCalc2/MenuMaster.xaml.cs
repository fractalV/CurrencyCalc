using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CurrencyCalc2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuMaster : ContentPage
    {
        public ListView ListView;

        public MenuMaster()
        {
            InitializeComponent();            

            BindingContext = new MenuMasterViewModel();
            ListView = MenuItemsListView;

            BackgroundColor = Color.FromHex("#56198E");

        }

        class MenuMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<MenuMenuItem> MenuItems { get; set; }

            public MenuMasterViewModel()
            {
                MenuItems = new ObservableCollection<MenuMenuItem>(new[]
                {
                    new MenuMenuItem { Id = 0, Title = "Конвертер валют", TargetType = typeof(CalculatorPage) },
                    new MenuMenuItem { Id = 1, Title = "Курсы валют" },
                    new MenuMenuItem { Id = 2, Title = "Параметры", TargetType = typeof(SettingsPage) },
                    new MenuMenuItem { Id = 3, Title = "О программе", TargetType = typeof(AboutPage) }
                    //new MenuMenuItem { Id = 4, Title = "Отправить отзыв" }                    
                });



                switch (Device.RuntimePlatform)
                {
                    case Device.UWP:
                        MenuItems[0].IconSource = Char.ConvertFromUtf32(0xE75F);
                        MenuItems[1].IconSource = Char.ConvertFromUtf32(0xE9D2);
                        MenuItems[2].IconSource = Char.ConvertFromUtf32(0xE713);
                        MenuItems[3].IconSource = Char.ConvertFromUtf32(0xE946);
                        break;
                    default:
                        MenuItems[0].IconSource = Char.ConvertFromUtf32(0xf51e);
                        MenuItems[1].IconSource = Char.ConvertFromUtf32(0xf201);
                        MenuItems[2].IconSource = Char.ConvertFromUtf32(0xf013);
                        MenuItems[3].IconSource = Char.ConvertFromUtf32(0xf05a);
                        break;
                }
            }
           

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}