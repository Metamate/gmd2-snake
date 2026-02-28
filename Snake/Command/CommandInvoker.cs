using System.Collections.Generic;

namespace Snake.Command;

public class CommandInvoker()
{
    private Stack<ICommand> undoStack = new();
    private Stack<ICommand> redoStack = new();

    public void ExecuteCommand(ICommand command)
    {
        command.Execute();
        undoStack.Push(command);
    }

    public void UndoCommand()
    {
        if (undoStack.Count > 0)
        {
            var command = undoStack.Pop();
            command.Undo();
        }
    }

    public void RedoCommand()
    {
        if (redoStack.Count > 0)
        {
            var command = redoStack.Pop();
            command.Execute();
            undoStack.Push(command);
        }
    }
}