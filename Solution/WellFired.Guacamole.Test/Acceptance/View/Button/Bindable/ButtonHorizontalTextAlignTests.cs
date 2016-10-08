﻿using NUnit.Framework;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Test.Acceptance.View.Button.Bindable
{
	[TestFixture]
	public class ButtonHorizontalTextAlignTests
	{
		private Guacamole.View.Button _buttonView;
		private ButtonContextObject _buttonContext;

		[SetUp]
		public void OneTimeSetup()
		{
			_buttonView = new Guacamole.View.Button();
			_buttonContext = new ButtonContextObject();
			_buttonView.BindingContext = _buttonContext;
		}

		[Test]
		public void IsBindable()
		{
			_buttonView.HorizontalTextAlign = UITextAlign.End;
			_buttonContext.HorizontalTextAlign = UITextAlign.Middle;
			Assert.That(_buttonContext.HorizontalTextAlign != _buttonView.HorizontalTextAlign);
			_buttonView.Bind(Guacamole.View.Button.HorizontalTextAlignProperty, nameof(_buttonContext.HorizontalTextAlign));
			Assert.That(_buttonContext.HorizontalTextAlign == _buttonView.HorizontalTextAlign);
			_buttonContext.HorizontalTextAlign = UITextAlign.Start;
			Assert.That(_buttonContext.HorizontalTextAlign == _buttonView.HorizontalTextAlign);
		}
	}
}