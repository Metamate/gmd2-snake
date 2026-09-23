using GMDCore;
using GMDCore.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Snake8;

public class Game1 : Core
{
    public const int VirtualWidth = 320;
    public const int VirtualHeight = 180;
    private InputHandler _inputHandler;
    private Snake _snake;
    private Bat _bat;
    private Tilemap _tilemap;
    private Rectangle _roomBounds;

    public Game1() : base("Snake", 1280, 720, VirtualWidth, VirtualHeight)
    {
    }

    protected override void Initialize()
    {
        base.Initialize();

        _bat.RandomizePosition();
        _bat.RandomizeVelocity();

        // Start the snake on the center tile of the tilemap.
        int centerRow = _tilemap.Rows / 2;
        int centerColumn = _tilemap.Columns / 2;
        _snake.Position = new Vector2(centerColumn * _tilemap.TileWidth, centerRow * _tilemap.TileHeight);
    }

    protected override void LoadContent()
    {
        TextureAtlas atlas = TextureAtlas.FromFile(Content, "images/atlas-definition.xml");
        _tilemap = Tilemap.FromFile(Content, "images/tilemap-definition.xml");

        // The room is the area inside the walls: the tilemap minus one tile on each side.
        _roomBounds = new Rectangle(
            (int)_tilemap.TileWidth,
            (int)_tilemap.TileHeight,
            VirtualWidth - (int)_tilemap.TileWidth * 2,
            VirtualHeight - (int)_tilemap.TileHeight * 2
        );

        _snake = new Snake(atlas.CreateAnimatedSprite("snake-animation"), _roomBounds);
        _bat = new Bat(atlas.CreateAnimatedSprite("bat-animation"), _roomBounds);
        _inputHandler = new InputHandler(_snake);
    }

    protected override void Update(GameTime gameTime)
    {
        _inputHandler.HandleInput();
        _snake.Update(gameTime);
        _bat.Update(gameTime);

        CollisionChecks();

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

    private void CollisionChecks()
    {
        // If the snake collides with the bat, the snake eats the bat and a new bat appears.
        if (_snake.Bounds.Intersects(_bat.Bounds))
        {
            _bat.RandomizePosition();
            _bat.RandomizeVelocity();
        }

        // If the bat leaves the room, it has hit a wall and bounces off it.
        // The normal points away from the wall, back into the room.
        if (_bat.Bounds.Top < _roomBounds.Top)
        {
            _bat.Bounce(Vector2.UnitY);
        }
        else if (_bat.Bounds.Bottom > _roomBounds.Bottom)
        {
            _bat.Bounce(-Vector2.UnitY);
        }

        if (_bat.Bounds.Left < _roomBounds.Left)
        {
            _bat.Bounce(Vector2.UnitX);
        }
        else if (_bat.Bounds.Right > _roomBounds.Right)
        {
            _bat.Bounce(-Vector2.UnitX);
        }
    }
}
