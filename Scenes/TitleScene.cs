using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameGum;
using Gum.Forms.Controls;
using MonoGameLibrary;
using MonoGameLibrary.Scenes;

namespace TicTacToe.Scenes;

public class TitleScene : Scene
{
    private const string TITLE_TEXT = "Swords & Shields";
    private const string PRESS_ENTER_TEXT = "Press Enter to Play";

    private SpriteFont _font;
    private SpriteFont _font5x;

    private Vector2 _titleTextPos;
    private Vector2 _titleTextOrigin;
    private Vector2 _pressEnterTextPos;
    private Vector2 _pressEnterTextOrigin;

    private SoundEffect _popSound;
    private Panel _titleScreenPanel;
    private Button _playButton;

    public override void Initialize()
    {
        base.Initialize();

        Core.ExitOnEscape = true;

        Vector2 size = _font5x.MeasureString(TITLE_TEXT);
        _titleTextPos = new Vector2(200, 165);
        _titleTextOrigin = size * 0.5f;

        size = _font.MeasureString(PRESS_ENTER_TEXT);
        _pressEnterTextPos = new Vector2(200, 215);
        _pressEnterTextOrigin = size * 0.5f;

        InitializeUI();
    }

    public override void LoadContent()
    {
        _font = Core.Content.Load<SpriteFont>("fonts/jacquard24");
        _font5x = Core.Content.Load<SpriteFont>("fonts/jacquard24_5x");

        _popSound = Core.Content.Load<SoundEffect>("audio/pop");
    }

    public override void Update(GameTime gameTime)
    {
        if (Core.Input.Keyboard.WasKeyJustPressed(Keys.Enter))
        {
            Core.ChangeScene(new GameScene());
        }

        GumService.Default.Update(gameTime);
    }

    public override void Draw(GameTime gameTime)
    {
        Core.GraphicsDevice.Clear(Color.BurlyWood);

        Core.SpriteBatch.Begin(samplerState: SamplerState.PointClamp);

        Color dropShadowColor = Color.Black * 0.5f;

        Core.SpriteBatch.DrawString(_font5x, TITLE_TEXT, _titleTextPos + new Vector2(2, 2), dropShadowColor, 0.0f, _titleTextOrigin, 1.0f, SpriteEffects.None, 1.0f);
        Core.SpriteBatch.DrawString(_font5x, TITLE_TEXT, _titleTextPos, Color.SaddleBrown, 0.0f, _titleTextOrigin, 1.0f, SpriteEffects.None, 1.0f);

        Core.SpriteBatch.DrawString(_font, PRESS_ENTER_TEXT, _pressEnterTextPos + new Vector2(1, 1), dropShadowColor, 0.0f, _pressEnterTextOrigin, 1.0f, SpriteEffects.None, 1.0f);
        Core.SpriteBatch.DrawString(_font, PRESS_ENTER_TEXT, _pressEnterTextPos, Color.White, 0.0f, _pressEnterTextOrigin, 1.0f, SpriteEffects.None, 1.0f);

        Core.SpriteBatch.End();

        GumService.Default.Draw();
    }

    //private void CreateTitlePanel()
    //{
    //    _titleScreenPanel = new Panel();
    //    _titleScreenPanel.Dock(Gum.Wireframe.Dock.Fill);
    //    _titleScreenPanel.AddToRoot();

    //    _playButton = new Button();
    //    _playButton.Anchor(Gum.Wireframe.Anchor.Center);
    //    _playButton.X = 0;
    //    _playButton.Y = 100;
    //    _playButton.Width = 100;
    //    _playButton.Height = 50;
    //    _playButton.Text = "Play";
    //    _playButton.Click += HandlePlayClicked;
    //    _titleScreenPanel.AddChild(_playButton);

    //    _playButton.IsFocused = true;
    //}

    //private void HandlePlayClicked(object sender, EventArgs e)
    //{
    //    Core.Audio.PlaySoundEffect(_popSound);
    //    Core.ChangeScene(new GameScene());
    //}

    private void InitializeUI()
    {
        GumService.Default.Root.Children.Clear();
        //CreateTitlePanel();
    }
}
