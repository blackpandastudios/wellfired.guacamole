﻿using System;
using NUnit.Framework;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Test.Bindable.UI.ViewBase
{
	[TestFixture]
	public class ViewBaseCornerRadiusTests
	{
		private View.ViewBase _viewBase;
		private ViewBaseContextObject _viewBaseContext;

		[SetUp]
		public void OneTimeSetup()
		{
			_viewBase = new View.ViewBase();
			_viewBaseContext = new ViewBaseContextObject();
			_viewBase.BindingContext = _viewBaseContext;
		}

		[Test]
		public void OnBindViewBaseIsAutomaticallyUpdatedToTheValueOfBindingContextCornerRadius()
		{
			_viewBase.CornerRadius = 0.0f;
			_viewBaseContext.CornerRadius = 1.0f;
			Assert.That(Math.Abs(_viewBaseContext.CornerRadius - _viewBase.CornerRadius) > 0.01f);
			_viewBase.Bind(View.ViewBase.CornerRadiusProperty, nameof(_viewBaseContext.CornerRadius));
			Assert.That(Math.Abs(_viewBaseContext.CornerRadius - _viewBase.CornerRadius) < 0.01f);
		}

		[Test]
		public void ViewBaseCornerRadiusBindingWorksInOneWay()
		{
			_viewBase.Bind(View.ViewBase.CornerRadiusProperty, nameof(_viewBaseContext.CornerRadius));
			Assert.That(Math.Abs(_viewBaseContext.CornerRadius - _viewBase.CornerRadius) < 0.01f);
			_viewBaseContext.CornerRadius = 2.0f;
			Assert.That(Math.Abs(_viewBaseContext.CornerRadius - _viewBase.CornerRadius) < 0.01f);
		}

		[Test]
		public void ViewBaseCornerRadiusBindingWorksInTwoWay()
		{
			_viewBase.Bind(View.ViewBase.CornerRadiusProperty, nameof(_viewBaseContext.CornerRadius), BindingMode.TwoWay);
			Assert.That(Math.Abs(_viewBaseContext.CornerRadius - _viewBase.CornerRadius) < 0.01f);
			_viewBaseContext.CornerRadius = 2.0f;
			Assert.That(Math.Abs(_viewBaseContext.CornerRadius - _viewBase.CornerRadius) < 0.01f);
			_viewBase.CornerRadius = 3.0f;
			Assert.That(Math.Abs(_viewBaseContext.CornerRadius - _viewBase.CornerRadius) < 0.01f);
		}

		[Test]
		public void ViewBaseCornerRadiusBindingDoesntWorkInTwoWayWithOneWayMode()
		{
			_viewBase.Bind(View.ViewBase.CornerRadiusProperty, nameof(_viewBaseContext.CornerRadius));
			Assert.That(Math.Abs(_viewBaseContext.CornerRadius - _viewBase.CornerRadius) < 0.01f);
			_viewBaseContext.CornerRadius = 2.0f;
			Assert.That(Math.Abs(_viewBaseContext.CornerRadius - _viewBase.CornerRadius) < 0.01f);
			_viewBase.CornerRadius = 3.0f;
			Assert.That(Math.Abs(_viewBaseContext.CornerRadius - _viewBase.CornerRadius) > 0.01f);
		}
	}
}