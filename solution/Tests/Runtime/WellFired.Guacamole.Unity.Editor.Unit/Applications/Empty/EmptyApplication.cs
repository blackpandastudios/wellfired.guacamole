﻿using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Unity.Editor.Tests.Applications.Empty
{
	[UsedImplicitly]
	public class EmptyApplication : LaunchableApplication
	{
		[UsedImplicitly]
		public static IApplication Launch()
		{
			return Launch<EmptyWindow>(UIRect.With(50, 50, 600, 300), UISize.Of(260, 60));
		}
	}
}