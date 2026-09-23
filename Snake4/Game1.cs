using GMDCore;
using GMDCore.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Snake4;

public class Game1 : Core
{
    public const int VirtualWidth = 320;
    public const int VirtualHeight = 180;
    private Snake _snake;
    private AnimatedSprite _bat;
    private Rectangle _roomBounds;

    public Game1() : base("Snake", 1280, 720, VirtualWidth, VirtualHeight)
    {
    }

    protected override void Initialize()
    {
        base.Initialize();

        // Start the snake in the center of the screen, aligned to the 20x20 grid.
        _snake.Position = new Vector2(160, 80);
    }

    protected override void LoadContent()
    {
        TextureAtlas atlas = TextureAtlas.FromFile(Content, "images/atlas-definition.xml");
        _bat = atlas.CreateAnimatedSprite("bat-animation");

        // For now, the room is the whole screen.
        _roomBounds = new Rectangle(0, 0, VirtualWidth, VirtualHeight);
        _snake = new Snake(atlas.CreateAnimatedSprite("snake-animation"), _roomBounds);
    }

    protected override void Update(GameTime gameTime)
    {
        _snake.Update(gameTime);
        _bat.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        SpriteBatch.Begin(transformMatrix: ScreenScaleMatrix, samplerState: SamplerState.PointClamp);
        _snake.Draw(SpriteBatch);
        _bat.Draw(SpriteBatch, new Vector2(40, 40));
        SpriteBatch.End();

        base.Draw(gameTime);
    }
}
