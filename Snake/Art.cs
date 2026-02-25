using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Snake;

public static class Art
{
    public static Texture2D Pixel { get; private set; }

    

    public static void LoadContent(GraphicsDevice graphicsDevice)
    {
        Pixel = new Texture2D(graphicsDevice, 1, 1);
        Pixel.SetData([Color.White]);
    }
}