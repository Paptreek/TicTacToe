using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
    }

    public override void LoadContent()
    {
        _font = Core.Content.Load<SpriteFont>("fonts/jacquard24");
        _font5x = Core.Content.Load<SpriteFont>("fonts/jacquard24_5x");
    }

    public override void Update(GameTime gameTime)
    {
        if (Core.Input.Keyboard.WasKeyJustPressed(Keys.Enter))
        {
            Core.ChangeScene(new GameScene());
        }
    }

    public override void Draw(GameTime gameTime)
    {
        Core.GraphicsDevice.Clear(Color.BurlyWood);

        Core.SpriteBatch.Begin(samplerState: SamplerState.PointClamp);

        Color dropShadowColor = Color.Black * 0.5f;

        Core.SpriteBatch.DrawString(_font5x, TITLE_TEXT, _titleTextPos + new Vector2(2, 2), dropShadowColor, 0.0f, _titleTextOrigin, 1.0f, SpriteEffects.None, 1.0f);
        Core.SpriteBatch.DrawString(_font5x, TITLE_TEXT, _titleTextPos, Color.DarkRed, 0.0f, _titleTextOrigin, 1.0f, SpriteEffects.None, 1.0f);

        Core.SpriteBatch.DrawString(_font, PRESS_ENTER_TEXT, _pressEnterTextPos + new Vector2(1, 1), dropShadowColor, 0.0f, _pressEnterTextOrigin, 1.0f, SpriteEffects.None, 1.0f);
        Core.SpriteBatch.DrawString(_font, PRESS_ENTER_TEXT, _pressEnterTextPos, Color.White, 0.0f, _pressEnterTextOrigin, 1.0f, SpriteEffects.None, 1.0f);

        Core.SpriteBatch.End();
    }
}
