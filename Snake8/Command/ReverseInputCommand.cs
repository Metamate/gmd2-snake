namespace Snake8.Command;

public class ReverseInputCommand(InputHandler inputHandler) : ICommand
{
    public void Execute()
    {
        inputHandler.ReverseInput();
    }

    public void Undo()
    {
        // Reversing the input twice restores the original mapping.
        inputHandler.ReverseInput();
    }
}
