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
using System.Diagnostics;
using System.Transactions;

namespace TicTacToe
{
    public class TicTacToeGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _textureAtlas;
        private GameBoard _gameBoard;
        private List<TokenX> _xTokens = new();
        private List<TokenO> _oTokens = new();
        private Rectangle _gameBoardSourceRect = new Rectangle(0, 0, 48, 48);
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
            GumService.Default.Initialize(this, DefaultVisualsVersion.V3);
            GumService.Default.ContentLoader.XnaContentManager = Content;

            _mouseInfo = new MouseInfo();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _textureAtlas = Content.Load<Texture2D>("images/textures");

            _gameBoard = new GameBoard(_textureAtlas, new Vector2(Window.ClientBounds.Width, Window.ClientBounds.Height) * 0.5f, _gameBoardSourceRect);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _mouseState = Mouse.GetState();
            _mouseInfo.Update();

            GumService.Default.Update(gameTime);

            GetPlayerChoice();

            //if (_mouseInfo.WasButtonJustPressed(MouseButton.Left) && isTurnX == true)
            //{
            //    _xTokens.Add(new TokenX(_textureAtlas, new Vector2(_mouseState.Position.X, _mouseState.Position.Y)));
            //    isTurnX = false;
            //}
            //else if (_mouseInfo.WasButtonJustPressed(MouseButton.Left) && isTurnX == false)
            //{
            //    _oTokens.Add(new TokenO(_textureAtlas, new Vector2(_mouseState.Position.X, _mouseState.Position.Y)));
            //    isTurnX = true;
            //}

            //foreach (TokenX token in _xTokens)
            //{
            //    Vector2 location = token.TokenPosition;

            //    Rectangle tokenBounds = new Rectangle(
            //            (int)token.TokenPosition.X - 16,
            //            (int)token.TokenPosition.Y - 16,
            //            (int)_xTokenSourceRectangle.Width * 2,
            //            (int)_xTokenSourceRectangle.Height * 2
            //    );

            //    if (tokenBounds.Intersects(_gameBoard.GetTopLeft()))
            //    {
            //        //token.DrawOnBoard(_spriteBatch, _gameBoard.GetTopLeftLocation(), _xTokenSourceRectangle, new Vector2(8, 8));
            //    }

