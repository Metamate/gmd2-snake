using Microsoft.Xna.Framework;

namespace Snake5.Command;

public class MoveCommand(Snake snake, Vector2 direction) : ICommand
{
    public void Execute()
    {
        if (snake.IsValidMove(direction))
        {
            snake.Move(direction);
        }
    }
}
