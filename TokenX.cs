using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TicTacToe;

public class TokenX
{
    private Texture2D _tokenTexture;
    private Color _tokenColor = Color.White;
    private float _tokenRotation = 0.0f;
    private float _tokenScale = 4.0f;
    private SpriteEffects _tokenEffects = SpriteEffects.None;
    private float _tokenLayerDepth = 0.0f;
    public Vector2 TokenPosition { get; private set; }

    public TokenX(Texture2D tokenTexture, Vector2 tokenPosition)
    {
        _tokenTexture = tokenTexture;
        TokenPosition = tokenPosition;
    }

    public void Draw(SpriteBatch spriteBatch, Rectangle tokenSource, Vector2 tokenOrigin)
    {
        spriteBatch.Draw(_tokenTexture, TokenPosition, tokenSource, _tokenColor, _tokenRotation, tokenOrigin, _tokenScale, _tokenEffects, _tokenLayerDepth);
    }
}