using GMDCore;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Snake.Command;

namespace Snake;

public class InputHandler
{
    public ICommand ButtonA { get; set; }
    public ICommand ButtonD { get; set; }
    public ICommand ButtonW { get; set; }
    public ICommand ButtonS { get; set; }
    public ICommand ButtonR { get; set; }

    public InputHandler(Snake snake)
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