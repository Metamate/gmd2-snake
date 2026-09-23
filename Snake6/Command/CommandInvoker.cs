using System.Collections.Generic;

namespace Snake6.Command;

// Executes commands and keeps a history of them, so they can be undone and redone.
public class CommandInvoker
{
    private readonly Stack<ICommand> _undoStack = new();
    private readonly Stack<ICommand> _redoStack = new();

    public void ExecuteCommand(ICommand command)
    {
        command.Execute();
        _undoStack.Push(command);

        // A new command starts a new history, so the old redo history is no longer valid.
        _redoStack.Clear();
    }

    public void Undo()
    {
        if (_undoStack.Count > 0)
        {
            ICommand command = _undoStack.Pop();
            command.Undo();
            _redoStack.Push(command);
        }
    }

    public void Redo()
    {
        if (_redoStack.Count > 0)
        {
            ICommand command = _redoStack.Pop();
            command.Execute();
            _undoStack.Push(command);
        }
    }
}
