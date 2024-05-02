using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;

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
		private Camera _camera;
		private Camera3D _camera3D;
		private CubeModel _cubeModel;
		private GameState _gameState;
		private Effect _effect;
		private UIManager _uiManager;
		public Game1()
		{
			Graphics = new GraphicsDeviceManager(this);
			Graphics.PreferredBackBufferWidth = 1920;
			Graphics.PreferredBackBufferHeight = 1080;
			Graphics.HardwareModeSwitch = false;
			Graphics.PreferMultiSampling = true;
			Graphics.IsFullScreen = true;
			Graphics.ApplyChanges();
			_uiManager = new UIManager(new Vector2(1920, 1080), Graphics);

			Content.RootDirectory = "Content";
			IsMouseVisible = true;

			_gameState = new GameState();
		}

		protected override void Initialize()
		{
			_camera3D = new Camera3D(Graphics.GraphicsDevice);
			base.Initialize();
		}

		protected override void LoadContent()
		{
			_cubeModel = new CubeModel(Content.Load<Model>("Models/cube"));
			_effect = Content.Load<Effect>("Shaders/SimpleShader");

			_spriteBatch = new SpriteBatch(GraphicsDevice);
			_font = Content.Load<SpriteFont>("Font");

			string[] menuItems = { "Start Game", "Options", "Exit" };
			_mainMenu = new MainMenu(menuItems, _font, Graphics);
			_gameplay = new Gameplay(Graphics, GraphicsDevice);
			_gameplay.Loadcontent(Content);

		}

		protected override void Update(GameTime gameTime)
		{

			_camera3D.Update();
			_cubeModel.Update(gameTime);

			Debug.WriteLine("Buffer Width: " + Graphics.PreferredBackBufferWidth + ", Buffer Height: " + Graphics.PreferredBackBufferHeight);
			_uiManager.UpdateResolution();
			_gameState.EarlyUpdate(Graphics, gameTime, Keyboard.GetState(), Mouse.GetState());

			ushort[] widths;
			ushort[] heights;

			widths = new ushort[] { 1920, 1366, 1280, 1280 };
			heights = new ushort[] { 1080, 768, 1024, 720 };

			// This Keys [] array and for loop is a small input handler to reduce the number of if statements
			Keys[] FunctionKeys = new Keys[] { Keys.F1, Keys.F2, Keys.F3, Keys.F4 };

			// This for loop will check if one of the keys in the FunctionKeys array is pressed.
			for (byte i = 0; i < FunctionKeys.Length; i++)
			{
				// If one of the keys in FunctionKeys is pressed it will change the resolution based on the index of that key
				if (Keyboard.GetState().IsKeyDown(FunctionKeys[i]))
				{
					ChangeResolution(i);
				}
			}

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

		void ChangeResolution(byte newResolution)
		{
			ushort[] widths;
			ushort[] heights;

			widths = new ushort[] { 1920, 1366, 1280, 1280 };
			heights = new ushort[] { 1080, 768, 1024, 720 };

			// Only change resolution if the newResolution is between 0 and the length of the widths array
			if (newResolution >= 0 && newResolution < widths.Length)
			{
				// Change the width and height to the new values in the array
				Graphics.PreferredBackBufferWidth = widths[newResolution];
				Graphics.PreferredBackBufferHeight = heights[newResolution];
				GraphicsDevice.Viewport = new Viewport(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height);
				
				// Apply the changes
				Graphics.ApplyChanges();
			}
		}

		protected override void Draw(GameTime gameTime)
		{
			
			GraphicsDevice.Clear(Color.CornflowerBlue);
			_effect.CurrentTechnique.Passes[0].Apply();

			Matrix worldMatrix = Matrix.Identity;
			Matrix viewMatrix = _camera3D.ViewMatrix;
			Matrix projectionMatrix = _camera3D.ProjectionMatrix;
			Vector4 ambientColor = new Vector4(0.1f, 0.1f, 0.1f, 1.0f);
			Vector3 lightDirection = new Vector3(0.5f, -1.0f, 0.5f);
			Vector4 diffuseColor = new Vector4(1.0f, 0.0f, 0.0f, 1.0f);

			//_effect.Parameters["World"].SetValue(worldMatrix);
			//_effect.Parameters["View"].SetValue(viewMatrix);
			//_effect.Parameters["Projection"].SetValue(projectionMatrix);
			_effect.Parameters["AmbientColor"].SetValue(ambientColor);
			_effect.Parameters["LightDirection"].SetValue(lightDirection);
			_effect.Parameters["DiffuseColor"].SetValue(diffuseColor);

			_cubeModel.Draw(_camera3D, _effect);
			switch (_gameState.CurrentScene)
			{
				case SceneType.MainMenu:
					_mainMenu.Draw(_spriteBatch);
					break;
				case SceneType.Gameplay:
					_gameplay.Draw(_spriteBatch, _uiManager);
					break;
			}

			base.Draw(gameTime);
		}
	}
}