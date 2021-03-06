﻿using System.Collections.Generic;
using System.ComponentModel;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Diagnostics;
using WellFired.Guacamole.Pages;
using WellFired.Guacamole.Pages.MasterDetailPage;
using WellFired.Guacamole.Platforms;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Examples.Simple.MasterDetailPageExample
{
    public class MasterDetailPageWindow : Window
    {
        public MasterDetailPageWindow(ILogger logger, INotifyPropertyChanged persistantData, IPlatformProvider platformProvider) 
            : base(logger, persistantData, platformProvider)
        {
            Padding = 0;
            
            var master = new ListView {
                ShouldShowScrollBar = false,
                ItemSource = new List<MasterPageItem> { new MasterPageItem("One", typeof(Page)), new MasterPageItem("Two", typeof(Page1)), new MasterPageItem("Three", typeof(Page2)) },
                HorizontalLayout =  LayoutOptions.Expand,
                VerticalLayout = LayoutOptions.Fill,
                MaxSize = new UISize(150, 0),
                MinSize = new UISize(150, 0)
            };
            
            var detail = new View {
                BackgroundColor = UIColor.Aquamarine,
                HorizontalLayout =  LayoutOptions.Fill,
                VerticalLayout = LayoutOptions.Fill
            };
            
            Content = new ListViewMasterDetailPage(master, detail);
        }
    }

    public class Page1 : Page
    {
        public Page1() { BackgroundColor = UIColor.Beige; }
    }

    public class Page2 : Page
    {
        public Page2() { BackgroundColor = UIColor.Brown; }
    }
}