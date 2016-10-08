﻿using System;
using NUnit.Framework;

namespace WellFired.Guacamole.Test.Acceptance.View.Slider.Bindable
{
	[TestFixture]
	public class SliderThumbCornerRadiusTests
	{
		private Guacamole.View.Slider _sliderView;
		private SliderContextObject _sliderContext;

		[SetUp]
		public void OneTimeSetup()
		{
			_sliderView = new Guacamole.View.Slider();
			_sliderContext = new SliderContextObject();
			_sliderView.BindingContext = _sliderContext;
		}

		[Test]
		public void IsBindable()
		{
			_sliderView.ThumbCornerRadius = 0.0;
			_sliderContext.ThumbCornerRadius = 1.0;
			Assert.That(Math.Abs(_sliderContext.ThumbCornerRadius - _sliderView.ThumbCornerRadius) > 0.001);
			_sliderView.Bind(Guacamole.View.Slider.ThumbCornerRadiusProperty, nameof(_sliderContext.ThumbCornerRadius));
			Assert.That(Math.Abs(_sliderContext.ThumbCornerRadius - _sliderView.ThumbCornerRadius) < 0.001);
			_sliderContext.ThumbCornerRadius = 2.0;
			Assert.That(Math.Abs(_sliderContext.ThumbCornerRadius - _sliderView.ThumbCornerRadius) < 0.001);
		}
	}
}