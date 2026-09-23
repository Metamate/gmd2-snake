namespace Snake8.Command;

// A command is a reified method call: an action wrapped in an object.
// Because the object remembers what it did, it can also undo it.
public interface ICommand
{
    void Execute();
    void Undo();
}
