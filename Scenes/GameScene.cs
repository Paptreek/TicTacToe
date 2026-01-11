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

namespace SwordsAndShields.Scenes;

public class GameScene : Scene
{
    private Texture2D _textureAtlas;
    private GameBoard _gameBoard;
    private MouseInfo _mouseInfo;
    private SoundEffect _swordSound;
    private SoundEffect _shieldSound;
    private SoundEffect _victorySound;

    private readonly List<Token> _swordTokens = [];
    private readonly List<Token> _shieldTokens = [];

    private MouseState _mouseState;

    private Rectangle _gameBoardSourceRect = new(0, 0, 48, 48);
    private Rectangle _swordTokenSourceRect = new(48, 0, 16, 16);
    private Rectangle _shieldTokenSourceRect = new(64, 0, 16, 16);
    private Rectangle _buttonBorderSourceRectangle = new Rectangle(48, 16, 32, 16);

    private bool _isSwordsTurn = true;

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

        _swordSound = Content.Load<SoundEffect>("audio/sword");
        _shieldSound = Content.Load<SoundEffect>("audio/shield");
        _victorySound = Content.Load<SoundEffect>("audio/victory");
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

        foreach (Token token in _swordTokens)
        {
            token.DrawOnBoard(Core.SpriteBatch, _swordTokenSourceRect, new Vector2(8, 8));
        }

        foreach (Token token in _shieldTokens)
        {
            token.DrawOnBoard(Core.SpriteBatch, _shieldTokenSourceRect, new Vector2(8, 8));
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
                (int)_swordTokenSourceRect.Width - 8,
                (int)_swordTokenSourceRect.Height - 8
        );

