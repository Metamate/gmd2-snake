namespace Snake.Command;

public interface ICommand
{
    void Execute();
    void Undo();
}