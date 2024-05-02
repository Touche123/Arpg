using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arpg
{
	public class CubeModel
	{
		private Model _model;
		private Matrix _world;

		public CubeModel(Model model)
		{
			_model = model;
			_world = Matrix.Identity;
		}

		public void Update(GameTime gameTime)
		{
			// Update rotation
			float rotationSpeed = 0.5f;
			_world *= Matrix.CreateFromYawPitchRoll(rotationSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds,
													 rotationSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds,
													 rotationSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);
		}

		public void Draw(Camera3D camera3d, Effect effect)
		{
			foreach (ModelMesh mesh in _model.Meshes)
			{
				foreach (ModelMeshPart part in mesh.MeshParts)
				{
					part.Effect = effect;
					part.Effect.Parameters["World"].SetValue(_world);
					part.Effect.Parameters["View"].SetValue(camera3d.ViewMatrix);
					part.Effect.Parameters["Projection"].SetValue(camera3d.ProjectionMatrix);
				}
				mesh.Draw();
			}
		}
	}
}