            //    if (tokenBounds.Intersects(_gameBoard.GetTopMiddle()))
            //    {
            //        //token.DrawOnBoard(_spriteBatch, _gameBoard.GetTopMiddleLocation(), _xTokenSourceRectangle, new Vector2(8, 8));
            //    }
            //}

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.BurlyWood);

            GumService.Default.Draw();

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            _gameBoard.Draw(_spriteBatch, new Vector2(_gameBoardSourceRect.Width, _gameBoardSourceRect.Height) * 0.5f);

            foreach (TokenX token in _xTokens)
            {
                token.DrawOnBoard(_spriteBatch, _xTokenSourceRectangle, new Vector2(8, 8));
            }

            foreach (TokenO token in _oTokens)
            {
                token.DrawOnBoard(_spriteBatch, _oTokenSourceRectangle, new Vector2(8, 8));
            }

            //DrawBoardRectangleTest();
            //DrawTokenRectangleTest();

            //foreach (TokenX token in _xTokens)
            //{
            //    Vector2 location = token.TokenPosition;

            //    Rectangle tokenBounds = new Rectangle(
            //            (int)token.TokenPosition.X - 16,
            //            (int)token.TokenPosition.Y - 16,
            //            (int)_xTokenSourceRectangle.Width * 2,
            //            (int)_xTokenSourceRectangle.Height * 2
            //    );

            //    if (tokenBounds.Intersects(_gameBoard.GetTopLeft()))
            //    {
            //        token.DrawOnBoard(_spriteBatch, _gameBoard.GetTopLeftLocation(), _xTokenSourceRectangle, new Vector2(8, 8));
            //    }

            //    if (tokenBounds.Intersects(_gameBoard.GetTopMiddle()))
            //    {
            //        token.DrawOnBoard(_spriteBatch, _gameBoard.GetTopMiddleLocation(), _xTokenSourceRectangle, new Vector2(8, 8));
            //    }
            //}

            //foreach (TokenO token in _oTokens)
            //{
            //    Vector2 location = token.TokenPosition;

            //    Rectangle tokenBounds = new Rectangle(
            //            (int)token.TokenPosition.X - 16,
            //            (int)token.TokenPosition.Y - 16,
            //            (int)_oTokenSourceRectangle.Width * 2,
            //            (int)_oTokenSourceRectangle.Height * 2
            //    );

            //    if (tokenBounds.Intersects(_gameBoard.GetTopLeft()))
            //    {
            //        token.DrawOnBoard(_spriteBatch, _gameBoard.GetTopLeftLocation(), _oTokenSourceRectangle, new Vector2(8, 8));
            //    }

            //    if (tokenBounds.Intersects(_gameBoard.GetTopMiddle()))
            //    {
            //        token.DrawOnBoard(_spriteBatch, _gameBoard.GetTopMiddleLocation(), _oTokenSourceRectangle, new Vector2(8, 8));
            //    }
            //}

            _spriteBatch.End();


            base.Draw(gameTime);
        }

        //public void DrawBoardRectangleTest()
        //{
        //    Texture2D texture;

        //    texture = new Texture2D(GraphicsDevice, 1, 1);
        //    texture.SetData(new Color[] { Color.Snow });

        //    _spriteBatch.Draw(
        //        texture,
        //        new Rectangle(272, 272, 120, 120),
        //        Color.White
        //    );
        //}

        //public void DrawTokenRectangleTest()
        //{
        //    Texture2D texture;

        //    texture = new Texture2D(GraphicsDevice, 1, 1);
        //    texture.SetData(new Color[] { Color.Snow });

        //    foreach (TokenO token in _oTokens)
        //    {
        //        _spriteBatch.Draw(
        //            texture,
        //            new Rectangle(
        //                (int)token.TokenPosition.X - 16,
        //                (int)token.TokenPosition.Y - 16,
        //                (int)_oTokenSourceRectangle.Width * 2,
        //                (int)_oTokenSourceRectangle.Height * 2),
        //            Color.White);
        //    }
        //}

        public void GetPlayerChoice()
        {
            Rectangle clickArea = new Rectangle(
                    (int)_mouseState.Position.X - 16,
                    (int)_mouseState.Position.Y - 16,
                    (int)_xTokenSourceRectangle.Width * 2,
                    (int)_xTokenSourceRectangle.Height * 2
            );

            if (_mouseInfo.WasButtonJustPressed(MouseButton.Left) && clickArea.Intersects(_gameBoard.GetTopLeft()) && _gameBoard.IsTopLeftTaken == false && isTurnX == true)
            {
                _xTokens.Add(new TokenX(_textureAtlas, new Vector2(_gameBoard.GetTopLeftLocation().X, _gameBoard.GetTopLeftLocation().Y)));
                isTurnX = false;
                _gameBoard.IsTopLeftTaken = true;
                _gameBoard.IsTopLeftPlayedByX = true;
            }
            else if (_mouseInfo.WasButtonJustPressed(MouseButton.Left) && clickArea.Intersects(_gameBoard.GetTopLeft()) && _gameBoard.IsTopLeftTaken == false && isTurnX == false)
            {
                _oTokens.Add(new TokenO(_textureAtlas, new Vector2(_gameBoard.GetTopLeftLocation().X, _gameBoard.GetTopLeftLocation().Y)));
                isTurnX = true;
                _gameBoard.IsTopLeftTaken = true;
                _gameBoard.IsTopLeftPlayedByO = true;
            }

            if (_mouseInfo.WasButtonJustPressed(MouseButton.Left) && clickArea.Intersects(_gameBoard.GetTopMiddle()) && _gameBoard.IsTopMiddleTaken == false && isTurnX == true)
            {
                _xTokens.Add(new TokenX(_textureAtlas, new Vector2(_gameBoard.GetTopMiddleLocation().X, _gameBoard.GetTopMiddleLocation().Y)));
                isTurnX = false;
                _gameBoard.IsTopMiddleTaken = true;
                _gameBoard.IsTopMiddlePlayedByX = true;
            }
            else if (_mouseInfo.WasButtonJustPressed(MouseButton.Left) && clickArea.Intersects(_gameBoard.GetTopMiddle()) && _gameBoard.IsTopMiddleTaken == false && isTurnX == false)
            {
                _oTokens.Add(new TokenO(_textureAtlas, new Vector2(_gameBoard.GetTopMiddleLocation().X, _gameBoard.GetTopMiddleLocation().Y)));
                isTurnX = true;
                _gameBoard.IsTopMiddleTaken = true;
                _gameBoard.IsTopMiddlePlayedByO = true;
            }

            if (_mouseInfo.WasButtonJustPressed(MouseButton.Left) && clickArea.Intersects(_gameBoard.GetTopRight()) && _gameBoard.IsTopRightTaken == false && isTurnX == true)
            {
                _xTokens.Add(new TokenX(_textureAtlas, new Vector2(_gameBoard.GetTopRightLocation().X, _gameBoard.GetTopRightLocation().Y)));
                isTurnX = false;
                _gameBoard.IsTopRightTaken = true;
                _gameBoard.IsTopRightPlayedByX = true;
            }
            else if (_mouseInfo.WasButtonJustPressed(MouseButton.Left) && clickArea.Intersects(_gameBoard.GetTopRight()) && _gameBoard.IsTopRightTaken == false && isTurnX == false)
            {
                _oTokens.Add(new TokenO(_textureAtlas, new Vector2(_gameBoard.GetTopRightLocation().X, _gameBoard.GetTopRightLocation().Y)));
                isTurnX = true;
                _gameBoard.IsTopRightTaken = true;
                _gameBoard.IsTopRightPlayedByO = true;
            }
        }
    }
}
