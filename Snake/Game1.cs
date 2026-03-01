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

        _roomBounds = new Rectangle(
             (int)_tilemap.TileWidth,
             (int)_tilemap.TileHeight,
             VirtualWidth - (int)_tilemap.TileWidth * 2,
             VirtualHeight - (int)_tilemap.TileHeight * 2
         );

        _bat.RandomizeVelocity();
        PositionBatAwayFromSnake();

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
        // First perform a collision check to see if the slime is colliding with
        // the bat, which means the slime eats the bat.
        if (_snake.Bounds.Intersects(_bat.Bounds))
        {
            PositionBatAwayFromSnake();
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

    private void PositionBatAwayFromSnake()
    {
        // Calculate the position that is in the center of the bounds
        // of the room.
        float roomCenterX = _roomBounds.X + _roomBounds.Width * 0.5f;
        float roomCenterY = _roomBounds.Y + _roomBounds.Height * 0.5f;
        Vector2 roomCenter = new(roomCenterX, roomCenterY);

        Circle slimeBounds = _snake.Bounds;
        Vector2 slimeCenter = new(slimeBounds.X, slimeBounds.Y);

        // Calculate the distance vector from the center of the room to the
        // center of the slime.
        Vector2 centerToSlime = slimeCenter - roomCenter;

        // Calculate the amount of padding we will add to the new position of
        // the bat to ensure it is not sticking to walls
        int padding = _bat.Bounds.Radius * 2;

        // Calculate the new position of the bat by finding which component of
        // the center to slime vector (X or Y) is larger and in which direction.
        Vector2 newBatPosition = Vector2.Zero;
        if (Math.Abs(centerToSlime.X) > Math.Abs(centerToSlime.Y))
        {
            // The slime is closer to either the left or right wall, so the Y
            // position will be a random position between the top and bottom
            // walls.
            newBatPosition.Y = Random.Shared.Next(
                _roomBounds.Top + padding,
                _roomBounds.Bottom - padding
            );

            if (centerToSlime.X > 0)
            {
                // The slime is closer to the right side wall, so place the
                // bat on the left side wall.
                newBatPosition.X = _roomBounds.Left + padding;
            }
            else
            {
                // The slime is closer ot the left side wall, so place the
                // bat on the right side wall.
                newBatPosition.X = _roomBounds.Right - padding * 2;
            }
        }
        else
        {
            // The slime is closer to either the top or bottom wall, so the X
            // position will be a random position between the left and right
            // walls.
            newBatPosition.X = Random.Shared.Next(
                _roomBounds.Left + padding,
                _roomBounds.Right - padding
            );

            if (centerToSlime.Y > 0)
            {
                // The slime is closer to the top wall, so place the bat on the
                // bottom wall.
                newBatPosition.Y = _roomBounds.Top + padding;
            }
            else
            {
                // The slime is closer to the bottom wall, so place the bat on
                // the top wall.
                newBatPosition.Y = _roomBounds.Bottom - padding * 2;
            }
        }

        _bat.Position = newBatPosition;
    }
}
