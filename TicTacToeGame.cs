using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary.Input;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

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

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _textureAtlas = Content.Load<Texture2D>("images/textures");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _mouseState = Mouse.GetState();
            _mouseInfo.Update();

            if (_mouseInfo.WasButtonJustPressed(MouseButton.Left) && isTurnX == true)
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
