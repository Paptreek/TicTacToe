using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary.Input;
using System.Collections.Generic;
using Gum.Forms;
using Gum.Forms.Controls;
using MonoGameLibrary;
using MonoGameGum;
using MonoGameGum.GueDeriving;
using System;

namespace TicTacToe
{
    public class TicTacToeGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _textureAtlas;
        private List<TokenX> _tokensX = new();
        private List<TokenO> _tokensO = new();
        private Rectangle _gameBoard = new Rectangle(0, 0, 48, 48);
        private Rectangle _xTokenSourceRectangle = new Rectangle(48, 0, 16, 16);
        private Rectangle _oTokenSourceRectangle = new Rectangle(64, 0, 16, 16);
        private MouseState _mouseState;
        private MouseInfo _mouseInfo;
        private bool isTurnX = true;
        private bool isTopLeftClicked = false;

        // UI Stuff
        private Panel _gamePanel;
        private Button _topLeftButton;

        public TicTacToeGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 400;
            _graphics.PreferredBackBufferHeight = 400;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _mouseInfo = new MouseInfo();
            GumService.Default.Initialize(this, DefaultVisualsVersion.V3);
            GumService.Default.ContentLoader.XnaContentManager = Content;

            CreateGamePanel();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _textureAtlas = Content.Load<Texture2D>("images/textures");
        }

        private void CreateGamePanel()
        {
            _gamePanel = new Panel();
            _gamePanel.Anchor(Gum.Wireframe.Anchor.Center);
            _gamePanel.WidthUnits = Gum.DataTypes.DimensionUnitType.Absolute;
            _gamePanel.HeightUnits = Gum.DataTypes.DimensionUnitType.Absolute;
            _gamePanel.Width = 384;
            _gamePanel.Height = 384;
            _gamePanel.IsVisible = true;
            _gamePanel.AddToRoot();

            //var background = new ColoredRectangleRuntime();
            //background.Dock(Gum.Wireframe.Dock.Fill);
            //background.Color = Color.ForestGreen;
            //background.Alpha = 100;
            //_gamePanel.AddChild(background);

            _topLeftButton = new Button();
            _topLeftButton.Text = "";
            _topLeftButton.Anchor(Gum.Wireframe.Anchor.TopLeft);
            _topLeftButton.X = 5;
            _topLeftButton.Y = 5;
            _topLeftButton.Width = 110;
            _topLeftButton.Height = 105;
            _topLeftButton.Click += HandleTopLeftButtonClicked;
            _gamePanel.AddChild(_topLeftButton);
        }

        public void HandleTopLeftButtonClicked(object sender, EventArgs e)
        {
            isTopLeftClicked = true;
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _mouseState = Mouse.GetState();
            _mouseInfo.Update();

            GumService.Default.Update(gameTime);

            if (_mouseInfo.WasButtonJustPressed(MouseButton.Left) && isTurnX == true && isTopLeftClicked == false)
            {
                _tokensX.Add(new TokenX(_textureAtlas, new Vector2(_mouseState.Position.X, _mouseState.Position.Y)));
                isTurnX = false;
            }
            else if (_mouseInfo.WasButtonJustPressed(MouseButton.Left) && isTurnX == false)
            {
                _tokensO.Add(new TokenO(_textureAtlas, new Vector2(_mouseState.Position.X, _mouseState.Position.Y)));
                isTurnX = true;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.BurlyWood);

            GumService.Default.Draw();

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            _spriteBatch.Draw(
                _textureAtlas,
                new Vector2(Window.ClientBounds.Width * 0.5f, Window.ClientBounds.Height * 0.5f),
                _gameBoard,
                Color.White,
                0.0f,
                new Vector2(_gameBoard.Width * 0.5f, _gameBoard.Height * 0.5f),
                8.0f,
                SpriteEffects.None,
                0.0f);

            foreach (TokenX token in _tokensX)
            {
                token.Draw(_spriteBatch, _xTokenSourceRectangle, new Vector2(8, 8));
            }

            foreach (TokenO token in _tokensO)
            {
                token.Draw(_spriteBatch, _oTokenSourceRectangle, new Vector2(8, 8));
            }

            _spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
