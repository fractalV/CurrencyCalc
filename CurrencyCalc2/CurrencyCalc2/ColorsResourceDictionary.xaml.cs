﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CurrencyCalc2
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MyResourceDictionary : ResourceDictionary
    {
		public MyResourceDictionary ()
		{
			InitializeComponent ();
		}
	}
}