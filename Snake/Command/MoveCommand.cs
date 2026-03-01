using Microsoft.Xna.Framework;

namespace Snake.Command;

public class MoveCommand(Snake snake, Vector2 direction) : ICommand
{
    public void Execute()
    {
        snake.Move(direction);
    }

    public void Undo()
    {
        snake.Move(-direction);
    }
}