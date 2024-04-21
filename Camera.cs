using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arpg
{
	public class Camera
	{
		private GraphicsDeviceManager _graphics;

		public Matrix Transform { get; private set; }
		public float Zoom { get; set; }
		public Vector2 Position { get; set; }
		public float Rotation { get; set; }

		public Camera(GraphicsDeviceManager graphics)
		{
			_graphics = graphics;
			Zoom = 1f;
			Rotation = 0f;
			Position = Vector2.Zero;
		}

		public void Update(GameTime gameTime)
		{
			// Update camera logic here if needed
		}

		public Matrix GetViewMatrix()
		{
			return
			Matrix.CreateTranslation(new Vector3(-Position.X, -Position.Y, 0)) *
			Matrix.CreateRotationZ(Rotation) *
			Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
			Matrix.CreateTranslation(new Vector3(_graphics.PreferredBackBufferWidth * 0.5f, _graphics.PreferredBackBufferHeight * 0.5f, 0));
		}
	}
}
