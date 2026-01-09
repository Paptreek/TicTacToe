using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameGum;
using Gum.Forms;
using Gum.Forms.Controls;
using MonoGameLibrary;
using MonoGameLibrary.Input;

namespace TicTacToe
{
    public class TicTacToeGame : Core
    {
        private Texture2D _textureAtlas;
        private GameBoard _gameBoard;
        private MouseInfo _mouseInfo;
        private SoundEffect _popSound;
        
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

        public TicTacToeGame() : base("Swords and Shields", 400, 400, false)
        {

        }

        protected override void Initialize()
        {
            GumService.Default.Initialize(this, DefaultVisualsVersion.V3);
            GumService.Default.ContentLoader.XnaContentManager = Content;
            FrameworkElement.KeyboardsForUiControl.Add(GumService.Default.Keyboard);

            //Adjusting Gum UI to asset size(8x)
            GumService.Default.CanvasWidth = Graphics.GraphicsDevice.PresentationParameters.BackBufferWidth / 8.0f;
            GumService.Default.CanvasHeight = Graphics.GraphicsDevice.PresentationParameters.BackBufferHeight / 8.0f;
            GumService.Default.Renderer.Camera.Zoom = 8.0f;

            _mouseInfo = new MouseInfo();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _textureAtlas = Content.Load<Texture2D>("images/textures");

            _gameBoard = new GameBoard(_textureAtlas, new Vector2(Window.ClientBounds.Width, Window.ClientBounds.Height) * 0.5f, _gameBoardSourceRect);

            _popSound = Content.Load<SoundEffect>("audio/pop");

            base.LoadContent();
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

            SpriteBatch.Begin(samplerState: SamplerState.PointClamp);

            _gameBoard.Draw(SpriteBatch, new Vector2(_gameBoardSourceRect.Width, _gameBoardSourceRect.Height) * 0.5f);

            //DrawBoardRectangleTest();
            //DrawTokenRectangleTest();

            foreach (Token token in _xTokens)
            {
                token.DrawOnBoard(SpriteBatch, _xTokenSourceRectangle, new Vector2(8, 8));
            }

            foreach (Token token in _oTokens)
            {
                token.DrawOnBoard(SpriteBatch, _oTokenSourceRectangle, new Vector2(8, 8));
            }

            SpriteBatch.End();


            base.Draw(gameTime);
        }

        // I'm sure this can be shortened, but haven't spent enough time to come up with a better solution. Does the job for now.
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
                _popSound.Play();

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
        
        // LOL I hate this, but it was the first working solution that came to mind. "felt cute, might delete later"
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
                EndGame("SWORDS");
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
                EndGame("SHIELDS");
            }
            else if (_topLeft != TopLeft.Empty && _topMiddle != TopMiddle.Empty && _topRight != TopRight.Empty &&
                _left != Left.Empty && _middle != Middle.Empty && _right != Right.Empty &&
                _bottomLeft != BottomLeft.Empty && _bottomMiddle != BottomMiddle.Empty && _bottomRight != BottomRight.Empty)
            {
                EndGame("DRAW");
            }
        }

        public void EndGame(string winner)
        {
            if (winner == "SWORDS" || winner == "SHIELDS")
            {
                Debug.WriteLine($"{winner} WIN!");
            }
            else
            {
                Debug.WriteLine($"It's a draw!");
            }
        }

        //public void DrawBoardRectangleTest()
        //{
        //    Texture2D texture;

        //    texture = new Texture2D(GraphicsDevice, 1, 1);
        //    texture.SetData([Color.Snow]);

        //    _spriteBatch.Draw(
        //        texture,
        //        new Rectangle(8, 8, 120, 120),
        //        Color.White
        //    );
        //}

        //public void DrawTokenRectangleTest()
        //{
        //    Texture2D texture;

        //    texture = new Texture2D(GraphicsDevice, 1, 1);
        //    texture.SetData([Color.Snow]);

        //    foreach (Token token in _oTokens)
        //    {
        //        _spriteBatch.Draw(
        //            texture,
        //            new Rectangle(
        //                (int)token.TokenPosition.X - 4,
        //                (int)token.TokenPosition.Y - 4,
        //                (int)_oTokenSourceRectangle.Width - 8,
        //                (int)_oTokenSourceRectangle.Height - 8),
        //            Color.White);
        //    }
        //}
    }
}
