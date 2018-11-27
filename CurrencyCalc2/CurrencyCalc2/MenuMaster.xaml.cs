using CurrencyCalc2.Resx;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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

            // BackgroundColor = Color.FromHex("#56198E");
        
        }        

        public class MenuMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<MenuMenuItem> MenuItems { get; set; }

            public MenuMasterViewModel()
            {
                MenuItems = new ObservableCollection<MenuMenuItem>(new[]
                {
                    new MenuMenuItem { Id = 0, Title = AppResources.MenuTitle0, TargetType = typeof(CalculatorPage) },
                    new MenuMenuItem { Id = 1, Title = AppResources.MenuTitle1, TargetType = typeof(ListViewPageRates) },
                    new MenuMenuItem { Id = 2, Title = AppResources.MenuTitle2, TargetType = typeof(SettingsPage) },
                    new MenuMenuItem { Id = 3, Title = AppResources.MenuTitle3, TargetType = typeof(AboutPage) }
                    //new MenuMenuItem { Id = 4, Title = "Отправить отзыв" }                    
                });                


                MessagingCenter.Subscribe<SettingsPage, string>(this, "UpdateLang", (sender, args) =>
                {
                    System.Diagnostics.Debug.WriteLine($"вызов UpdateLang {AppResources.MenuTitle0}" );
               
                    MenuItems.ElementAt(0).Title = AppResources.MenuTitle0;
                    MenuItems.ElementAt(1).Title = AppResources.MenuTitle1;
                    MenuItems.ElementAt(2).Title = AppResources.MenuTitle2;
                    MenuItems.ElementAt(3).Title = AppResources.MenuTitle3;


                    

                    //MenuMenuItem test = new MenuMenuItem();
                    //MenuItems.Add(test);

                    //foreach (var menuMenuItem in MenuItems)
                    //{
                    //    switch (menuMenuItem.Id)
                    //    {
                    //        case 0:
                    //            menuMenuItem.Title = AppResources.MenuTitle0;
                    //            break;
                    //        case 1:
                    //            menuMenuItem.Title = AppResources.MenuTitle1;
                    //            break;
                    //        case 2:
                    //            menuMenuItem.Title = AppResources.MenuTitle2;
                    //            break;
                    //        case 3:
                    //            menuMenuItem.Title = AppResources.MenuTitle3;
                    //            break;
                    //    };
                    //}



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

                System.Diagnostics.Debug.WriteLine(PropertyChanged.ToString());
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion


            
        }
    }

}