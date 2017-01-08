﻿using NUnit.Framework;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Tests.Integration.View.TextEntry.Bindable
{
	[TestFixture]
	public class TextEntryHorizontalTextAlignTests
	{
		[SetUp]
		public void Setup()
		{
			_textEntryView = new Guacamole.View.TextEntry();
			_textEntryContext = new TextEntryContextObject();
			_textEntryView.BindingContext = _textEntryContext;
		}

		private Guacamole.View.TextEntry _textEntryView;
		private TextEntryContextObject _textEntryContext;

		[Test]
		public void IsBindable()
		{
			_textEntryView.HorizontalTextAlign = UITextAlign.End;
			_textEntryContext.HorizontalTextAlign = UITextAlign.Middle;
			Assert.That(_textEntryContext.HorizontalTextAlign != _textEntryView.HorizontalTextAlign);
			_textEntryView.Bind(Guacamole.View.TextEntry.HorizontalTextAlignProperty,
				nameof(_textEntryContext.HorizontalTextAlign));
			Assert.That(_textEntryContext.HorizontalTextAlign == _textEntryView.HorizontalTextAlign);
			_textEntryContext.HorizontalTextAlign = UITextAlign.Start;
			Assert.That(_textEntryContext.HorizontalTextAlign == _textEntryView.HorizontalTextAlign);
		}
	}
}