using GMDCore.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Snake5;

public class Snake(AnimatedSprite sprite, Rectangle roomBounds)
{
    private readonly AnimatedSprite _sprite = sprite;
    private readonly Rectangle _roomBounds = roomBounds;
    public Vector2 Position { get; set; }

    public void Update(GameTime gameTime)
    {
        _sprite.Update(gameTime);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        _sprite.Draw(spriteBatch, Position);
    }

    // Moves the snake one tile in the given direction.
    public void Move(Vector2 direction)
    {
        Position += direction * _sprite.Width;
    }

    // A move is valid if the snake stays inside the room.
    public bool IsValidMove(Vector2 direction)
    {
        Vector2 newPosition = Position + direction * _sprite.Width;
        return _roomBounds.Contains(newPosition);
    }
}
