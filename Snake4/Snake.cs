using GMDCore.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GMDCore;
using Microsoft.Xna.Framework.Input;

namespace Snake4;

public class Snake(AnimatedSprite sprite, Rectangle roomBounds)
{
    private readonly AnimatedSprite _sprite = sprite;
    private readonly Rectangle _roomBounds = roomBounds;
    public Vector2 Position { get; set; }

    public void Update(GameTime gameTime)
    {
        HandleInput();
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

    // Input is polled directly inside the gameplay class, with the keys hardcoded.
    // Rebinding keys, adding a gamepad or letting an AI control the snake all mean editing this class.
    private void HandleInput()
    {
        if (Core.Input.Keyboard.WasKeyJustPressed(Keys.W))
        {
            TryMove(-Vector2.UnitY);
        }
        else if (Core.Input.Keyboard.WasKeyJustPressed(Keys.S))
        {
            TryMove(Vector2.UnitY);
        }
        else if (Core.Input.Keyboard.WasKeyJustPressed(Keys.A))
        {
            TryMove(-Vector2.UnitX);
        }
        else if (Core.Input.Keyboard.WasKeyJustPressed(Keys.D))
        {
            TryMove(Vector2.UnitX);
        }
    }

    private void TryMove(Vector2 direction)
    {
        if (IsValidMove(direction))
        {
            Move(direction);
        }
    }
}
