using GMDCore;
using GMDCore.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Snake;

public class Game1 : Core
{
    public const int VirtualWidth = 320;
    public const int VirtualHeight = 180;
    public static Rectangle RoomBounds { get; private set; }
    private InputHandler _inputHandler;
    private Snake _snake;
    private Bat _bat;
    private Tilemap _tilemap;

    public Game1() : base("Snake", 1280, 720, VirtualWidth, VirtualHeight)
    {
    }

    protected override void Initialize()
    {
        base.Initialize();

        RoomBounds = new Rectangle(
             (int)_tilemap.TileWidth,
             (int)_tilemap.TileHeight,
             VirtualWidth - (int)_tilemap.TileWidth * 2,
             VirtualHeight - (int)_tilemap.TileHeight * 2
         );

        // Initial snake position will be the center tile of the tile map.
        int centerRow = _tilemap.Rows / 2;
        int centerColumn = _tilemap.Columns / 2;
        _snake.Position = new Vector2(centerColumn * _tilemap.TileWidth, centerRow * _tilemap.TileHeight);
        _bat.Position = _snake.Position + new Vector2(40, 40);
    }

    protected override void LoadContent()
    {
        TextureAtlas atlas = TextureAtlas.FromFile(Content, "images/atlas-definition.xml");
        AnimatedSprite snake = atlas.CreateAnimatedSprite("snake-animation");
        AnimatedSprite bat = atlas.CreateAnimatedSprite("bat-animation");
        _snake = new(snake);
        _bat = new(bat);
        _inputHandler = new(_snake);

        _tilemap = Tilemap.FromFile(Content, "images/tilemap-definition.xml");
    }

    protected override void Update(GameTime gameTime)
    {
        _inputHandler.HandleInput();
        _snake.Update(gameTime);
        _bat.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        SpriteBatch.Begin(transformMatrix: ScreenScaleMatrix, samplerState: SamplerState.PointClamp);
        _tilemap.Draw(SpriteBatch);
        _snake.Draw(SpriteBatch);
        _bat.Draw(SpriteBatch);
        SpriteBatch.End();

        base.Draw(gameTime);
    }
}
