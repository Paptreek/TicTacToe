using MonoGameLibrary;
using TicTacToe.Scenes;
using MonoGameGum;
using Gum.Forms;
using Gum.Forms.Controls;

namespace TicTacToe
{
    public class TicTacToeGame : Core
    {
        public TicTacToeGame() : base("Swords and Shields", 400, 400, false)
        {

        }

        protected override void Initialize()
        {
            base.Initialize();
            InitializeGum();
            ChangeScene(new TitleScene());
        }

        private void InitializeGum()
        {
            GumService.Default.Initialize(this, DefaultVisualsVersion.V3);
            GumService.Default.ContentLoader.XnaContentManager = Core.Content;
            FrameworkElement.KeyboardsForUiControl.Add(GumService.Default.Keyboard);
        }
    }
}
