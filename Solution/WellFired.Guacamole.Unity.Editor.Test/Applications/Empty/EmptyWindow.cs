﻿using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.View;

namespace WellFired.Guacamole.Unity.Editor.Test.Applications.Empty
{
	[UsedImplicitly]
	public class EmptyWindow : Window
	{
		public EmptyWindow()
		{
			Padding = new UIPadding(5);
			Content = new Button();
		}
	}
}