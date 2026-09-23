namespace Snake5.Command;

// A command is a reified method call: an action wrapped in an object.
public interface ICommand
{
    void Execute();
}
