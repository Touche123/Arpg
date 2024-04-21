using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arpg
{
	internal class Gameplay
	{
		public bool ShouldPause { get; set; }
		public void Update(GameState gameState)
		{
			if (gameState.CurrentKeyboardState.IsKeyDown(Keys.Escape) && !gameState.PreviousKeyboardState.IsKeyDown(Keys.Escape))
			{
				ShouldPause = true;
			}
		}

		public void Draw(SpriteBatch spriteBatch) { }
	}
}
