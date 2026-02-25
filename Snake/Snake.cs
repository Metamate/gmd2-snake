using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Snake;

public class Snake
{
    public Vector2 Position { get; set; }

    public Snake()
    {
        Position = new Vector2(100, 100);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Art.Pixel, new Rectangle((int)Position.X, (int)Position.Y, 16, 16), Color.White);
    }

    public void Move(Vector2 direction)
    {
        Position += direction * 16;
    }
}