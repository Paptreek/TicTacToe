using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary.Input;
using System.Collections.Generic;
using Gum.Forms;
using MonoGameGum;
using System;
using System.Diagnostics;

namespace TicTacToe
{
    public class TicTacToeGame : Game
    {
        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _textureAtlas;
        private GameBoard _gameBoard;
        private MouseInfo _mouseInfo;
        private readonly List<Token> _xTokens = [];
        private readonly List<Token> _oTokens = [];
        private MouseState _mouseState;
        
        private Rectangle _gameBoardSourceRect = new (0, 0, 48, 48);
        private Rectangle _xTokenSourceRectangle = new (48, 0, 16, 16);
        private Rectangle _oTokenSourceRectangle = new (64, 0, 16, 16);
        
        private bool isTurnX = true;
        
        private TopLeft _topLeft;
        private TopMiddle _topMiddle;
        private TopRight _topRight;
        private Left _left;
        private Middle _middle;
        private Right _right;
        private BottomLeft _bottomLeft;
        private BottomMiddle _bottomMiddle;
        private BottomRight _bottomRight;

        public TicTacToeGame()
        {
            _graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = 400,
                PreferredBackBufferHeight = 400
            };

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
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

            CheckForWin();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.BurlyWood);

            GumService.Default.Draw();

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            _gameBoard.Draw(_spriteBatch, new Vector2(_gameBoardSourceRect.Width, _gameBoardSourceRect.Height) * 0.5f);

            //DrawBoardRectangleTest();
            //DrawTokenRectangleTest();

            foreach (Token token in _xTokens)
            {
                token.DrawOnBoard(_spriteBatch, _xTokenSourceRectangle, new Vector2(8, 8));
            }

            foreach (Token token in _oTokens)
            {
                token.DrawOnBoard(_spriteBatch, _oTokenSourceRectangle, new Vector2(8, 8));
            }

            _spriteBatch.End();


