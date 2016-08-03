﻿using System;
using WellFired.Guacamole.Annotations;

namespace WellFired.Guacamole.Types
{
    // ReSharper disable once InconsistentNaming
    public partial struct UIColor
    {
        [PublicAPI]
        public float R { get; set; }

        [PublicAPI]
        public float G { get; set; }

        [PublicAPI]
        public float B { get; set; }

        [PublicAPI]
        public float A { get; set; }

        // ReSharper disable once InconsistentNaming
        [PublicAPI]
        public static UIColor FromRGB(int red, int green, int blue)
        {
            return new UIColor {
                R = red / 255.0f,
                G = green / 255.0f,
                B = blue / 255.0f,
                A = 255.0f
            };
        }

        // ReSharper disable once InconsistentNaming
        [PublicAPI]
        public static UIColor FromRGBA(int red, int green, int blue, int alpha)
        {
            return new UIColor {
                R = red / 255.0f,
                G = green / 255.0f,
                B = blue / 255.0f,
                A = alpha / 255.0f
            };
        }

        public override bool Equals(object obj)
        {
            var compareTo = (UIColor)obj;
            return Math.Abs(compareTo.R - R) < 0.01f && Math.Abs(compareTo.G - G) < 0.01f &&
                   Math.Abs(compareTo.B - B) < 0.01f && Math.Abs(compareTo.A - A) < 0.01f;
        }

        public override int GetHashCode()
        {
            return (int)(R * 255.0f) ^ (int)(G * 255.0f) ^ (int)(B * 255.0f) ^ (int)(A * 255.0f);
        }

        public static bool operator ==(UIColor a, UIColor b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(UIColor a, UIColor b)
        {
            return !(a == b);
        }
    }
}