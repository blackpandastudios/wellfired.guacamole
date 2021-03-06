﻿using JetBrains.Annotations;

namespace WellFired.Guacamole.Data
{
	// ReSharper disable once InconsistentNaming
	public struct UISize
	{
		public float Width { get; set; }
		public float Height { get; set; }

		[PublicAPI]
		public static UISize Min { get; } = Of(0);

		[PublicAPI]
		public static UISize Max { get; } = Of(int.MaxValue, int.MaxValue);

		[PublicAPI]
		public static UISize One { get; } = Of(1);

		[PublicAPI]
		public static UISize Zero { get; } = Of(0);

		public UISize(float width, float height) : this()
		{
			Width = width;
			Height = height;
		}
		
		public override bool Equals(object obj)
		{
			// ReSharper disable once PossibleNullReferenceException
			var compareTo = (UISize) obj;
			return MathUtil.NearEqual(compareTo.Width, Width) && MathUtil.NearEqual(compareTo.Height, Height);
		}

		[PublicAPI]
		public bool Equals(UISize other)
		{
			return MathUtil.NearEqual(Width, other.Width) && MathUtil.NearEqual(Height, other.Height);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				// ReSharper disable once NonReadonlyMemberInGetHashCode
				return (Width.GetHashCode() * 397) ^ 
				       // ReSharper disable once NonReadonlyMemberInGetHashCode
				       Height.GetHashCode();
			}
		}
		
		public static UISize operator +(UISize a, UISize b)
		{
			return Of(a.Width + b.Width, a.Height + b.Height);
		}
		
		public static UISize operator -(UISize a, UISize b)
		{
			return Of(a.Width - b.Width, a.Height - b.Height);
		}

		public static bool operator ==(UISize a, UISize b)
		{
			return a.Equals(b);
		}

		public static bool operator !=(UISize a, UISize b)
		{
			return !(a == b);
		}

		public override string ToString()
		{
			return $"{nameof(Width)}: {Width} {nameof(Height)}: {Height}";
		}

		public static UISize Of(float width, float height)
		{
			return new UISize(width, height);
		}

		public static UISize Of(float size)
		{
			return new UISize(size, size);
		}
	}
}