﻿using System.ComponentModel;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Diagnostics;
using WellFired.Guacamole.Layouts;
using WellFired.Guacamole.Platform;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Examples.Simple.AdjacentLayoutExample
{
	public class AdjacentLayoutTestWindow : Window
	{
		public AdjacentLayoutTestWindow(ILogger logger, INotifyPropertyChanged persistantData, IPlatformProvider platformProvider) 
			: base(logger, persistantData, platformProvider)
		{
			Padding = UIPadding.Of(5);

			var textEntryStartStart = new TextEntry
			{
				Text = "h:Start v:Start Align"
			};

			var textEntryMiddleStart = new TextEntry
			{
				HorizontalTextAlign = UITextAlign.Middle,
				Text = "h:Middle v:Start Align"
			};

			var textEntryEndStart = new TextEntry
			{
				HorizontalTextAlign = UITextAlign.End,
				Text = "h:Right v:Start Align"
			};

			var textEntryStartMiddle = new TextEntry
			{
				VerticalTextAlign = UITextAlign.Middle,
				Text = "h:Start v:Middle Align"
			};

			var textEntryMiddleMiddle = new TextEntry
			{
				HorizontalTextAlign = UITextAlign.Middle,
				VerticalTextAlign = UITextAlign.Middle,
				Text = "h:Middle v:Middle Align"
			};

			var textEntryEndMiddle = new TextEntry
			{
				HorizontalTextAlign = UITextAlign.End,
				VerticalTextAlign = UITextAlign.Middle,
				Text = "h:Right v:Middle Align"
			};

			var textEntryStartEnd = new TextEntry
			{
				VerticalTextAlign = UITextAlign.End,
				Text = "h:Start v:End Align"
			};

			var textEntryMiddleEnd = new TextEntry
			{
				HorizontalTextAlign = UITextAlign.Middle,
				VerticalTextAlign = UITextAlign.End,
				Text = "h:Middle v:End Align"
			};

			var textEntryEndEnd = new TextEntry
			{
				HorizontalTextAlign = UITextAlign.End,
				VerticalTextAlign = UITextAlign.End,
				Text = "h:Right v:End Align"
			};

			Content = new LayoutView
			{
			    Layout = new AdjacentLayout { Orientation = OrientationOptions.Horizontal },
			    HorizontalLayout = LayoutOptions.Fill,
				Children =
				{
					textEntryStartStart,
					textEntryMiddleStart,
					textEntryEndStart,
					textEntryStartMiddle,
					textEntryMiddleMiddle,
					textEntryEndMiddle,
					textEntryStartEnd,
					textEntryMiddleEnd,
					textEntryEndEnd
				}
			};
		}
	}
}