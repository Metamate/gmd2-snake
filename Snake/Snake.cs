using System;
using GMDCore.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Snake;

public class Snake(AnimatedSprite animatedSprite)
{
    private AnimatedSprite _sprite = animatedSprite;
    private Vector2 _position = new(100, 100);

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
        _position += direction * _sprite.Width;
    }
}