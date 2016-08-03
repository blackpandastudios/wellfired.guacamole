﻿using UnityEditor;
using UnityEngine;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.View;

namespace WellFired.Guacamole.Examples.TwoWayBindingExample
{
    public class TwoWayBindingTestWindow : Window
    {
        public TwoWayBindingTestWindow()
        {
            Padding = new UIPadding(5);

            var boundTextEntry = new TextEntry {
                BackgroundColor = UIColor.White
            };

            Content = boundTextEntry;

            const string assetPath = "Assets/guacamole-examples/TwoWayBindingExample/Editor/WindowData.asset";
            var windowData = AssetDatabase.LoadAssetAtPath<TwoWayBindingTestModel>(assetPath);
            if(windowData == null)
            {
                windowData = ScriptableObject.CreateInstance<TwoWayBindingTestModel>();
                AssetDatabase.CreateAsset(windowData, assetPath);
            }

            BindingContext = windowData;

            boundTextEntry.Bind(TextEntry.TextProperty, "BoundText", BindingMode.TwoWay);
        }
    }
}