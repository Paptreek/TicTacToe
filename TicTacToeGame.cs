using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

namespace TicTacToe
{
    public class TicTacToeGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _textureAtlas;
        private List<Token> _tokens = new();
        private Rectangle _xTokenSourceRectangle = new Rectangle(48, 0, 16, 16);
        private Rectangle _oTokenSourceRectangle = new Rectangle(64, 0, 16, 16);
        private MouseState _mouseState;

        public TicTacToeGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

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

            if (_mouseState.LeftButton == ButtonState.Pressed)
            {
                _tokens.Add(new Token(_textureAtlas, new Vector2(_mouseState.Position.X, _mouseState.Position.Y)));
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.BurlyWood);

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            foreach(Token token in _tokens)
            {
                token.Draw(_spriteBatch);
            }

            _spriteBatch.Draw(
                _textureAtlas,
                new Vector2(_xTokenSourceRectangle.Width * 4.0f + 10, 64),
                _oTokenSourceRectangle,
                Color.White,
                0.0f,
                Vector2.One,
                4.0f,
                SpriteEffects.None,
                0.0f
            );

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
