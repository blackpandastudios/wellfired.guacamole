﻿using System;
using System.ComponentModel;
using System.Globalization;

namespace WellFired.Guacamole.Data.Converters
{
	// ReSharper disable once InconsistentNaming
	public class UIPaddingConverter : TypeConverter
	{
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(int);
		}

		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture,
			object value)
		{
			if (value is int)
				return UIPadding.Of((int) value);

			return base.ConvertFrom(context, culture, value);
		}
	}
}