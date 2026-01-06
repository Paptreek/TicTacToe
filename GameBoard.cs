using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace TicTacToe;

public class GameBoard
{
    private Texture2D _boardTexture;
    private Color _boardColor = Color.White;
    private Rectangle _boardSource;
    private float _boardRotation = 0.0f;
    private float _boardScale = 8.0f;
    private SpriteEffects _boardEffects = SpriteEffects.None;
    private float _boardLayerDepth = 0.0f;
    public Vector2 BoardPosition { get; private set; }

    public GameBoard(Texture2D boardTexture, Vector2 boardPosition, Rectangle boardSource)
    {
        _boardTexture = boardTexture;
        _boardSource = boardSource;
        BoardPosition = boardPosition;
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 boardOrigin)
    {
        spriteBatch.Draw(_boardTexture, BoardPosition, _boardSource, _boardColor, _boardRotation, boardOrigin, _boardScale, _boardEffects, _boardLayerDepth);
    }

    public Rectangle GetTopLeft()
    {
        Vector2 location = BoardPosition;

        Rectangle bounds = new Rectangle(
            (int)location.X,
            (int)location.Y,
            (int)_boardSource.Width,
            (int)_boardSource.Height
        );

        //Debug.WriteLine(bounds);

        return bounds;
    }
}