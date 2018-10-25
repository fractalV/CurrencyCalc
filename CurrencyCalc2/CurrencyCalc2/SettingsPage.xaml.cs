using System;
using System.Collections.Generic;
using System.Diagnostics;

using Xamarin.Forms;
using Xamarin.Forms.Themes;
using Xamarin.Forms.Xaml;
using CurrencyCalc2;

namespace CurrencyCalc2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SettingsPage : ContentPage
	{

        public enum XApplicationThemes
        {
            Original,
            Light,
            Dark
        }        

        public SettingsPage()
        {


            InitializeComponent();

            var themeList = new List<string>();
            themeList.Add("Оригинальная");
            themeList.Add("Светлая");
            themeList.Add("Темная");


            ThemePicker.ItemsSource = themeList;

            var origin = App.Current.Resources;
            Debug.WriteLine(origin.Source);

            if (App.ThemeID == 2)
            {
                ThemePicker.SelectedIndex = 2;                
                Debug.WriteLine("dark");
            }

            if (App.ThemeID == 1)
            {
                ThemePicker.SelectedIndex = 1;                
                Debug.WriteLine("light");
            }

            if (App.ThemeID == 0)
            {
                ThemePicker.SelectedIndex = 0;                
                Debug.WriteLine("custom");
            }

            

            //ThemePicker.SetBinding(Picker.ItemsSourceProperty, "Theme");
            //ThemePicker.SetBinding(Picker.SelectedItemProperty, "Системная");
            //ThemePicker.ItemDisplayBinding = new Binding("Name");

        }

        void OnPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;
            
            //App.Current.Resources = new Xamarin.Forms.Themes.LightThemeResources();
            


            if (selectedIndex != -1)
            {
                Debug.WriteLine((string)picker.ItemsSource[selectedIndex]);
                SwitchTheme(selectedIndex);               
            }
        }

        public void SwitchTheme(int themeId)
        {
            //if (Resources?.GetType() == typeof(DarkThemeResources))
            //{
            //    Resources = new LightThemeResources();
            //    return;
            //}
            //Resources = new DarkThemeResources();

                switch (themeId)
            {
                case (int)XApplicationThemes.Dark:
                    //Application.Current.Resources.MergedWith = typeof(DarkThemeResources);
                    Resources.Clear();
                    Resources = new DarkThemeResources();                    
                    App.ThemeID = 2;
                    break;
                case (int)XApplicationThemes.Light:
                    //Application.Current.Resources.MergedWith = typeof(LightThemeResources);
                    //Resources = typeof(LightThemeResources);
                   // Resources.MergedWith = typeof(LightThemeResources);
                    App.ThemeID = 1;
                    break;
                case (int)XApplicationThemes.Original:
                    //Application.Current.Resources.MergedWith = typeof(MyResourceDictionary);
                   // Resources.MergedWith = typeof(MyResourceDictionary);
                    App.ThemeID = 0;
                    break;
            }
        }
    }
}