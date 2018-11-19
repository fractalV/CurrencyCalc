using CurrencyCalc2.Resx;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Themes;
using Xamarin.Forms.Xaml;



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
            //    "Центральный банк РФ",
            //    "Европейский центральный банк"                
                AppResources.NameSource1,
                AppResources.NameSource2
            };            

            SourcePicker.ItemsSource = sourceList;

            if (App.SourceUrl == App.cbr)
            {
                SourcePicker.SelectedIndex = 0;
            } else
            {
                SourcePicker.SelectedIndex = 1;
            }


            var lngList = new List<string>
            {
                "Deutsch", //0
                "English",
                "Español",
                "Francés",
                "Bahasa indonesia",
                "日本語",
                "Melayu",
                "Português",
                "Русский",
                "中文（繁體)",
                "中文（简体)",
            };

            LngPicker.ItemsSource = lngList;

            string cultureName = App.ci.EnglishName;
            
            if (cultureName.Contains("German"))
            {
                LngPicker.SelectedIndex = 0;
            }
            else if (cultureName.Contains("English"))
            {
                LngPicker.SelectedIndex = 1;
            }
            else if (cultureName.Contains("Spanish"))
            {
                LngPicker.SelectedIndex = 2;
            }
            else if (cultureName.Contains("French"))
            {
                LngPicker.SelectedIndex = 3;
            }
            else if (cultureName.Contains("Indonesian"))
            {
                LngPicker.SelectedIndex = 4;
            }
            else if (cultureName.Contains("Japanese"))
            {
                LngPicker.SelectedIndex = 5;
            }
            else if (cultureName.Contains("Malay"))
            {
                LngPicker.SelectedIndex = 6;
            }
            else if (cultureName.Contains("Portuguese"))
            {
                LngPicker.SelectedIndex = 7;
            }
            else if (cultureName.Contains("Russia"))
            {
                LngPicker.SelectedIndex = 8;
            }
            else if (cultureName.Contains("Chinese (Simplified"))
            {
                LngPicker.SelectedIndex = 9;
            }
            else if (cultureName.Contains("Chinese (Traditional"))
            {
                LngPicker.SelectedIndex = 10;
            } 
            
            else LngPicker.SelectedIndex = 1; //Engl

            Debug.WriteLine("App.ci.EnglishName - " + cultureName);
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
                    App.SourceUrl = App.ecb;
                } else {
                    //LabelMessage.Text = String.Empty; 
                    App.SourceUrl = App.cbr;
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
        

        private void LngPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;
            if (selectedIndex != -1)
            {
                Debug.WriteLine((string)picker.ItemsSource[selectedIndex]);                

                switch (selectedIndex)
                {
                    case 0:
                        App.ci = CultureInfo.CreateSpecificCulture("de");
                        break;
                    case 1:
                        App.ci = CultureInfo.CreateSpecificCulture("en");
                        break;
                    case 2:
                        App.ci = CultureInfo.CreateSpecificCulture("es");
                        break;
                    case 3:
                        //AppResources.Culture = new CultureInfo("fr-FR");
                        App.ci = CultureInfo.CreateSpecificCulture("fr");
                        break;
                    case 4:
                        App.ci = CultureInfo.CreateSpecificCulture("id");
                        break;
                    case 5:
                        App.ci = CultureInfo.CreateSpecificCulture("ja");
                        break;
                    case 6:
                        App.ci = CultureInfo.CreateSpecificCulture("ms");
                        break;
                    case 7:
                        App.ci = CultureInfo.CreateSpecificCulture("pt");
                        break;
                    case 8:
                        App.ci = CultureInfo.CreateSpecificCulture("ru");
                        break;
                    case 9:
                        App.ci = CultureInfo.CreateSpecificCulture("zh-Hans");
                        break;
                    case 10:
                        App.ci = CultureInfo.CreateSpecificCulture("zh-Hant");
                        break;
                    default:
                        App.ci = CultureInfo.CreateSpecificCulture("en");
                        break;
                }
                Debug.WriteLine("Установлено App.ci.EnglishName - " + App.ci.EnglishName);
                //System.Diagnostics.Debug.WriteLine(ci.EnglishName);

                Resx.AppResources.Culture = App.ci;
                LabelMessage.Text = App.ci.EnglishName; 
            }
        }
    }
}