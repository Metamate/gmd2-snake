using System;
using GMDCore.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Snake;

public class Snake(AnimatedSprite animatedSprite)
{
    private AnimatedSprite _sprite = animatedSprite;
    private Vector2 _position = new(100, 100);
    private Vector2 _newPosition;

    public void Update(GameTime gameTime)
    {
        _sprite.Update(gameTime);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        _sprite.Draw(spriteBatch, _position);
    }

    public void Move(Vector2 direction)
    {
        _newPosition = _position + direction * _sprite.Width;

        if (IsValidMove(_newPosition))
        {
            _position = _newPosition;
        }
    }

    private static bool IsValidMove(Vector2 newPosition)
    {
        Rectangle screenBounds = new(0, 0, Game1.VirtualWidth, Game1.VirtualHeight);
        return screenBounds.Contains(newPosition);
    }
}