﻿using System.ComponentModel;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Diagnostics;
using WellFired.Guacamole.Platform;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Examples.Simple.LabelExample
{
	public class LabelTestWindow : Window
	{
		public LabelTestWindow(ILogger logger, INotifyPropertyChanged persistantData, IPlatformProvider platformProvider) 
			: base(logger, persistantData, platformProvider)
		{
			Padding = UIPadding.Of(5);
		    Content = new Label { Padding = UIPadding.Of(5), Text = "Sausages" };
		}
	}
}