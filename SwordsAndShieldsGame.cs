using Gum.Forms;
using Gum.Forms.Controls;
using Microsoft.Xna.Framework.Media;
using MonoGameGum;
using MonoGameLibrary;
using SwordsAndShields.Scenes;

namespace SwordsAndShields;

public class SwordsAndShieldsGame : Core
{
    private Song _backgroundMusic;

    public SwordsAndShieldsGame() : base("Swords and Shields", 400, 400, false)
    {

    }

    protected override void Initialize()
    {
        base.Initialize();
        Audio.PlaySong(_backgroundMusic);
        InitializeGum();
        ChangeScene(new TitleScene());
    }

    protected override void LoadContent()
    {
        _backgroundMusic = Content.Load<Song>("audio/song");
    }

    private void InitializeGum()
    {
        GumService.Default.Initialize(this, DefaultVisualsVersion.V3);
        GumService.Default.ContentLoader.XnaContentManager = Core.Content;
        FrameworkElement.KeyboardsForUiControl.Add(GumService.Default.Keyboard);
    }
}
