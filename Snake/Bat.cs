using System;
using GMDCore;
using GMDCore.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Snake;

public class Bat
{
    private AnimatedSprite _sprite;
    private Vector2 _velocity;
    public Vector2 Position { get; set; }
    public Circle Bounds => new(
        (int)(Position.X + (Width * 0.5f)),
        (int)(Position.Y + (Height * 0.5f)),
        (int)(Width * 0.5f)
    );
    public float Width => _sprite.Width;
    public float Height => _sprite.Height;

    public Bat(AnimatedSprite sprite)
    {
        _sprite = sprite;
        _velocity = RandomBatVelocity();
    }

    public void Update(GameTime gameTime)
    {
        Vector2 normal = Vector2.Zero;
        Vector2 newPosition = Position + _velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

        // Use distance based checks to determine if the bat is within the
        // bounds of the game screen, and if it is outside that screen edge,
        // reflect it about the screen edge normal.
        if (Bounds.Left < Game1.RoomBounds.Left)
        {
            normal.X = Vector2.UnitX.X;
            newPosition = new Vector2(Game1.RoomBounds.Left, Position.Y);
        }
        else if (Bounds.Right > Game1.RoomBounds.Right)
        {
            normal.X = -Vector2.UnitX.X;
            newPosition = new Vector2(Game1.RoomBounds.Right - Width, Position.Y);
        }

        if (Bounds.Top < Game1.RoomBounds.Top)
        {
            normal.Y = Vector2.UnitY.Y;
            newPosition = new Vector2(Position.X, Game1.RoomBounds.Top);
        }
        else if (Bounds.Bottom > Game1.RoomBounds.Bottom)
        {
            normal.Y = -Vector2.UnitY.Y;
            newPosition = new Vector2(Position.X, Game1.RoomBounds.Bottom - Height);
        }

        // If the normal is anything but Vector2.Zero, this means the bat had
        // moved outside the screen edge so we should reflect it about the
        // normal.
        if (normal != Vector2.Zero)
        {
            normal.Normalize();
            _velocity = Vector2.Reflect(_velocity, normal);
        }

        Position = newPosition;
        _sprite.Update(gameTime);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        _sprite.Draw(spriteBatch, Position);
    }

    private Vector2 RandomBatVelocity()
    {
        // Generate a random angle.
        float angle = (float)(Random.Shared.NextDouble() * Math.PI * 2);

        // Convert angle to a direction vector.
        float x = (float)Math.Cos(angle);
        float y = (float)Math.Sin(angle);
        Vector2 direction = new(x, y);

        // Multiply the direction vector by the movement speed.
        return direction * 100f;
    }


}