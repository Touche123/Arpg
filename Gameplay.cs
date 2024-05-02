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
		private Texture2D _inventoryTexture;
		private bool _showInventory;
		private GameState _gameState;
		private Vector2 _inventoryPosition;
		private GraphicsDeviceManager _graphicsDeviceManager;
		private GraphicsDevice _graphicsDevice;
		public Gameplay(GraphicsDeviceManager graphicsDeviceManager, GraphicsDevice graphicsDevice)
		{
			_graphicsDeviceManager = graphicsDeviceManager;
			_graphicsDevice = graphicsDevice;

			_player = new Player();
			_camera = new Camera(_graphicsDeviceManager);
			_inventoryPosition = new Vector2(0, 0);
			
		}

		public void Loadcontent(ContentManager content)
		{
			_player.LoadContent(content);
			_bgTexture = content.Load<Texture2D>("bg");
			_inventoryTexture = content.Load<Texture2D>("inventory");
		}
		public void Update(GameState gameState)
		{
			_gameState = gameState;
			if (_gameState.CurrentKeyboardState.IsKeyDown(Keys.Escape) && !_gameState.PreviousKeyboardState.IsKeyDown(Keys.Escape))
			{
				_gameState.CurrentScene = SceneType.MainMenu;
			}

			Vector2 movement = Vector2.Zero;

			if (_gameState.CurrentKeyboardState.IsKeyDown(Keys.W))
			{
				movement.Y -= 1;
			}
			if (_gameState.CurrentKeyboardState.IsKeyDown(Keys.S))
			{
				movement.Y += 1;
			}
			if (_gameState.CurrentKeyboardState.IsKeyDown(Keys.A))
			{
				movement.X -= 1;
			}
			if (_gameState.CurrentKeyboardState.IsKeyDown(Keys.D))
			{
				movement.X += 1;
			}

			if (_gameState.CurrentKeyboardState.IsKeyDown(Keys.C) && !_gameState.PreviousKeyboardState.IsKeyDown(Keys.C))
			{
				_showInventory = !_showInventory;
			}

			if (movement != Vector2.Zero)
				movement.Normalize();

			_player.Position += movement * _player.Speed;
			_camera.Position += movement * _player.Speed;
			_camera.Update(_gameState.CurrentGametime);


		}

		public void Draw(SpriteBatch spriteBatch, UIManager uIManager)
		{
			spriteBatch.Begin(transformMatrix: _camera.GetViewMatrix());
			spriteBatch.Draw(_bgTexture, new Vector2(0.0f, 0.0f), Color.White);
			spriteBatch.Draw(_player.Texture, _player.Position, Color.White);
			spriteBatch.End();

			if (_showInventory)
			{
				InventoryDraw(spriteBatch, uIManager);
			}
		}

		public void InventoryDraw(SpriteBatch spriteBatch, UIManager uIManager)
		{
			//spriteBatch.Draw(_inventoryTexture, uIManager.ScaleVector(_inventoryPosition), Color.White);
			// Calculate the width to maintain aspect ratio and fill the whole height
			spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, uIManager.GetScaleMatrix());

			// Calculate the width to maintain aspect ratio and fill the whole height
			float aspectRatio = (float)_inventoryTexture.Width / _inventoryTexture.Height;
			int textureWidth = (int)(_graphicsDevice.Viewport.Height * aspectRatio);

			// Draw the texture at the top-left corner of the screen with calculated width
			spriteBatch.Draw(_inventoryTexture, new Rectangle(0, 0, textureWidth, _graphicsDevice.Viewport.Height), Color.White);
			spriteBatch.End();
		}

		public void InventoryUpdate()
		{

		}
	}
}
