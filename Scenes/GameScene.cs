using System;
using Gum.DataTypes;
using Gum.Forms.Controls;
using Gum.Forms.DefaultVisuals.V3;
using Gum.Managers;
using Gum.Wireframe;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameGum;
using MonoGameGum.GueDeriving;
using MonoGameLibrary;
using MonoGameLibrary.Input;
using MonoGameLibrary.Scenes;
using System.Collections.Generic;

namespace TicTacToe.Scenes;

public class GameScene : Scene
{
    private Texture2D _textureAtlas;
    private GameBoard _gameBoard;
    private MouseInfo _mouseInfo;
    private SoundEffect _xSound;
    private SoundEffect _oSound;

    private readonly List<Token> _xTokens = [];
    private readonly List<Token> _oTokens = [];

    private MouseState _mouseState;

    private Rectangle _gameBoardSourceRect = new(0, 0, 48, 48);
    private Rectangle _xTokenSourceRectangle = new(48, 0, 16, 16);
    private Rectangle _oTokenSourceRectangle = new(64, 0, 16, 16);
    private Rectangle _buttonBorderSourceRectangle = new Rectangle(48, 16, 32, 16);

    private bool _isTurnX = true;

    private TopLeft _topLeft;
    private TopMiddle _topMiddle;
    private TopRight _topRight;
    private Left _left;
    private Middle _middle;
    private Right _right;
    private BottomLeft _bottomLeft;
    private BottomMiddle _bottomMiddle;
    private BottomRight _bottomRight;

    private Panel _gameOverPanel;

    public override void Initialize()
    {
        base.Initialize();

        Core.ExitOnEscape = true;
        _mouseInfo = new MouseInfo();

        InitializeUI();
    }

    public override void LoadContent()
    {
        _textureAtlas = Content.Load<Texture2D>("images/textures");

        _gameBoard = new GameBoard(_textureAtlas, new Vector2(400, 400) * 0.5f, _gameBoardSourceRect);

        _xSound = Content.Load<SoundEffect>("audio/sword");
        _oSound = Content.Load<SoundEffect>("audio/shield");
    }

    public override void Update(GameTime gameTime)
    {
        GumService.Default.Update(gameTime);

        if (_gameOverPanel.IsVisible)
        {
            return;
        }

        _mouseState = Mouse.GetState();
        _mouseInfo.Update();

        GetPlayerChoice();

        CheckForWin();
    }