        if (_mouseInfo.WasButtonJustPressed(MouseButton.Left))
        {
            if (clickArea.Intersects(_gameBoard.GetBounds(BoardLocation.TopLeft)) && _topLeft == TopLeft.Empty)
            {
                if (_isSwordsTurn == true)
                {
                    PlaySwordTurn(BoardLocation.TopLeft);
                    _topLeft = TopLeft.PlayedBySwords;
                }
                else
                {
                    PlayShieldTurn(BoardLocation.TopLeft);
                    _topLeft = TopLeft.PlayedByShields;
                }
            }

            if (clickArea.Intersects(_gameBoard.GetBounds(BoardLocation.TopMiddle)) && _topMiddle == TopMiddle.Empty)
            {
                if (_isSwordsTurn == true)
                {
                    PlaySwordTurn(BoardLocation.TopMiddle);
                    _topMiddle = TopMiddle.PlayedBySwords;
                }
                else
                {
                    PlayShieldTurn(BoardLocation.TopMiddle);
                    _topMiddle = TopMiddle.PlayedByShields;
                }
            }

            if (clickArea.Intersects(_gameBoard.GetBounds(BoardLocation.TopRight)) && _topRight == TopRight.Empty)
            {
                if (_isSwordsTurn == true)
                {
                    PlaySwordTurn(BoardLocation.TopRight);
                    _topRight = TopRight.PlayedBySwords;
                }
                else
                {
                    PlayShieldTurn(BoardLocation.TopRight);
                    _topRight = TopRight.PlayedByShields;
                }
            }

            if (clickArea.Intersects(_gameBoard.GetBounds(BoardLocation.Left)) && _left == Left.Empty)
            {
                if (_isSwordsTurn == true)
                {
                    PlaySwordTurn(BoardLocation.Left);
                    _left = Left.PlayedBySwords;
                }
                else
                {
                    PlayShieldTurn(BoardLocation.Left);
                    _left = Left.PlayedByShields;
                }
            }

            if (clickArea.Intersects(_gameBoard.GetBounds(BoardLocation.Middle)) && _middle == Middle.Empty)
            {
                if (_isSwordsTurn == true)
                {
                    PlaySwordTurn(BoardLocation.Middle);
                    _middle = Middle.PlayedBySwords;
                }
                else
                {
                    PlayShieldTurn(BoardLocation.Middle);
                    _middle = Middle.PlayedByShields;
                }
            }

            if (clickArea.Intersects(_gameBoard.GetBounds(BoardLocation.Right)) && _right == Right.Empty)
            {
                if (_isSwordsTurn == true)
                {
                    PlaySwordTurn(BoardLocation.Right);
                    _right = Right.PlayedBySwords;
                }
                else
                {
                    PlayShieldTurn(BoardLocation.Right);
                    _right = Right.PlayedByShields;
                }
            }

            if (clickArea.Intersects(_gameBoard.GetBounds(BoardLocation.BottomLeft)) && _bottomLeft == BottomLeft.Empty)
            {
                if (_isSwordsTurn == true)
                {
                    PlaySwordTurn(BoardLocation.BottomLeft);
                    _bottomLeft = BottomLeft.PlayedBySwords;
                }
                else
                {
                    PlayShieldTurn(BoardLocation.BottomLeft);
                    _bottomLeft = BottomLeft.PlayedByShields;
                }
            }

            if (clickArea.Intersects(_gameBoard.GetBounds(BoardLocation.BottomMiddle)) && _bottomMiddle == BottomMiddle.Empty)
            {
                if (_isSwordsTurn == true)
                {
                    PlaySwordTurn(BoardLocation.BottomMiddle);
                    _bottomMiddle = BottomMiddle.PlayedBySwords;
                }
                else
                {
                    PlayShieldTurn(BoardLocation.BottomMiddle);
                    _bottomMiddle = BottomMiddle.PlayedByShields;
                }
            }

            if (clickArea.Intersects(_gameBoard.GetBounds(BoardLocation.BottomRight)) && _bottomRight == BottomRight.Empty)
            {
                if (_isSwordsTurn == true)
                {
                    PlaySwordTurn(BoardLocation.BottomRight);
                    _bottomRight = BottomRight.PlayedBySwords;
                }
                else
                {
                    PlayShieldTurn(BoardLocation.BottomRight);
                    _bottomRight = BottomRight.PlayedByShields;
                }
            }
        }
    }

    private void PlaySwordTurn(BoardLocation boardLocation)
    {
        _swordSound.Play();
        _swordTokens.Add(new Token(_textureAtlas, _gameBoard.GetLocation(boardLocation)));
        _isSwordsTurn = false;
    }

    private void PlayShieldTurn(BoardLocation boardLocation)
    {
        _shieldSound.Play();
        _shieldTokens.Add(new Token(_textureAtlas, _gameBoard.GetLocation(boardLocation)));
        _isSwordsTurn = true;
    }

    // LOL I hate this, but it was the first working solution that came to mind. "felt cute, might delete later"
    public void CheckForWin()
    {
        if (_topLeft == TopLeft.PlayedBySwords && _topMiddle == TopMiddle.PlayedBySwords && _topRight == TopRight.PlayedBySwords ||
            _left == Left.PlayedBySwords && _middle == Middle.PlayedBySwords && _right == Right.PlayedBySwords ||
            _bottomLeft == BottomLeft.PlayedBySwords && _bottomMiddle == BottomMiddle.PlayedBySwords && _bottomRight == BottomRight.PlayedBySwords ||
            _topLeft == TopLeft.PlayedBySwords && _left == Left.PlayedBySwords && _bottomLeft == BottomLeft.PlayedBySwords ||
            _topMiddle == TopMiddle.PlayedBySwords && _middle == Middle.PlayedBySwords && _bottomMiddle == BottomMiddle.PlayedBySwords ||
            _topRight == TopRight.PlayedBySwords && _right == Right.PlayedBySwords && _bottomRight == BottomRight.PlayedBySwords ||
            _topLeft == TopLeft.PlayedBySwords && _middle == Middle.PlayedBySwords && _bottomRight == BottomRight.PlayedBySwords ||
            _topRight == TopRight.PlayedBySwords && _middle == Middle.PlayedBySwords && _bottomLeft == BottomLeft.PlayedBySwords)
        {
            PrintWinner("Swords Win");
        }
        else if (_topLeft == TopLeft.PlayedByShields && _topMiddle == TopMiddle.PlayedByShields && _topRight == TopRight.PlayedByShields ||
            _left == Left.PlayedByShields && _middle == Middle.PlayedByShields && _right == Right.PlayedByShields ||
            _bottomLeft == BottomLeft.PlayedByShields && _bottomMiddle == BottomMiddle.PlayedByShields && _bottomRight == BottomRight.PlayedByShields ||
            _topLeft == TopLeft.PlayedByShields && _left == Left.PlayedByShields && _bottomLeft == BottomLeft.PlayedByShields ||
            _topMiddle == TopMiddle.PlayedByShields && _middle == Middle.PlayedByShields && _bottomMiddle == BottomMiddle.PlayedByShields ||
            _topRight == TopRight.PlayedByShields && _right == Right.PlayedByShields && _bottomRight == BottomRight.PlayedByShields ||
            _topLeft == TopLeft.PlayedByShields && _middle == Middle.PlayedByShields && _bottomRight == BottomRight.PlayedByShields ||
            _topRight == TopRight.PlayedByShields && _middle == Middle.PlayedByShields && _bottomLeft == BottomLeft.PlayedByShields)
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
        _victorySound.Play();

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
        _swordTokens.Clear();
        _shieldTokens.Clear();

        _topLeft = TopLeft.Empty;
        _topMiddle = TopMiddle.Empty;
        _topRight = TopRight.Empty;
        _left = Left.Empty;
        _middle = Middle.Empty;
        _right = Right.Empty;
        _bottomLeft = BottomLeft.Empty;
        _bottomMiddle = BottomMiddle.Empty;
        _bottomRight = BottomRight.Empty;

        _isSwordsTurn = true;

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

        foreach (Token token in _shieldTokens)
        {
            Core.SpriteBatch.Draw(
                texture,
                new Rectangle(
                    (int)token.TokenPosition.X - 4,
                    (int)token.TokenPosition.Y - 4,
                    (int)_shieldTokenSourceRect.Width - 8,
                    (int)_shieldTokenSourceRect.Height - 8),
                Color.White);
        }
    }
}
