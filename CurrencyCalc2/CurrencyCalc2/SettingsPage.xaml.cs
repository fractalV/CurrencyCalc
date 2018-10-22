using System;
using System.Collections.Generic;
using System.Diagnostics;

using Xamarin.Forms;
using Xamarin.Forms.Themes;
using Xamarin.Forms.Xaml;

namespace CurrencyCalc2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SettingsPage : ContentPage
	{



        public SettingsPage()
        {


            InitializeComponent();

            var themeList = new List<string>();
            //themeList.Add("Системная");
            themeList.Add("Светлая");
            themeList.Add("Темная");


            ThemePicker.ItemsSource = themeList;

            if (Resources?.GetType() == typeof(DarkThemeResources)) { ThemePicker.SelectedIndex = 1; } else { ThemePicker.SelectedIndex = 0; };
            //ThemePicker.SetBinding(Picker.ItemsSourceProperty, "Theme");
            //ThemePicker.SetBinding(Picker.SelectedItemProperty, "Системная");
            //ThemePicker.ItemDisplayBinding = new Binding("Name");

        }

        void OnPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

           
            //App.Current.Resources = new Xamarin.Forms.Themes.LightThemeResources();
            var origin = App.Current.Resources;


            if (selectedIndex != -1)
            {
                Debug.WriteLine((string)picker.ItemsSource[selectedIndex]);
                if (selectedIndex == 0)
                {
                    //origin.Source = typeof(Xamarin.Forms.Themes.LightThemeResources);
                    //origin.MergedWith = typeof(LightThemeResources);
                    SwitchTheme();
                };
                if (selectedIndex == 1)
                {
                    //origin.MergedWith = typeof(Xamarin.Forms.Themes.DarkThemeResources);
                    //origin.MergedWith = typeof(DarkThemeResources);
                    SwitchTheme();
                }
            }
        }

        public void SwitchTheme()
        {
            if (Resources?.GetType() == typeof(DarkThemeResources))
            {
                Resources = new LightThemeResources();
                return;
            }
            Resources = new DarkThemeResources();
        }
    }
}