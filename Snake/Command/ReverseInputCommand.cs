using Microsoft.Xna.Framework;

namespace Snake.Command;

public class ReverseInputCommand(InputHandler inputHandler) : ICommand
{
    public void Execute()
    {
        inputHandler.ReverseInput();
    }

    public void Undo()
    {

    }
}