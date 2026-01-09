using Gum.Forms.Controls;
using MonoGameGum;
using System;

namespace TicTacToe;

public class TitleScene
{
    private Panel _titleScreenButtonsPanel;
    private Button _playButton;
    private Button _exitButton;

    private void CreateTitlePanel()
    {
        _titleScreenButtonsPanel = new Panel();
        _titleScreenButtonsPanel.Dock(Gum.Wireframe.Dock.Fill);
        _titleScreenButtonsPanel.AddToRoot();

        _playButton = new Button();
        _playButton.Anchor(Gum.Wireframe.Anchor.Left);
        _playButton.X = 50;
        _playButton.Y = -25;
        _playButton.Width = 50;
        _playButton.Height = 25;
        _playButton.Text = "Start";
        _playButton.Click += HandlePlayClicked;
        _titleScreenButtonsPanel.AddChild(_playButton);

        _titleScreenButtonsPanel = new Panel();
        _titleScreenButtonsPanel.Dock(Gum.Wireframe.Dock.Fill);
        _titleScreenButtonsPanel.AddToRoot();

        _exitButton = new Button();
        _exitButton.Anchor(Gum.Wireframe.Anchor.Right);
        _exitButton.X = -50;
        _exitButton.Y = -25;
        _exitButton.Width = 50;
        _exitButton.Height = 25;
        _exitButton.Text = "Start";
        _exitButton.Click += HandleExitClicked;
        _titleScreenButtonsPanel.AddChild(_exitButton);

        _playButton.IsFocused = true;
    }

    private void HandlePlayClicked(object sender, EventArgs e)
    {
        
    }

    private void HandleExitClicked(object sender, EventArgs e)
    {

    }
}
