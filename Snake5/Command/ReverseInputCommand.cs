namespace Snake5.Command;

public class ReverseInputCommand(InputHandler inputHandler) : ICommand
{
    public void Execute()
    {
        inputHandler.ReverseInput();
    }
}
