using GMDCore;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Snake.Command;

namespace Snake;

// Example of a simpler input handler that directly executes commands without checking for valid moves first.
// Does not support undo/redo, but can be swapped in to compare with the more complex InputHandler.
public class InputHandlerSimple
{
    public ICommand ButtonA { get; set; }
    public ICommand ButtonD { get; set; }
    public ICommand ButtonW { get; set; }
    public ICommand ButtonS { get; set; }
    public ICommand ButtonR { get; set; }

    public InputHandlerSimple(Snake snake)
    {
        ButtonA = new MoveCommand(snake, -Vector2.UnitX);
        ButtonD = new MoveCommand(snake, Vector2.UnitX);
        ButtonW = new MoveCommand(snake, -Vector2.UnitY);
        ButtonS = new MoveCommand(snake, Vector2.UnitY);
        ButtonR = new ReverseInputCommand(this);
    }

    public void HandleInput()
    {
        if (Core.Input.Keyboard.WasKeyJustPressed(Keys.A))
        {
            ButtonA.Execute();
        }
        else if (Core.Input.Keyboard.WasKeyJustPressed(Keys.D))
        {
            ButtonD.Execute();
        }
        else if (Core.Input.Keyboard.WasKeyJustPressed(Keys.W))
        {
            ButtonW.Execute();
        }
        else if (Core.Input.Keyboard.WasKeyJustPressed(Keys.S))
        {
            ButtonS.Execute();
        }
        else if (Core.Input.Keyboard.WasKeyJustPressed(Keys.R))
        {
            ButtonR.Execute();
        }
    }

    public void ReverseInput()
    {
        (ButtonD, ButtonA) = (ButtonA, ButtonD);
        (ButtonW, ButtonS) = (ButtonS, ButtonW);
    }
}