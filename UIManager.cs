using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Arpg
{
	internal class UIManager
	{
		private Vector2 _referenceResolution;
		private Vector2 _screenResolution;
		private Matrix _scaleMatrix;
		private float _scaleFactor;
		private GraphicsDeviceManager _graphicsDeviceManager;

		public UIManager(Vector2 referenceResolution, GraphicsDeviceManager graphicsDeviceManager)
		{
			_referenceResolution = referenceResolution;
			_graphicsDeviceManager = graphicsDeviceManager;
			CalculateScaleMatrix();
		}

		private void CalculateScaleMatrix()
		{
			float scaleX = _graphicsDeviceManager.PreferredBackBufferWidth / _referenceResolution.X;
			float scaleY = _graphicsDeviceManager.PreferredBackBufferHeight / _referenceResolution.Y;
			_scaleFactor = Math.Min(scaleX, scaleY);
			_scaleMatrix = Matrix.CreateScale(_scaleFactor, _scaleFactor, 1f);
		}

		public void UpdateResolution()
		{
			CalculateScaleMatrix();
		}

		public Vector2 ScaleVector(Vector2 vector)
		{
			return vector * _scaleFactor;
		}

		public Rectangle ScaleRectangle(Rectangle rectangle)
		{
			Vector2 position = ScaleVector(new Vector2(rectangle.X, rectangle.Y));
			Vector2 size = ScaleVector(new Vector2(rectangle.Width, rectangle.Height));
			return new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
		}

		public Matrix GetScaleMatrix()
		{
			return _scaleMatrix;
		}
	}
}
