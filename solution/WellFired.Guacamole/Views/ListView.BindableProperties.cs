﻿using System.ComponentModel;
using System.Diagnostics;
using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.Cells;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Layouts;

namespace WellFired.Guacamole.Views
{
    public partial class ListView
    {
        [PublicAPI] public static readonly BindableProperty SpacingProperty = BindableProperty.Create<ListView, int>(
            default(int),
            BindingMode.TwoWay,
            listView => listView.Spacing
        );
        
        [PublicAPI] public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create<ListView, INotifyPropertyChanged>(
            default(INotifyPropertyChanged),
            BindingMode.TwoWay,
            listView => listView.SelectedItem
        );
        
        [PublicAPI] public static readonly BindableProperty EntrySizeProperty = BindableProperty.Create<ListView, int>(
            50,
            BindingMode.TwoWay,
            listView => listView.EntrySize
        );

        [PublicAPI]
        public int Spacing
        {
            get { return (int) GetValue(SpacingProperty); }
            set
            {
                if (!SetValue(SpacingProperty, value)) 
                    return;
                
                var adjacentLayout = Layout as AdjacentLayout;
                if (adjacentLayout != null) 
                    adjacentLayout.Spacing = Spacing;
            }
        }

        [PublicAPI]
        public INotifyPropertyChanged SelectedItem
        {
            get { return (INotifyPropertyChanged)GetValue(SelectedItemProperty); }
            set
            {
                var oldItem = SelectedItem;
                if (!SetValue(SelectedItemProperty, value)) 
                    return;
                
                foreach (var child in Children)
                {
                    var view = child as ICell;
                    Debug.Assert(view != null, "view != null");
                    if (view.BindingContext.Equals(oldItem))
                        view.IsSelected = false;
                    else if (view.BindingContext.Equals(SelectedItem))
                        view.IsSelected = true;
                }
            }
        }

        /// <summary>
        /// The size of one Entry into this List View, for the moment, each entry should be the same size, though 
        /// this might change in the future.
        /// </summary>
        [PublicAPI]
        public int EntrySize
        {
            get { return (int) GetValue(EntrySizeProperty); }
            set { SetValue(EntrySizeProperty, value); }
        }
    }
}