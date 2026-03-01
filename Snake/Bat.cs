using System;
using GMDCore;
using GMDCore.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Snake;

public class Bat(AnimatedSprite animatedSprite)
{
    private const float Speed = 100f;
    private AnimatedSprite _sprite = animatedSprite;
    private Vector2 _velocity;
    public Vector2 Position { get; set; }
    public Circle Bounds => new(
        (int)(Position.X + (_sprite.Width * 0.5f)),
        (int)(Position.Y + (_sprite.Height * 0.5f)),
        (int)(_sprite.Width * 0.5f)
    );

    public void Update(GameTime gameTime)
    {
        Position += _velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
        _sprite.Update(gameTime);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        _sprite.Draw(spriteBatch, Position);
    }

    public void Bounce(Vector2 normal)
    {
        Vector2 newPosition = Position;

        // Adjust the position based on the normal to prevent sticking to walls.
        if (normal.X != 0)
        {
            // We are bouncing off a vertical wall (left/right).
            // Move slightly away from the wall in the direction of the normal.
            newPosition.X += normal.X * (_sprite.Width * 0.1f);
        }

        if (normal.Y != 0)
        {
            // We are bouncing off a horizontal wall (top/bottom).
            // Move slightly way from the wall in the direction of the normal.
            newPosition.Y += normal.Y * (_sprite.Height * 0.1f);
        }

        Position = newPosition;

        // Normalize before reflecting
        normal.Normalize();

        // Apply reflection based on the normal.
        _velocity = Vector2.Reflect(_velocity, normal);
    }

    public void RandomizeVelocity()
    {
        // Generate a random angle.
        float angle = (float)(Random.Shared.NextDouble() * Math.PI * 2);

        // Convert angle to a direction vector.
        float x = (float)Math.Cos(angle);
        float y = (float)Math.Sin(angle);
        Vector2 direction = new(x, y);

        // Multiply the direction vector by the movement speed.
        _velocity = direction * Speed;
    }

    public void RandomizePosition()
    {
        // Divide the width  and height of the screen into equal columns and
        // rows based on the width and height of the bat.
        int totalColumns = Game1.VirtualWidth / (int)_sprite.Width;
        int totalRows = Game1.VirtualHeight / (int)_sprite.Height;

        // Choose a random row and column based on the total number of each
        int column = Random.Shared.Next(0, totalColumns);
        int row = Random.Shared.Next(0, totalRows);

        // Change the bat position by setting the x and y values equal to
        // the column and row multiplied by the width and height.
        Position = new Vector2(column * _sprite.Width, row * _sprite.Height);
    }
}