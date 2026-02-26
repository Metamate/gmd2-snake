using GMDCore;
using GMDCore.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Snake;

public class Game1 : Core
{
    public const int VirtualWidth = 512;
    public const int VirtualHeight = 288;
    private InputHandler _inputHandler;
    private Snake _snake;
    private Tilemap _tilemap;
    private Rectangle _roomBounds;

    public Game1() : base("Snake", 1280, 720, VirtualWidth, VirtualHeight)
    {
    }

    protected override void Initialize()
    {
        base.Initialize();

        Rectangle screenBounds = GraphicsDevice.PresentationParameters.Bounds;

        _roomBounds = new Rectangle(
             (int)_tilemap.TileWidth,
             (int)_tilemap.TileHeight,
             screenBounds.Width - (int)_tilemap.TileWidth * 2,
             screenBounds.Height - (int)_tilemap.TileHeight * 2
         );

        // Initial snake position will be the center tile of the tile map.
        int centerRow = _tilemap.Rows / 2;
        int centerColumn = _tilemap.Columns / 2;
        _snake.Position = new Vector2(centerColumn * _tilemap.TileWidth, centerRow * _tilemap.TileHeight);
    }

    protected override void LoadContent()
    {
        TextureAtlas atlas = TextureAtlas.FromFile(Content, "images/atlas-definition.xml");
        AnimatedSprite snake = atlas.CreateAnimatedSprite("snake-animation");
        _snake = new(snake);
        _inputHandler = new(_snake);

        _tilemap = Tilemap.FromFile(Content, "images/tilemap-definition.xml");
    }

    protected override void Update(GameTime gameTime)
    {
        _inputHandler.HandleInput();
        _snake.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        SpriteBatch.Begin(transformMatrix: ScreenScaleMatrix);
        _tilemap.Draw(SpriteBatch);
        _snake.Draw(SpriteBatch);
        SpriteBatch.End();

        base.Draw(gameTime);
    }
}
