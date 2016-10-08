﻿using NUnit.Framework;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Test.Acceptance.View.Button.Bindable
{
	[TestFixture]
	public class ButtonTextColorTests
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
			_buttonView.TextColor = UIColor.Black;
			_buttonContext.TextColor = UIColor.Blue;
			Assert.That(_buttonContext.TextColor != _buttonView.TextColor);
			_buttonView.Bind(Guacamole.View.Button.TextColorProperty, nameof(_buttonContext.TextColor));
			Assert.That(_buttonContext.TextColor == _buttonView.TextColor);
			_buttonContext.TextColor = UIColor.Brown;
			Assert.That(_buttonContext.TextColor == _buttonView.TextColor);
		}
	}
}