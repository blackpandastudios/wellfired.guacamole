﻿using JetBrains.Annotations;

namespace WellFired.Guacamole.Drawing.Shapes
{
	[PublicAPI]
	public interface IRasterizableShape
	{
		void Rasterize(byte[] byteData, int width, int height);
	}
}