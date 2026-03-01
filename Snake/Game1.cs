using System;
using GMDCore;
using GMDCore.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Snake;

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

        // Initial snake position will be the center tile of the tile map.
        int centerRow = _tilemap.Rows / 2;
        int centerColumn = _tilemap.Columns / 2;
        _snake.Position = new Vector2(centerColumn * _tilemap.TileWidth, centerRow * _tilemap.TileHeight);
    }

    protected override void LoadContent()
    {
        TextureAtlas atlas = TextureAtlas.FromFile(Content, "images/atlas-definition.xml");
        AnimatedSprite snake = atlas.CreateAnimatedSprite("snake-animation");
        AnimatedSprite bat = atlas.CreateAnimatedSprite("bat-animation");
        _tilemap = Tilemap.FromFile(Content, "images/tilemap-definition.xml");

        _roomBounds = new Rectangle(
            (int)_tilemap.TileWidth,
            (int)_tilemap.TileHeight,
            VirtualWidth - (int)_tilemap.TileWidth * 2,
            VirtualHeight - (int)_tilemap.TileHeight * 2
        );

        _snake = new(snake, _roomBounds);
        _bat = new(bat);
        _inputHandler = new(_snake);
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
        // First perform a collision check to see if the snake is colliding with
        // the bat, which means the snake eats the bat.
        if (_snake.Bounds.Intersects(_bat.Bounds))
        {
            _bat.RandomizePosition();
            _bat.RandomizeVelocity();
        }

        // Next check if the snake is colliding with the wall by validating if
        // it is within the bounds of the room. If it is outside the room
        // bounds, then it collided with a wall which triggers a game over.
        if (_snake.Bounds.Top < _roomBounds.Top ||
           _snake.Bounds.Bottom > _roomBounds.Bottom ||
           _snake.Bounds.Left < _roomBounds.Left ||
           _snake.Bounds.Right > _roomBounds.Right)
        {
            //GameOver();
            return;
        }

        // Finally, check if the bat is colliding with a wall by validating if
        // it is within the bounds of the room. If it is outside the room
        // bounds, then it collided with a wall, and the bat should bounce
        // off of that wall.
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
