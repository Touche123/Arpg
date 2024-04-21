using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Arpg
{
	public class Player
	{
		public Texture2D Texture { get; set; }
		public Vector2 Position { get; set; }
		public float Speed { get; set; }
		public Player()
		{
			Position = new Vector2(0, 0);
			Speed = 5f;
		}

		public void LoadContent(ContentManager content)
		{
			Texture = content.Load<Texture2D>("player");
		}
	}
}
