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

		public void EarlyUpdate(GameTime gameTime, KeyboardState keyboardState, MouseState mouseState)
		{
			CurrentGametime = gameTime;
			CurrentKeyboardState = keyboardState;
			CurrentMouseState = mouseState;
		}

		public void LateUpdate()
		{
			PreviousKeyboardState = CurrentKeyboardState;
			PreviousMouseState = CurrentMouseState;
		}
	}

	public class Game1 : Game
	{
		Texture2D playerTexture;
		private GraphicsDeviceManager _graphics;
		private SpriteBatch _spriteBatch;
		private SpriteFont _font;
		private MainMenu _mainMenu;
		private Gameplay _gameplay;
		private SceneType _currentScene;
		private GameState _gameState;
		public Game1()
		{
			_graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			IsMouseVisible = true;

			_gameState = new GameState();
			_currentScene = SceneType.MainMenu;
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
			playerTexture = Content.Load<Texture2D>("player");

			string[] menuItems = { "Start Game", "Options", "Exit" };
			_mainMenu = new MainMenu(menuItems, _font, _graphics);
			_gameplay = new Gameplay();
		}

		protected override void Update(GameTime gameTime)
		{
			_gameState.EarlyUpdate(gameTime, Keyboard.GetState(), Mouse.GetState());

			switch (_currentScene)
			{
				case SceneType.MainMenu:
					_mainMenu.Update(_gameState);
					break;
				case SceneType.Gameplay:
					_gameplay.Update(_gameState);
					break;
			}

			if (_currentScene == SceneType.MainMenu && _mainMenu.ShouldStartGame)
			{
				_mainMenu.ShouldStartGame = false;
				_currentScene = SceneType.Gameplay;
			}
			else if (_currentScene == SceneType.Gameplay && _gameplay.ShouldPause)
			{
				_gameplay.ShouldPause = false;
				_currentScene = SceneType.MainMenu;
			}

			if (_mainMenu.ShouldExit)
				Exit();

			_gameState.LateUpdate();
			// TODO: Add your update logic here

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			_spriteBatch.Begin();

			switch (_currentScene)
			{
				case SceneType.MainMenu:
					_mainMenu.Draw(_spriteBatch);
					break;
				case SceneType.Gameplay:
					_gameplay.Draw(_spriteBatch);
					_spriteBatch.Draw(playerTexture, new Vector2(0, 0), Color.White);
					break;
			}
			_spriteBatch.End();

			// TODO: Add your drawing code here

			base.Draw(gameTime);
		}
	}
}