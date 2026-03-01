using System.Numerics;
using GMDCore;
using Microsoft.Xna.Framework.Input;
using Snake.Command;

namespace Snake;

public class InputHandler(Snake snake)
{
    public void HandleInput()
    {
        if (Core.Input.Keyboard.WasKeyJustPressed(Keys.A))
        {
            RunInputCommand(-Vector2.UnitX);
        }
        else if (Core.Input.Keyboard.WasKeyJustPressed(Keys.D))
        {
            RunInputCommand(Vector2.UnitX);
        }
        else if (Core.Input.Keyboard.WasKeyJustPressed(Keys.W))
        {
            RunInputCommand(-Vector2.UnitY);
        }
        else if (Core.Input.Keyboard.WasKeyJustPressed(Keys.S))
        {
            RunInputCommand(Vector2.UnitY);
        }
        else if (Core.Input.Keyboard.WasKeyJustPressed(Keys.R))
        {
            //CommandInvoker.ExecuteCommand(new ReverseInputCommand(this));
        }
        else if (Core.Input.Keyboard.WasKeyJustPressed(Keys.Q))
        {
            CommandInvoker.UndoCommand();
        }
        else if (Core.Input.Keyboard.WasKeyJustPressed(Keys.E))
        {
            CommandInvoker.RedoCommand();
        }
    }

    private void RunInputCommand(Vector2 direction)
    {
        if (snake.IsValidMove(direction))
        {
            CommandInvoker.ExecuteCommand(new MoveCommand(snake, direction));
        }
    }

    // public void ReverseInput()
    // {
    //     (ButtonD, ButtonA) = (ButtonA, ButtonD);
    //     (ButtonW, ButtonS) = (ButtonS, ButtonW);
    // }


}