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
    public Rectangle BoardSource { get; private set; }

    public bool IsTopLeftTaken { get; set; }
    public bool IsTopMiddleTaken { get; set; }
    public bool IsTopRightTaken { get; set; }

    public bool IsTopLeftPlayedByX { get; set; }
    public bool IsTopLeftPlayedByO { get; set; }
    public bool IsTopMiddlePlayedByX { get; set; }
    public bool IsTopMiddlePlayedByO { get; set; }
    public bool IsTopRightPlayedByX { get; set; }
    public bool IsTopRightPlayedByO { get; set; }


    public GameBoard(Texture2D boardTexture, Vector2 boardPosition, Rectangle boardSource)
    {
        _boardTexture = boardTexture;
        BoardSource = boardSource;
        BoardPosition = boardPosition;
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 boardOrigin)
    {
        spriteBatch.Draw(_boardTexture, BoardPosition, BoardSource, _boardColor, _boardRotation, boardOrigin, _boardScale, _boardEffects, _boardLayerDepth);
    }

    public Rectangle GetTopLeft()
    {
        Rectangle bounds = new Rectangle(8, 8, 120, 120);
        return bounds;
    }

    public Rectangle GetTopMiddle()
    {
        Rectangle bounds = new Rectangle(144, 8, 112, 120);
        return bounds;
    }

    public Rectangle GetTopRight()
    {
        Rectangle bounds = new Rectangle(272, 8, 120, 120);
        return bounds;
    }

    public Rectangle GetLeft()
    {
        Rectangle bounds = new Rectangle(8, 144, 120, 112);
        return bounds;
    }

    public Rectangle GetMiddle()
    {
        Rectangle bounds = new Rectangle(144, 144, 112, 112);
        return bounds;
    }

    public Rectangle GetRight()
    {
        Rectangle bounds = new Rectangle(272, 144, 120, 112);
        return bounds;
    }

    public Rectangle GetBottomLeft()
    {
        Rectangle bounds = new Rectangle(8, 272, 120, 120);
        return bounds;
    }

    public Rectangle GetBottomMiddle()
    {
        Rectangle bounds = new Rectangle(144, 272, 112, 120);
        return bounds;
    }

    public Rectangle GetBottomRight()
    {
        Rectangle bounds = new Rectangle(272, 272, 120, 120);
        return bounds;
    }

    public Vector2 GetTopLeftLocation()
    {
        return new Vector2(GetTopLeft().X + 60, GetTopLeft().Y + 64);
    }

    public Vector2 GetTopMiddleLocation()
    {
        return new Vector2(GetTopMiddle().X + 56, GetTopMiddle().Y + 64);
    }

    public Vector2 GetTopRightLocation()
    {
        return new Vector2(GetTopRight().X + 60, GetTopRight().Y + 64);
    }
}