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

    private Tile _tile;
    private int _tilesPlaced;

    private Panel _gameOverPanel;

    public override void Initialize()
    {
        base.Initialize();

        Core.ExitOnEscape = true;

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

        if (Core.Input.Keyboard.WasKeyJustPressed(Keys.M))
        {
            Core.Audio.ToggleMute();
        }

        if (_gameOverPanel.IsVisible)
        {
            return;
        }

        GetPlayerChoice();

        if (HasWon(_gameBoard, Tile.Sword))
        {
            PrintWinner("Swords Win");
        }
        else if (HasWon(_gameBoard, Tile.Shield))
        {
            PrintWinner("Shields Win");
        }
        else if (_tilesPlaced == 9)
        {
            PrintWinner("Draw!");
        }
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
        _mouseState = Mouse.GetState();

        Rectangle clickArea = new(
                (int)_mouseState.Position.X - 4,
                (int)_mouseState.Position.Y - 4,
                (int)_swordTokenSourceRect.Width - 8,
                (int)_swordTokenSourceRect.Height - 8
        );

        if (Core.Input.Mouse.WasButtonJustPressed(MouseButton.Left))
        {
            if (clickArea.Intersects(_gameBoard.GetBounds(BoardLocation.TopLeft)) && _gameBoard.GetTile(0, 0) == Tile.Empty)
            {
                _gameBoard.SetTile(0, 0, SetPlayerToken(BoardLocation.TopLeft));
            }

            if (clickArea.Intersects(_gameBoard.GetBounds(BoardLocation.TopMiddle)) && _gameBoard.GetTile(0, 1) == Tile.Empty)
            {
                _gameBoard.SetTile(0, 1, SetPlayerToken(BoardLocation.TopMiddle));
            }

            if (clickArea.Intersects(_gameBoard.GetBounds(BoardLocation.TopRight)) && _gameBoard.GetTile(0, 2) == Tile.Empty)
            {
                _gameBoard.SetTile(0, 2, SetPlayerToken(BoardLocation.TopRight));
            }

            if (clickArea.Intersects(_gameBoard.GetBounds(BoardLocation.Left)) && _gameBoard.GetTile(1, 0) == Tile.Empty)
            {
                _gameBoard.SetTile(1, 0, SetPlayerToken(BoardLocation.Left));
            }

            if (clickArea.Intersects(_gameBoard.GetBounds(BoardLocation.Middle)) && _gameBoard.GetTile(1, 1) == Tile.Empty)
            {
                _gameBoard.SetTile(1, 1, SetPlayerToken(BoardLocation.Middle));
            }

            if (clickArea.Intersects(_gameBoard.GetBounds(BoardLocation.Right)) && _gameBoard.GetTile(1, 2) == Tile.Empty)
            {
                _gameBoard.SetTile(1, 2, SetPlayerToken(BoardLocation.Right));
            }

            if (clickArea.Intersects(_gameBoard.GetBounds(BoardLocation.BottomLeft)) && _gameBoard.GetTile(2, 0) == Tile.Empty)
            {
                _gameBoard.SetTile(2, 0, SetPlayerToken(BoardLocation.BottomLeft));
            }

            if (clickArea.Intersects(_gameBoard.GetBounds(BoardLocation.BottomMiddle)) && _gameBoard.GetTile(2, 1) == Tile.Empty)
            {
                _gameBoard.SetTile(2, 1, SetPlayerToken(BoardLocation.BottomMiddle));
            }

            if (clickArea.Intersects(_gameBoard.GetBounds(BoardLocation.BottomRight)) && _gameBoard.GetTile(2, 2) == Tile.Empty)
            {
                _gameBoard.SetTile(2, 2, SetPlayerToken(BoardLocation.BottomRight));
            }
        }
    }

    private Tile SetPlayerToken(BoardLocation boardLocation)
    {
        if (_isSwordsTurn)
        {
            _swordSound.Play();
            _swordTokens.Add(new Token(_textureAtlas, _gameBoard.GetLocation(boardLocation)));
            _isSwordsTurn = false;
            _tile = Tile.Sword;
            _tilesPlaced++;
            return Tile.Sword;
        }
        else
        {
            _shieldSound.Play();
            _shieldTokens.Add(new Token(_textureAtlas, _gameBoard.GetLocation(boardLocation)));
            _isSwordsTurn = true;
            _tile = Tile.Shield;
            _tilesPlaced++;
            return Tile.Shield;
        }
    }

    public bool HasWon(GameBoard gameBoard, Tile tileStatus)
    {
        if (gameBoard.GetTile(0, 0) == tileStatus && gameBoard.GetTile(0, 1) == tileStatus && gameBoard.GetTile(0, 2) == tileStatus) return true;
        if (gameBoard.GetTile(1, 0) == tileStatus && gameBoard.GetTile(1, 1) == tileStatus && gameBoard.GetTile(1, 2) == tileStatus) return true;
        if (gameBoard.GetTile(2, 0) == tileStatus && gameBoard.GetTile(2, 1) == tileStatus && gameBoard.GetTile(2, 2) == tileStatus) return true;

        if (gameBoard.GetTile(0, 0) == tileStatus && gameBoard.GetTile(1, 0) == tileStatus && gameBoard.GetTile(2, 0) == tileStatus) return true;
        if (gameBoard.GetTile(0, 1) == tileStatus && gameBoard.GetTile(1, 1) == tileStatus && gameBoard.GetTile(2, 1) == tileStatus) return true;
        if (gameBoard.GetTile(0, 2) == tileStatus && gameBoard.GetTile(1, 2) == tileStatus && gameBoard.GetTile(2, 2) == tileStatus) return true;

        if (gameBoard.GetTile(0, 0) == tileStatus && gameBoard.GetTile(1, 1) == tileStatus && gameBoard.GetTile(2, 2) == tileStatus) return true;
        if (gameBoard.GetTile(0, 2) == tileStatus && gameBoard.GetTile(1, 1) == tileStatus && gameBoard.GetTile(2, 0) == tileStatus) return true;

        return false;
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
        Core.ChangeScene(new TitleScene());
    }

    private void InitializeUI()
    {
        GumService.Default.Root.Children.Clear();
        CreateGameOverPanel();
    }

    // these are just for visualizing collision rectangles on screen for proper placement
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
