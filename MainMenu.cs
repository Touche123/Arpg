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

		public MainMenu(string[] itemTexts, SpriteFont font)
		{
			_font = font;
			_items = new MenuItem[itemTexts.Length];
			for (int i = 0; i < itemTexts.Length; i++)
			{
				_items[i]  =new MenuItem(itemTexts[i], new Rectangle(100, 100 + i * 40, 200, 30));
			}

			_selectedIndex = 0;
		}

		public void Update(MouseState mouseState)
		{
			for (int i = 0; i < _items.Length; i++)
			{
				_items[i].Update(mouseState);
				if (_items[i].IsHighlighted && mouseState.LeftButton == ButtonState.Pressed)
				{
					_selectedIndex = i;
					Debug.WriteLine("Selected: " + _items[_selectedIndex].Text);
					//Console.WriteLine("Selected: " + _items[_selectedIndex].Text);
				}
			}
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			for (int i = 0; i < _items.Length; i++)
			{
				_items[i].Draw(spriteBatch, _font);
			}
		}
	}
}
