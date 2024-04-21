using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Arpg
{
	internal class Gameplay
	{
		Player _player;
		private Camera _camera;
		private Texture2D _bgTexture;

		public Gameplay(GraphicsDeviceManager graphics)
		{
			_player = new Player();
			_camera = new Camera(graphics);
		}

		public void Loadcontent(ContentManager content)
		{
			_player.LoadContent(content);
			_bgTexture = content.Load<Texture2D>("bg");
		}
		public void Update(GameState gameState)
		{
			if (gameState.CurrentKeyboardState.IsKeyDown(Keys.Escape) && !gameState.PreviousKeyboardState.IsKeyDown(Keys.Escape))
			{
				gameState.CurrentScene = SceneType.MainMenu;
			}

			Vector2 movement = Vector2.Zero;

			if (gameState.CurrentKeyboardState.IsKeyDown(Keys.W)) {
				movement.Y -= 1;
			}
			if (gameState.CurrentKeyboardState.IsKeyDown(Keys.S))
			{
				movement.Y += 1;
			}
			if (gameState.CurrentKeyboardState.IsKeyDown(Keys.A))
			{
				movement.X -= 1;
			}
			if (gameState.CurrentKeyboardState.IsKeyDown(Keys.D))
			{
				movement.X += 1;
			}

			if (movement != Vector2.Zero)
				movement.Normalize();

			_player.Position += movement * _player.Speed;
			_camera.Position += movement * _player.Speed;
			_camera.Update(gameState.CurrentGametime);
		}

		public void Draw(SpriteBatch spriteBatch) 
		{
			spriteBatch.Begin(transformMatrix: _camera.GetViewMatrix());
			spriteBatch.Draw(_bgTexture, new Vector2(0.0f, 0.0f), Color.White);
			spriteBatch.Draw(_player.Texture, _player.Position, Color.White);
			spriteBatch.End();
		}
	}
}
