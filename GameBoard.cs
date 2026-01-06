using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TicTacToe;

public class GameBoard
{
    private Texture2D _boardTexture;
    private Color _boardColor = Color.White;
    private float _boardRotation = 0.0f;
    private float _boardScale = 8.0f;
    private SpriteEffects _boardEffects = SpriteEffects.None;
    private float _boardLayerDepth = 0.0f;
    public Vector2 BoardPosition { get; private set; }

    public GameBoard(Texture2D boardTexture, Vector2 boardPosition)
    {
        _boardTexture = boardTexture;
        BoardPosition = boardPosition;
    }

    public void Draw(SpriteBatch spriteBatch, Rectangle boardSource, Vector2 boardOrigin)
    {
        spriteBatch.Draw(_boardTexture, BoardPosition, boardSource, _boardColor, _boardRotation, boardOrigin, _boardScale, _boardEffects, _boardLayerDepth);
    }
}