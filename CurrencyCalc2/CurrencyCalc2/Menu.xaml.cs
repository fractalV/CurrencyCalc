using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CurrencyCalc2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Menu : MasterDetailPage
    {        

        public Menu()
        {
            InitializeComponent();

            MasterBehavior = MasterBehavior.Popover;

            

            NavigationPage page = new NavigationPage(MasterPage);

            page.Title = Resx.AppResources.MenuTitle0;



            //Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(MenuMaster))) { BarBackgroundColor = Color.Red };



            MasterPage.ListView.ItemSelected += ListView_ItemSelected;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {


            if (!(e.SelectedItem is MenuMenuItem item))
                return;

            var page = (Page)Activator.CreateInstance(item.TargetType);
            page.Title = item.Title;

            //NavigationPage.SetHasNavigationBar(page, false);  //убпарать титл меню

            Detail = new NavigationPage(page);
            IsPresented = false;

            MasterPage.ListView.SelectedItem = null;
        }
    }
}