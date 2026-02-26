using System;
using GMDCore.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Snake;

public class Snake(AnimatedSprite animatedSprite)
{
    private AnimatedSprite _sprite = animatedSprite;
    private Vector2 _newPosition;
    public Vector2 Position { get; set; }

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

    private static bool IsValidMove(Vector2 newPosition)
    {
        //Rectangle screenBounds = new(0, 0, Game1.VirtualWidth, Game1.VirtualHeight);
        //return screenBounds.Contains(newPosition);
        return true; // Placeholder for now, we will implement collision detection later
    }
}