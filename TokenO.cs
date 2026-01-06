using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TicTacToe;

public class TokenO
{
    private Texture2D _tokenTexture;
    private Vector2 _tokenPosition;
    private Rectangle _tokenSource;
    private Color _tokenColor = Color.White;
    private float _tokenRotation = 0.0f;
    private Vector2 _tokenOrigin = Vector2.One;
    private float _tokenScale = 4.0f;
    private SpriteEffects _tokenEffects = SpriteEffects.None;
    private float _tokenLayerDepth = 0.0f;

    public TokenO(Texture2D tokenTexture, Vector2 tokenPosition)
    {
        _tokenTexture = tokenTexture;
        _tokenPosition = tokenPosition;
        _tokenSource.X = 48;
        _tokenSource.Y = 0;
        _tokenSource.Width = 16;
        _tokenSource.Height = 16;
    }

    public void Draw(SpriteBatch spriteBatch, Rectangle tokenSource, Vector2 tokenOrigin)
    {
        spriteBatch.Draw(_tokenTexture, _tokenPosition, tokenSource, _tokenColor, _tokenRotation, tokenOrigin, _tokenScale, _tokenEffects, _tokenLayerDepth);
    }
}