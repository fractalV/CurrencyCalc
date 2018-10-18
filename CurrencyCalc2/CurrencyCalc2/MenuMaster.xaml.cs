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
        }

        class MenuMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<MenuMenuItem> MenuItems { get; set; }
            
            public MenuMasterViewModel()
            {
                MenuItems = new ObservableCollection<MenuMenuItem>(new[]
                {
                    new MenuMenuItem { Id = 0, Title = "Конвертер валют", TargetType = typeof(CalculatorPage), IconSource = "images\am.png" },
                    new MenuMenuItem { Id = 1, Title = "Курсы валют" },
                    new MenuMenuItem { Id = 2, Title = "Параметры", TargetType = typeof(SettingsPage) },
                    new MenuMenuItem { Id = 3, Title = "О программе", TargetType = typeof(AboutPage) },
                    new MenuMenuItem { Id = 4, Title = "Отправить отзыв" }                    
                });
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