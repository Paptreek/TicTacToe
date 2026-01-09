using MonoGameLibrary;
using TicTacToe.Scenes;

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

            ChangeScene(new TitleScene());
        }
    }
}
