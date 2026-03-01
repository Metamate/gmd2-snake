using System.Collections.Generic;

namespace Snake.Command;

public class CommandInvoker()
{
    private static Stack<ICommand> undoStack = new();
    private static Stack<ICommand> redoStack = new();

    public static void ExecuteCommand(ICommand command)
    {
        command.Execute();
        undoStack.Push(command);

        // Clear the redo stack when a new command is executed
        redoStack.Clear();
    }

    public static void UndoCommand()
    {
        if (undoStack.Count > 0)
        {
            var command = undoStack.Pop();
            command.Undo();
            redoStack.Push(command);
        }
    }

    public static void RedoCommand()
    {
        if (redoStack.Count > 0)
        {
            var command = redoStack.Pop();
            command.Execute();
            undoStack.Push(command);
        }
    }
}