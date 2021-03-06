﻿using NUnit.Framework;
using WellFired.Guacamole.DataBinding;

namespace WellFired.Guacamole.Unit.Bindable
{
	[TestFixture]
	public class DoubleBindableObjectTests
	{
		private class BindableTestObject : BindableObject
		{
			public static readonly BindableProperty DoubleProperty = BindableProperty.Create<BindableTestObject, double>(
				0.0,
				BindingMode.TwoWay,
				testObject => testObject.Value
			);

			public double Value
			{
				get => (double) GetValue(DoubleProperty);
				set => SetValue(DoubleProperty, value);
			}
		}

		private class ContextObject : ObservableBase
		{
			private double _currentValue;

			public double Value
			{
				get => _currentValue;
				set => SetProperty(ref _currentValue, value);
			}
		}

		[Test]
		public void OneWayBindingInverseTest()
		{
			var source = new BindableTestObject();
			var bindingContext = new ContextObject();
			source.BindingContext = bindingContext;
			source.Bind(BindableTestObject.DoubleProperty, nameof(ContextObject.Value), BindingMode.OneWay);
			bindingContext.Value = 10.0;

			Assert.That(source.Value, Is.EqualTo(bindingContext.Value));

			source.Value = 15.0;
			Assert.That(bindingContext.Value, Is.Not.EqualTo(source.Value));
		}

		[Test]
		public void OneWayBindingTest()
		{
			var source = new BindableTestObject();
			var bindingContext = new ContextObject();
			source.BindingContext = bindingContext;
			source.Bind(BindableTestObject.DoubleProperty, nameof(ContextObject.Value));
			bindingContext.Value = 10.0;

			Assert.That(source.Value, Is.EqualTo(bindingContext.Value));
		}
		
		[Test]
		public void ReadOnlyBindingTest()
		{
			var source = new BindableTestObject();
			var bindingContext = new ContextObject();
			source.BindingContext = bindingContext;
			source.Bind(BindableTestObject.DoubleProperty, nameof(ContextObject.Value), BindingMode.ReadOnly);
			
			bindingContext.Value = 10.0;

			Assert.That(bindingContext.Value, Is.EqualTo(10.0));
			Assert.That(source.Value, Is.EqualTo(bindingContext.Value));

			source.Value = 15.0;
			Assert.That(source.Value, Is.EqualTo(10.0));
			Assert.That(source.Value, Is.EqualTo(bindingContext.Value));
		}

		[Test]
		public void TwoWayBindingTest()
		{
			var source = new BindableTestObject();
			var bindingContext = new ContextObject();
			source.BindingContext = bindingContext;
			source.Bind(BindableTestObject.DoubleProperty, nameof(ContextObject.Value), BindingMode.TwoWay);
			bindingContext.Value = 10.0;

			Assert.That(source.Value, Is.EqualTo(bindingContext.Value));

			source.Value = 15.0;

			Assert.That(bindingContext.Value, Is.EqualTo(source.Value));
		}
	}
}