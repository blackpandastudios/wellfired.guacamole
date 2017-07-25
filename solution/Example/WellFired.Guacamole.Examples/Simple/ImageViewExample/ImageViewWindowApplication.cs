﻿using UnityEditor;
using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Unity.Editor;

namespace WellFired.Guacamole.Examples.Simple.ImageViewExample
{
	[UsedImplicitly]
	public class ImageViewWindowApplication : LaunchableApplication
	{
		[MenuItem("Window/guacamole/Test/Image View Test")]
		[UsedImplicitly]
		private static void Launch()
		{
			Launch<ImageViewWindow>(
				uiRect: UIRect.With(50, 50, 200, 200),
				minSize: UISize.Of(200, 200),
				title: "ImageView");
		}
	}
}