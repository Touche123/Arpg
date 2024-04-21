using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arpg
{
	internal class MenuItem
	{
		public string Text { get; set; }

		private Rectangle _bounds;
		private Color _color;
		public bool IsHighlighted { get; set; }

		public MenuItem(string text, Rectangle bounds)
		{
			Text = text;
			_bounds = bounds;
			_color = Color.White;
		}

		public void Update(MouseState mouseState)
		{
			IsHighlighted = _bounds.Contains(mouseState.Position);
		}

		public void Draw(SpriteBatch spriteBatch, SpriteFont font)
		{
			spriteBatch.DrawString(font, Text, new Vector2(_bounds.X, _bounds.Y), IsHighlighted ? Color.Yellow : _color);
		}
	}

	internal class MainMenu
	{
		private SpriteFont _font;
		private readonly MenuItem[] _items;
		private int _selectedIndex;
		public bool ShouldExit { get; set; }
		public bool ShouldStartGame { get; set; }
		public MainMenu(string[] itemTexts, SpriteFont font, GraphicsDeviceManager graphics)
		{
			_font = font;
			_items = new MenuItem[itemTexts.Length];
			for (int i = 0; i < itemTexts.Length; i++)
			{

				_items[i] = new MenuItem(itemTexts[i], new Rectangle((graphics.PreferredBackBufferWidth / 2) - 50, 100 + i * 40, 200, 30));
			}

			_selectedIndex = 0;
		}

		public void Update(GameState gameState)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || gameState.CurrentKeyboardState.IsKeyDown(Keys.Escape) && !gameState.PreviousKeyboardState.IsKeyDown(Keys.Escape))
			{
				gameState.CurrentScene = SceneType.Gameplay;
				return;
			}

			for (int i = 0; i < _items.Length; i++)
			{
				_items[i].Update(gameState.CurrentMouseState);
				if (_items[i].IsHighlighted && gameState.CurrentMouseState.LeftButton == ButtonState.Pressed)
				{
					_selectedIndex = i;

					if (_selectedIndex == 0)
						gameState.CurrentScene = SceneType.Gameplay;

					if (_selectedIndex == 2)
						ShouldExit = true;

					Debug.WriteLine("Selected: " + _items[_selectedIndex].Text);
					//Console.WriteLine("Selected: " + _items[_selectedIndex].Text);
				}
			}
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Begin();
			for (int i = 0; i < _items.Length; i++)
			{
				_items[i].Draw(spriteBatch, _font);
			}
			spriteBatch.End();
		}
	}
}
