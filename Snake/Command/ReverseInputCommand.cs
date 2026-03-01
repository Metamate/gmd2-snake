using Microsoft.Xna.Framework;

namespace Snake.Command;

public class ReverseInputCommand(InputHandlerSimple inputHandler) : ICommand
{
    public void Execute()
    {
        inputHandler.ReverseInput();
    }

    public void Undo()
    {
        inputHandler.ReverseInput();
    }
}