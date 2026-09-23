using GMDCore;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Snake6.Command;

namespace Snake6;

// To support undo, every action creates a new command object that remembers what it did,
// and all commands go through the invoker, which keeps the history.
public class InputHandler(Snake snake)
{
    private readonly CommandInvoker _invoker = new();
    private Vector2 _up = -Vector2.UnitY;
    private Vector2 _down = Vector2.UnitY;
    private Vector2 _left = -Vector2.UnitX;
    private Vector2 _right = Vector2.UnitX;

    public void HandleInput()
    {
        if (Core.Input.Keyboard.WasKeyJustPressed(Keys.W))
        {
            Move(_up);
        }
        else if (Core.Input.Keyboard.WasKeyJustPressed(Keys.A))
        {
            Move(_left);
        }
        else if (Core.Input.Keyboard.WasKeyJustPressed(Keys.S))
        {
            Move(_down);
        }
        else if (Core.Input.Keyboard.WasKeyJustPressed(Keys.D))
        {
            Move(_right);
        }
        else if (Core.Input.Keyboard.WasKeyJustPressed(Keys.R))
        {
            _invoker.ExecuteCommand(new ReverseInputCommand(this));
        }
        else if (Core.Input.Keyboard.WasKeyJustPressed(Keys.Q))
        {
            _invoker.Undo();
        }
        else if (Core.Input.Keyboard.WasKeyJustPressed(Keys.E))
        {
            _invoker.Redo();
        }
    }

    // Up becomes down, left becomes right.
    public void ReverseInput()
    {
        (_up, _down) = (_down, _up);
        (_left, _right) = (_right, _left);
    }

    private void Move(Vector2 direction)
    {
        // Only valid moves are executed. Otherwise, undoing a blocked move
        // would move the snake backwards even though it never moved forwards.
        if (snake.IsValidMove(direction))
        {
            _invoker.ExecuteCommand(new MoveCommand(snake, direction));
        }
    }
}
