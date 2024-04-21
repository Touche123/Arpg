using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Arpg
{
	public enum SceneType
	{
		MainMenu,
		Gameplay,
		Options
	}

	public class GameState
	{
		public KeyboardState CurrentKeyboardState { get; set; }
		public KeyboardState PreviousKeyboardState { get; set; }
		public MouseState CurrentMouseState { get; set; }
		public MouseState PreviousMouseState { get; set; }
		public GameTime CurrentGametime { get; set; }
		public SceneType CurrentScene { get; set; }
		public int ScreenWidth { get; set; }
		public int ScreenHeight { get; set; }

		public GameState()
		{
			CurrentScene = SceneType.MainMenu;
		}

		public void EarlyUpdate(GraphicsDeviceManager graphics, GameTime gameTime, KeyboardState keyboardState, MouseState mouseState)
		{
			CurrentGametime = gameTime;
			CurrentKeyboardState = keyboardState;
			CurrentMouseState = mouseState;

			ScreenWidth = graphics.PreferredBackBufferWidth;
			ScreenHeight = graphics.PreferredBackBufferHeight;
		}

		public void LateUpdate()
		{
			PreviousKeyboardState = CurrentKeyboardState;
			PreviousMouseState = CurrentMouseState;
		}
	}

	public class Game1 : Game
	{
		public GraphicsDeviceManager Graphics { get; set; }
		private SpriteBatch _spriteBatch;
		private SpriteFont _font;
		private MainMenu _mainMenu;
		private Gameplay _gameplay;

		private GameState _gameState;
		public Game1()
		{
			Graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			IsMouseVisible = true;

			_gameState = new GameState();
		}

		protected override void Initialize()
		{
			// TODO: Add your initialization logic here

			base.Initialize();
		}

		protected override void LoadContent()
		{
			_spriteBatch = new SpriteBatch(GraphicsDevice);
			_font = Content.Load<SpriteFont>("Font");

			string[] menuItems = { "Start Game", "Options", "Exit" };
			_mainMenu = new MainMenu(menuItems, _font, Graphics);
			_gameplay = new Gameplay(Graphics);
			_gameplay.Loadcontent(Content);
		}

		protected override void Update(GameTime gameTime)
		{
			_gameState.EarlyUpdate(Graphics, gameTime, Keyboard.GetState(), Mouse.GetState());

			switch (_gameState.CurrentScene)
			{
				case SceneType.MainMenu:
					_mainMenu.Update(_gameState);
					break;
				case SceneType.Gameplay:
					_gameplay.Update(_gameState);
					break;
			}

			if (_mainMenu.ShouldExit)
				Exit();

			_gameState.LateUpdate();

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			switch (_gameState.CurrentScene)
			{
				case SceneType.MainMenu:
					_mainMenu.Draw(_spriteBatch);
					break;
				case SceneType.Gameplay:
					_gameplay.Draw(_spriteBatch);
					break;
			}

			base.Draw(gameTime);
		}
	}
}