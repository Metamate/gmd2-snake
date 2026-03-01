using System;
using GMDCore;
using GMDCore.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Snake;

public class Snake(AnimatedSprite animatedSprite, Rectangle roomBounds)
{
    private AnimatedSprite _sprite = animatedSprite;
    private Rectangle _roomBounds = roomBounds;
    private Vector2 _newPosition;
    public Vector2 Position { get; set; }
    public Circle Bounds => new(
        (int)(Position.X + (_sprite.Width * 0.5f)),
        (int)(Position.Y + (_sprite.Height * 0.5f)),
        (int)(_sprite.Width * 0.5f)
    );

    public void Update(GameTime gameTime)
    {
        _sprite.Update(gameTime);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        _sprite.Draw(spriteBatch, Position);
    }

    public void Move(Vector2 direction)
    {
        _newPosition = Position + direction * _sprite.Width;

        if (IsValidMove(_newPosition))
        {
            Position = _newPosition;
        }
    }

    private bool IsValidMove(Vector2 newPosition)
    {
        return _roomBounds.Contains(newPosition);
    }
}