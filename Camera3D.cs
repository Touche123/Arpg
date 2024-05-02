using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arpg
{
	public class Camera3D
	{
		public Vector3 Position { get; set; }
		public Vector3 Target { get; set; }
		public Vector3 Up { get; set; }

		public Matrix ViewMatrix { get; private set; }
		public Matrix ProjectionMatrix { get; private set; }

		public Camera3D(GraphicsDevice graphicsDevice)
		{
			Position = new Vector3(0, 0, 5);
			Target = Vector3.Zero;

			ViewMatrix = Matrix.CreateLookAt(Position, Target, Vector3.Up);
			ProjectionMatrix = Matrix.CreatePerspectiveFieldOfView(
				MathHelper.ToRadians(45),
				graphicsDevice.Viewport.AspectRatio,
				0.1f,
				1000f);

		}

		public void Update()
		{
			ViewMatrix = Matrix.CreateLookAt(Position, Target, Vector3.Up);
		}
	}
}
