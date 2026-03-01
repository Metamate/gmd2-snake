using System;
using GMDCore;
using GMDCore.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Snake;

public class Snake(AnimatedSprite animatedSprite)
{
    private AnimatedSprite _sprite = animatedSprite;
    private Vector2 _newPosition;
    public Vector2 Position { get; set; }
    public Circle Bounds => new(
        (int)(Position.X + (Width * 0.5f)),
        (int)(Position.Y + (Height * 0.5f)),
        (int)(Width * 0.5f)
    );
    public float Width => _sprite.Width;
    public float Height => _sprite.Height;

    public void Update(GameTime gameTime)
    {
        if (!Game1.RoomBounds.Contains(Position))
        {
            // Use distance based checks to determine if the snake is within the
            // bounds of the game screen, and if it is outside that screen edge,
            // move it back inside.
            if (Bounds.Left < Game1.RoomBounds.Left)
            {
                Position = new Vector2(Game1.RoomBounds.Left, Position.Y);
            }
            else if (Bounds.Right > Game1.RoomBounds.Right)
            {
                Position = new Vector2(Game1.RoomBounds.Right - Width, Position.Y);
            }

            if (Bounds.Top < Game1.RoomBounds.Top)
            {
                Position = new Vector2(Position.X, Game1.RoomBounds.Top);
            }
            else if (Bounds.Bottom > Game1.RoomBounds.Bottom)
            {
                Position = new Vector2(Position.X, Game1.RoomBounds.Bottom - Height);
            }
        }
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