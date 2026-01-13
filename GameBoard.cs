using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace SwordsAndShields;

public class GameBoard(Texture2D boardTexture, Vector2 boardPosition, Rectangle boardSource)
{
    private readonly Texture2D _boardTexture = boardTexture;
    private Color _boardColor = Color.White;
    private readonly float _boardRotation = 0.0f;
    private readonly float _boardScale = 8.0f;
    private readonly SpriteEffects _boardEffects = SpriteEffects.None;
    private readonly float _boardLayerDepth = 0.0f;
    private readonly Tile[,] _tiles = new Tile[3, 3];
    
    private readonly Rectangle[] _bounds = [
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

    private readonly Vector2 _boardPosition = boardPosition;
    private readonly Rectangle _boardSource = boardSource;

    public void Draw(SpriteBatch spriteBatch, Vector2 boardOrigin)
    {
        spriteBatch.Draw(_boardTexture, _boardPosition, _boardSource, _boardColor, _boardRotation, boardOrigin, _boardScale, _boardEffects, _boardLayerDepth);
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
            BoardLocation.BottomRight  => _bounds[8],
            _ => _bounds[0]
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
            BoardLocation.BottomMiddle => new Vector2(_bounds[7].X + 56, _bounds[6].Y + 62),
            BoardLocation.BottomRight  => new Vector2(_bounds[8].X + 60, _bounds[6].Y + 62),
            _ => Vector2.Zero
        };

        return location;
    }

    public Tile GetTile(int row, int column)
    {
        return _tiles[row, column];
    }

    public void SetTile(int row, int column, Tile tileStatus)
    {
        _tiles[row, column] = tileStatus;
    }
}

public enum BoardLocation { TopLeft, TopMiddle, TopRight, Left, Middle, Right, BottomLeft, BottomMiddle, BottomRight }
public enum Tile { Empty, Sword, Shield }