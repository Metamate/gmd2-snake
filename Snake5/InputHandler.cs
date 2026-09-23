using GMDCore;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Snake5.Command;

namespace Snake5;

// Maps buttons to commands. The snake no longer knows anything about the keyboard,
// and changing what a button does is just a matter of assigning a different command.
public class InputHandler
{
    public ICommand ButtonW { get; set; }
    public ICommand ButtonA { get; set; }
    public ICommand ButtonS { get; set; }
    public ICommand ButtonD { get; set; }
    public ICommand ButtonR { get; set; }

    public InputHandler(Snake snake)
    {
        ButtonW = new MoveCommand(snake, -Vector2.UnitY);
        ButtonA = new MoveCommand(snake, -Vector2.UnitX);
        ButtonS = new MoveCommand(snake, Vector2.UnitY);
        ButtonD = new MoveCommand(snake, Vector2.UnitX);
        ButtonR = new ReverseInputCommand(this);
    }

    public void HandleInput()
    {
        if (Core.Input.Keyboard.WasKeyJustPressed(Keys.W))
        {
            ButtonW.Execute();
        }
        else if (Core.Input.Keyboard.WasKeyJustPressed(Keys.A))
        {
            ButtonA.Execute();
        }
        else if (Core.Input.Keyboard.WasKeyJustPressed(Keys.S))
        {
            ButtonS.Execute();
        }
        else if (Core.Input.Keyboard.WasKeyJustPressed(Keys.D))
        {
            ButtonD.Execute();
        }
        else if (Core.Input.Keyboard.WasKeyJustPressed(Keys.R))
        {
            ButtonR.Execute();
        }
    }

    // Up becomes down, left becomes right, by swapping the commands bound to the buttons.
    public void ReverseInput()
    {
        (ButtonW, ButtonS) = (ButtonS, ButtonW);
        (ButtonA, ButtonD) = (ButtonD, ButtonA);
    }
}
