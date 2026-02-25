using GMDCore;
using Microsoft.Xna.Framework.Input;

namespace Snake;

public static class GameController
{
    public static bool MoveLeft() => Core.Input.Keyboard.WasKeyJustPressed(Keys.A);
    public static bool MoveRight() => Core.Input.Keyboard.WasKeyJustPressed(Keys.D);
    public static bool MoveUp() => Core.Input.Keyboard.WasKeyJustPressed(Keys.W);
    public static bool MoveDown() => Core.Input.Keyboard.WasKeyJustPressed(Keys.S);
}