    public override void Draw(GameTime gameTime)
    {
        Core.GraphicsDevice.Clear(Color.BurlyWood);

        Core.SpriteBatch.Begin(samplerState: SamplerState.PointClamp);

        _gameBoard.Draw(Core.SpriteBatch, new Vector2(_gameBoardSourceRect.Width, _gameBoardSourceRect.Height) * 0.5f);

        //DrawBoardRectangleTest();
        //DrawTokenRectangleTest();

        foreach (Token token in _xTokens)
        {
            token.DrawOnBoard(Core.SpriteBatch, _xTokenSourceRectangle, new Vector2(8, 8));
        }

        foreach (Token token in _oTokens)
        {
            token.DrawOnBoard(Core.SpriteBatch, _oTokenSourceRectangle, new Vector2(8, 8));
        }

        Core.SpriteBatch.End();

        GumService.Default.Draw();

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
            if (clickArea.Intersects(_gameBoard.GetBounds(BoardLocation.TopLeft)) && _topLeft == TopLeft.Empty)
            {
                if (_isTurnX == true)
                {
                    PlayXTurn(BoardLocation.TopLeft);
                    _topLeft = TopLeft.PlayedByX;
                }
                else
                {
                    PlayOTurn(BoardLocation.TopLeft);
                    _topLeft = TopLeft.PlayedByO;
                }
            }

            if (clickArea.Intersects(_gameBoard.GetBounds(BoardLocation.TopMiddle)) && _topMiddle == TopMiddle.Empty)
            {
                if (_isTurnX == true)
                {
                    PlayXTurn(BoardLocation.TopMiddle);
                    _topMiddle = TopMiddle.PlayedByX;
                }
                else
                {
                    PlayOTurn(BoardLocation.TopMiddle);
                    _topMiddle = TopMiddle.PlayedByO;
                }
            }

            if (clickArea.Intersects(_gameBoard.GetBounds(BoardLocation.TopRight)) && _topRight == TopRight.Empty)
            {
                if (_isTurnX == true)
                {
                    PlayXTurn(BoardLocation.TopRight);
                    _topRight = TopRight.PlayedByX;
                }
                else
                {
                    PlayOTurn(BoardLocation.TopRight);
                    _topRight = TopRight.PlayedByO;
                }
            }

            if (clickArea.Intersects(_gameBoard.GetBounds(BoardLocation.Left)) && _left == Left.Empty)
            {
                if (_isTurnX == true)
                {
                    PlayXTurn(BoardLocation.Left);
                    _left = Left.PlayedByX;
                }
                else
                {
                    PlayOTurn(BoardLocation.Left);
                    _left = Left.PlayedByO;
                }
            }

            if (clickArea.Intersects(_gameBoard.GetBounds(BoardLocation.Middle)) && _middle == Middle.Empty)
            {
                if (_isTurnX == true)
                {
                    PlayXTurn(BoardLocation.Middle);
                    _middle = Middle.PlayedByX;
                }
                else
                {
                    PlayOTurn(BoardLocation.Middle);
                    _middle = Middle.PlayedByO;
                }
            }

            if (clickArea.Intersects(_gameBoard.GetBounds(BoardLocation.Right)) && _right == Right.Empty)
            {
                if (_isTurnX == true)
                {
                    PlayXTurn(BoardLocation.Right);
                    _right = Right.PlayedByX;
                }
                else
                {
                    PlayOTurn(BoardLocation.Right);
                    _right = Right.PlayedByO;
                }
            }

            if (clickArea.Intersects(_gameBoard.GetBounds(BoardLocation.BottomLeft)) && _bottomLeft == BottomLeft.Empty)
            {
                if (_isTurnX == true)
                {
                    PlayXTurn(BoardLocation.BottomLeft);
                    _bottomLeft = BottomLeft.PlayedByX;
                }
                else
                {
                    PlayOTurn(BoardLocation.BottomLeft);
                    _bottomLeft = BottomLeft.PlayedByO;
                }
            }

            if (clickArea.Intersects(_gameBoard.GetBounds(BoardLocation.BottomMiddle)) && _bottomMiddle == BottomMiddle.Empty)
            {
                if (_isTurnX == true)
                {
                    PlayXTurn(BoardLocation.BottomMiddle);
                    _bottomMiddle = BottomMiddle.PlayedByX;
                }
                else
                {
                    PlayOTurn(BoardLocation.BottomMiddle);
                    _bottomMiddle = BottomMiddle.PlayedByO;
                }
            }

            if (clickArea.Intersects(_gameBoard.GetBounds(BoardLocation.BottomRight)) && _bottomRight == BottomRight.Empty)
            {
                if (_isTurnX == true)
                {
                    PlayXTurn(BoardLocation.BottomRight);
                    _bottomRight = BottomRight.PlayedByX;
                }
                else
                {
                    PlayOTurn(BoardLocation.BottomRight);
                    _bottomRight = BottomRight.PlayedByO;
                }
            }
        }
    }

    private void PlayXTurn(BoardLocation boardLocation)
    {
        _xSound.Play();
        _xTokens.Add(new Token(_textureAtlas, _gameBoard.GetLocation(boardLocation)));
        _isTurnX = false;
    }

    private void PlayOTurn(BoardLocation boardLocation)
    {
        _oSound.Play();
        _oTokens.Add(new Token(_textureAtlas, _gameBoard.GetLocation(boardLocation)));
        _isTurnX = true;
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
            PrintWinner("Swords Win");
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
            PrintWinner("Shields Win");
        }
        else if (_topLeft != TopLeft.Empty && _topMiddle != TopMiddle.Empty && _topRight != TopRight.Empty &&
            _left != Left.Empty && _middle != Middle.Empty && _right != Right.Empty &&
            _bottomLeft != BottomLeft.Empty && _bottomMiddle != BottomMiddle.Empty && _bottomRight != BottomRight.Empty)
        {
            PrintWinner("Draw!");
        }

    }

    private void CreateGameOverPanel()
    {
        _gameOverPanel = new Panel();
        _gameOverPanel.Anchor(Anchor.Center);
        _gameOverPanel.Width = 400;
        _gameOverPanel.Height = 400;
        _gameOverPanel.IsVisible = false;
        _gameOverPanel.AddToRoot();
    }

    public void PrintWinner(string winner)
    {
        _gameOverPanel.IsVisible = true;

        var background = new ColoredRectangleRuntime();
        background.Dock(Dock.Fill);
        background.Color = Color.White * 0.85f;
        _gameOverPanel.AddChild(background);

        var button = new Button();
        button.Anchor(Anchor.Center);
        button.Text = "";
        background.AddChild(button);

        var buttonVisual = (ButtonVisual)button.Visual;
        buttonVisual.Y = 45;
        buttonVisual.Width = 185;
        buttonVisual.Height = 85;
        buttonVisual.HeightUnits = DimensionUnitType.Absolute;
        buttonVisual.WidthUnits = DimensionUnitType.Absolute;
        buttonVisual.BackgroundColor = Color.BurlyWood;
        buttonVisual.Click += HandlePlayAgainClicked;
        background.AddChild(buttonVisual);

        SpriteRuntime buttonBackground = new SpriteRuntime();
        buttonBackground.Texture = _textureAtlas;
        buttonBackground.TextureAddress = TextureAddress.Custom;
        buttonBackground.Width = 200;
        buttonBackground.Height = 100;
        buttonBackground.WidthUnits = DimensionUnitType.Absolute;
        buttonBackground.HeightUnits = DimensionUnitType.Absolute;
        buttonBackground.TextureHeight = _buttonBorderSourceRectangle.Height;
        buttonBackground.TextureLeft = _buttonBorderSourceRectangle.Left;
        buttonBackground.TextureTop = _buttonBorderSourceRectangle.Top;
        buttonBackground.TextureWidth = _buttonBorderSourceRectangle.Width;
        buttonBackground.Anchor(Anchor.Center);
        buttonVisual.AddChild(buttonBackground);

        TextRuntime winnerText = new TextRuntime();
        winnerText.Text = winner;
        winnerText.FontScale = 0.70f;
        winnerText.Color = Color.SaddleBrown;
        winnerText.UseCustomFont = true;
        winnerText.CustomFontFile = "fonts/jacquard_24.fnt";
        winnerText.Anchor(Anchor.Center);
        winnerText.Y = -35;
        background.AddChild(winnerText);

        TextRuntime playAgainText = new TextRuntime();
        playAgainText.Text = "play again";
        playAgainText.FontScale = 0.35f;
        playAgainText.Color = Color.White;
        playAgainText.UseCustomFont = true;
        playAgainText.CustomFontFile = "fonts/jacquard_24.fnt";
        playAgainText.Dock(Dock.Fill);
        buttonVisual.AddChild(playAgainText);
    }

    private void HandlePlayAgainClicked(object sender, EventArgs e)
    {
        StartNewGame();
    }

    private void StartNewGame()
    {
        _xTokens.Clear();
        _oTokens.Clear();

        _topLeft = TopLeft.Empty;
        _topMiddle = TopMiddle.Empty;
        _topRight = TopRight.Empty;
        _left = Left.Empty;
        _middle = Middle.Empty;
        _right = Right.Empty;
        _bottomLeft = BottomLeft.Empty;
        _bottomMiddle = BottomMiddle.Empty;
        _bottomRight = BottomRight.Empty;

        _isTurnX = true;

        _gameOverPanel.IsVisible = false;

        Core.ChangeScene(new TitleScene());
    }

    private void InitializeUI()
    {
        GumService.Default.Root.Children.Clear();
        CreateGameOverPanel();
    }

    public void DrawBoardRectangleTest()
    {
        Texture2D texture;

        texture = new Texture2D(Core.GraphicsDevice, 1, 1);
        texture.SetData([Color.Snow]);

        Core.SpriteBatch.Draw(
            texture,
            new Rectangle(8, 8, 120, 120),
            Color.White
        );
    }

    public void DrawTokenRectangleTest()
    {
        Texture2D texture;

        texture = new Texture2D(Core.GraphicsDevice, 1, 1);
        texture.SetData([Color.Snow]);

        foreach (Token token in _oTokens)
        {
            Core.SpriteBatch.Draw(
                texture,
                new Rectangle(
                    (int)token.TokenPosition.X - 4,
                    (int)token.TokenPosition.Y - 4,
                    (int)_oTokenSourceRectangle.Width - 8,
                    (int)_oTokenSourceRectangle.Height - 8),
                Color.White);
        }
    }
}
