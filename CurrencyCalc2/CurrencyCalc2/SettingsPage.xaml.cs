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

        public static string[] addresses = { "http://www.cbr.ru/scripts/XML_daily.asp", "https://www.ecb.europa.eu/stats/eurofxref/eurofxref-daily.xml" };

        public SettingsPage()
        {
            
            InitializeComponent();

            var themeList = new List<string> { "Оригинальная", "Светлая", "Темная" };
          

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

            var sourceList = new List<string>
            {
                "Центральный банк РФ",
                "Европейский центральный банк"
            };
            //sourceList.Add ("");

            SourcePicker.ItemsSource = sourceList;
            SourcePicker.SelectedIndex = 0;

        }

        void OnSourcePickerSelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;
            if (selectedIndex != -1)
            {
                Debug.WriteLine((string)picker.ItemsSource[selectedIndex]);
                if (selectedIndex == 1) {
                    //LabelMessage.Text = "Загрузка из этого источника пока невозможна"; LabelMessage.TextColor = Color.Violet;
                    App.SourceUrl = addresses[1];
                } else {
                    //LabelMessage.Text = String.Empty; 
                    App.SourceUrl = addresses[0];
                }
            }
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