            base.Draw(gameTime);
        }

        public void DrawBoardRectangleTest()
        {
            Texture2D texture;

            texture = new Texture2D(GraphicsDevice, 1, 1);
            texture.SetData([Color.Snow]);

            _spriteBatch.Draw(
                texture,
                new Rectangle(8, 8, 120, 120),
                Color.White
            );
        }

        public void DrawTokenRectangleTest()
        {
            Texture2D texture;

            texture = new Texture2D(GraphicsDevice, 1, 1);
            texture.SetData([Color.Snow]);

            foreach (Token token in _oTokens)
            {
                _spriteBatch.Draw(
                    texture,
                    new Rectangle(
                        (int)token.TokenPosition.X - 4,
                        (int)token.TokenPosition.Y - 4,
                        (int)_oTokenSourceRectangle.Width - 8,
                        (int)_oTokenSourceRectangle.Height - 8),
                    Color.White);
            }
        }

        public void GetPlayerChoice()
        {
            Rectangle clickArea = new(
                    (int)_mouseState.Position.X - 4,
                    (int)_mouseState.Position.Y - 4,
                    (int)_xTokenSourceRectangle.Width - 8,
                    (int)_xTokenSourceRectangle.Height - 8
            );

            if (_mouseInfo.WasButtonJustPressed(MouseButton.Left))
            {
                if (clickArea.Intersects(_gameBoard.GetBounds(BoardLocation.TopLeft)) && _topLeft == TopLeft.Empty)
                {
                    if (isTurnX == true)
                    {
                        _xTokens.Add(new Token(_textureAtlas, _gameBoard.GetLocation(BoardLocation.TopLeft)));
                        isTurnX = false;
                        _topLeft = TopLeft.PlayedByX;
                    }
                    else
                    {
                        _oTokens.Add(new Token(_textureAtlas, _gameBoard.GetLocation(BoardLocation.TopLeft)));
                        isTurnX = true;
                        _topLeft = TopLeft.PlayedByO;
                    }
                }

                if (clickArea.Intersects(_gameBoard.GetBounds(BoardLocation.TopMiddle)) && _topMiddle == TopMiddle.Empty)
                {
                    if (isTurnX == true)
                    {
                        _xTokens.Add(new Token(_textureAtlas, _gameBoard.GetLocation(BoardLocation.TopMiddle)));
                        isTurnX = false;
                        _topMiddle = TopMiddle.PlayedByX;
                    }
                    else
                    {
                        _oTokens.Add(new Token(_textureAtlas, _gameBoard.GetLocation(BoardLocation.TopMiddle)));
                        isTurnX = true;
                        _topMiddle = TopMiddle.PlayedByO;
                    }
                }

                if (clickArea.Intersects(_gameBoard.GetBounds(BoardLocation.TopRight)) && _topRight == TopRight.Empty)
                {
                    if (isTurnX == true)
                    {
                        _xTokens.Add(new Token(_textureAtlas, _gameBoard.GetLocation(BoardLocation.TopRight)));
                        isTurnX = false;
                        _topRight = TopRight.PlayedByX;
                    }
                    else
                    {
                        _oTokens.Add(new Token(_textureAtlas, _gameBoard.GetLocation(BoardLocation.TopRight)));
                        isTurnX = true;
                        _topRight = TopRight.PlayedByO;
                    }
                }

                if (clickArea.Intersects(_gameBoard.GetBounds(BoardLocation.Left)) && _left == Left.Empty)
                {
                    if (isTurnX == true)
                    {
                        _xTokens.Add(new Token(_textureAtlas, _gameBoard.GetLocation(BoardLocation.Left)));
                        isTurnX = false;
                        _left = Left.PlayedByX;
                    }
                    else
                    {
                        _oTokens.Add(new Token(_textureAtlas, _gameBoard.GetLocation(BoardLocation.Left)));
                        isTurnX = true;
                        _left = Left.PlayedByO;
                    }
                }

                if (clickArea.Intersects(_gameBoard.GetBounds(BoardLocation.Middle)) && _middle == Middle.Empty)
                {
                    if (isTurnX == true)
                    {
                        _xTokens.Add(new Token(_textureAtlas, _gameBoard.GetLocation(BoardLocation.Middle)));
                        isTurnX = false;
                        _middle = Middle.PlayedByX;
                    }
                    else
                    {
                        _oTokens.Add(new Token(_textureAtlas, _gameBoard.GetLocation(BoardLocation.Middle)));
                        isTurnX = true;
                        _middle = Middle.PlayedByO;
                    }
                }

                if (clickArea.Intersects(_gameBoard.GetBounds(BoardLocation.Right)) && _right == Right.Empty)
                {
                    if (isTurnX == true)
                    {
                        _xTokens.Add(new Token(_textureAtlas, _gameBoard.GetLocation(BoardLocation.Right)));
                        isTurnX = false;
                        _right = Right.PlayedByX;
                    }
                    else
                    {
                        _oTokens.Add(new Token(_textureAtlas, _gameBoard.GetLocation(BoardLocation.Right)));
                        isTurnX = true;
                        _right = Right.PlayedByO;
                    }
                }

                if (clickArea.Intersects(_gameBoard.GetBounds(BoardLocation.BottomLeft)) && _bottomLeft == BottomLeft.Empty)
                {
                    if (isTurnX == true)
                    {
                        _xTokens.Add(new Token(_textureAtlas, _gameBoard.GetLocation(BoardLocation.BottomLeft)));
                        isTurnX = false;
                        _bottomLeft = BottomLeft.PlayedByX;
                    }
                    else
                    {
                        _oTokens.Add(new Token(_textureAtlas, _gameBoard.GetLocation(BoardLocation.BottomLeft)));
                        isTurnX = true;
                        _bottomLeft = BottomLeft.PlayedByO;
                    }
                }

                if (clickArea.Intersects(_gameBoard.GetBounds(BoardLocation.BottomMiddle)) && _bottomMiddle == BottomMiddle.Empty)
                {
                    if (isTurnX == true)
                    {
                        _xTokens.Add(new Token(_textureAtlas, _gameBoard.GetLocation(BoardLocation.BottomMiddle)));
                        isTurnX = false;
                        _bottomMiddle = BottomMiddle.PlayedByX;
                    }
                    else
                    {
                        _oTokens.Add(new Token(_textureAtlas, _gameBoard.GetLocation(BoardLocation.BottomMiddle)));
                        isTurnX = true;
                        _bottomMiddle = BottomMiddle.PlayedByO;
                    }
                }

                if (clickArea.Intersects(_gameBoard.GetBounds(BoardLocation.BottomRight)) && _bottomRight == BottomRight.Empty)
                {
                    if (isTurnX == true)
                    {
                        _xTokens.Add(new Token(_textureAtlas, _gameBoard.GetLocation(BoardLocation.BottomRight)));
                        isTurnX = false;
                        _bottomRight = BottomRight.PlayedByX;
                    }
                    else
                    {
                        _oTokens.Add(new Token(_textureAtlas, _gameBoard.GetLocation(BoardLocation.BottomRight)));
                        isTurnX = true;
                        _bottomRight = BottomRight.PlayedByO;
                    }
                }
            }
        }

        public void CheckForWin()
        {
            if (_topLeft == TopLeft.PlayedByX && _topMiddle == TopMiddle.PlayedByX && _topRight == TopRight.PlayedByX ||
                _left == Left.PlayedByX && _middle == Middle.PlayedByX && _right == Right.PlayedByX ||
                _bottomLeft == BottomLeft.PlayedByX && _bottomMiddle == BottomMiddle.PlayedByX && _bottomRight == BottomRight.PlayedByX ||
                _topLeft == TopLeft.PlayedByX && _left == Left.PlayedByX && _bottomLeft == BottomLeft.PlayedByX ||
                _topMiddle == TopMiddle.PlayedByX && _middle == Middle.PlayedByX && _bottomMiddle == BottomMiddle.PlayedByX ||
                _topRight == TopRight.PlayedByX && _right == Right.PlayedByX && _bottomRight == BottomRight.PlayedByX ||
                _topLeft == TopLeft.PlayedByX && _middle == Middle.PlayedByX && _bottomRight == BottomRight.PlayedByX ||
                _topRight == TopRight.PlayedByX && _middle == Middle.PlayedByX && _bottomLeft == BottomLeft.PlayedByX)
            {
                EndGame("X");
            }
            else if (_topLeft == TopLeft.PlayedByO && _topMiddle == TopMiddle.PlayedByO && _topRight == TopRight.PlayedByO ||
                _left == Left.PlayedByO && _middle == Middle.PlayedByO && _right == Right.PlayedByO ||
                _bottomLeft == BottomLeft.PlayedByO && _bottomMiddle == BottomMiddle.PlayedByO && _bottomRight == BottomRight.PlayedByO ||
                _topLeft == TopLeft.PlayedByO && _left == Left.PlayedByO && _bottomLeft == BottomLeft.PlayedByO ||
                _topMiddle == TopMiddle.PlayedByO && _middle == Middle.PlayedByO && _bottomMiddle == BottomMiddle.PlayedByO ||
                _topRight == TopRight.PlayedByO && _right == Right.PlayedByO && _bottomRight == BottomRight.PlayedByO ||
                _topLeft == TopLeft.PlayedByO && _middle == Middle.PlayedByO && _bottomRight == BottomRight.PlayedByO ||
                _topRight == TopRight.PlayedByO && _middle == Middle.PlayedByO && _bottomLeft == BottomLeft.PlayedByO)
            {
                EndGame("O");
            }
        }

        public void EndGame(string winner)
        {
            Debug.WriteLine($"Player {winner} Wins!");
        }
    }
}
