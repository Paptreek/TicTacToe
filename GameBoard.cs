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
    
    private Rectangle[] _bounds = [
        new Rectangle(8, 8, 120, 120),
        new Rectangle(144, 8, 112, 120),
        new Rectangle(272, 8, 120, 120),
        new Rectangle(8, 144, 120, 112),
        new Rectangle(144, 144, 112, 112),
        new Rectangle(272, 144, 120, 112),
        new Rectangle(8, 272, 120, 120),
        new Rectangle(144, 272, 112, 120),
        new Rectangle(272, 272, 120, 120)
    ];

    public Vector2 BoardPosition { get; private set; }
    public Rectangle BoardSource { get; private set; }
    public bool IsTopLeftTaken { get; set; }
    public bool IsTopMiddleTaken { get; set; }
    public bool IsTopRightTaken { get; set; }
    public bool IsLeftTaken { get; set; }
    public bool IsMiddleTaken { get; set; }
    public bool IsRightTaken { get; set; }
    public bool IsBottomLeftTaken { get; set; }
    public bool IsBottomMiddleTaken { get; set; }
    public bool IsBottomRightTaken { get; set; }

    public bool IsTopLeftPlayedByX { get; set; }
    public bool IsTopMiddlePlayedByX { get; set; }
    public bool IsTopRightPlayedByX { get; set; }
    public bool IsLeftPlayedByX { get; set; }
    public bool IsMiddlePlayedByX { get; set; }
    public bool IsRightPlayedByX { get; set; }
    public bool IsBottomLeftPlayedByX { get; set; }
    public bool IsBottomMiddlePlayedByX { get; set; }
    public bool IsBottomRightPlayedByX { get; set; }

    public bool IsTopLeftPlayedByO { get; set; }
    public bool IsTopMiddlePlayedByO { get; set; }
    public bool IsTopRightPlayedByO { get; set; }
    public bool IsLeftPlayedByO { get; set; }
    public bool IsMiddlePlayedByO { get; set; }
    public bool IsRightPlayedByO { get; set; }
    public bool IsBottomLeftPlayedByO { get; set; }
    public bool IsBottomMiddlePlayedByO { get; set; }
    public bool IsBottomRightPlayedByO { get; set; }


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

    public Rectangle GetBounds(BoardLocation boardLocation)
    {
        Rectangle location = boardLocation switch
        {
            BoardLocation.TopLeft   => _bounds[0],
            BoardLocation.TopMiddle => _bounds[1],
            BoardLocation.TopRight  => _bounds[2],
            BoardLocation.Left   => _bounds[3],
            BoardLocation.Middle => _bounds[4],
            BoardLocation.Right  =>  _bounds[5],
            BoardLocation.BottomLeft   => _bounds[6],
            BoardLocation.BottomMiddle => _bounds[7],
            BoardLocation.BottomRight  => _bounds[8]
        };

        return location;
    }

    public Vector2 GetLocation(BoardLocation boardLocation)
    {
        Vector2 location = boardLocation switch
        {
            BoardLocation.TopLeft   => new Vector2(_bounds[0].X + 60, _bounds[0].Y + 62),
            BoardLocation.TopMiddle => new Vector2(_bounds[1].X + 56, _bounds[1].Y + 62),
            BoardLocation.TopRight  => new Vector2(_bounds[2].X + 60, _bounds[2].Y + 62),
            BoardLocation.Left   => new Vector2(_bounds[3].X + 60, _bounds[3].Y + 56),
            BoardLocation.Middle => new Vector2(_bounds[4].X + 56, _bounds[4].Y + 56),
            BoardLocation.Right  => new Vector2(_bounds[5].X + 60, _bounds[5].Y + 56),
            BoardLocation.BottomLeft   => new Vector2(_bounds[6].X + 60, _bounds[6].Y + 62),
            BoardLocation.BottomMiddle => new Vector2(_bounds[6].X + 56, _bounds[6].Y + 62),
            BoardLocation.BottomRight  => new Vector2(_bounds[6].X + 60, _bounds[6].Y + 62),
        };

        return location;
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
        return new Vector2(GetTopLeft().X + 60, GetTopLeft().Y + 62);
    }

    public Vector2 GetTopMiddleLocation()
    {
        return new Vector2(GetTopMiddle().X + 56, GetTopMiddle().Y + 62);
    }

    public Vector2 GetTopRightLocation()
    {
        return new Vector2(GetTopRight().X + 60, GetTopRight().Y + 62);
    }

    public Vector2 GetLeftLocation()
    {
        return new Vector2(GetLeft().X + 60, GetLeft().Y + 56);
    }

    public Vector2 GetMiddleLocation()
    {
        return new Vector2(GetMiddle().X + 56, GetMiddle().Y + 56);
    }

    public Vector2 GetRightLocation()
    {
        return new Vector2(GetRight().X + 60, GetRight().Y + 56);
    }

    public Vector2 GetBottomLeftLocation()
    {
        return new Vector2(GetBottomLeft().X + 60, GetBottomLeft().Y + 62);
    }

    public Vector2 GetBottomMiddleLocation()
    {
        return new Vector2(GetBottomMiddle().X + 56, GetBottomMiddle().Y + 62);
    }

    public Vector2 GetBottomRightLocation()
    {
        return new Vector2(GetBottomRight().X + 60, GetBottomRight().Y + 62);
    }
}

public enum BoardLocation { TopLeft, TopMiddle, TopRight, Left, Middle, Right, BottomLeft, BottomMiddle, BottomRight }