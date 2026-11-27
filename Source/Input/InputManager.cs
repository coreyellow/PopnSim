using Microsoft.Xna.Framework;

namespace PopnSim.Input;

public class InputManager
{
    /// <summary>
    /// Gets the state information of keyboard input.
    /// </summary>
    public KeyboardInfo Keyboard { get; private set; }

    /// <summary>
    /// Gets the state information of mouse input.
    /// </summary>
    public MouseInfo Mouse { get; private set; }

    /// <summary>
    /// Creates a new InputManager.
    /// </summary>
    public InputManager()
    {
        Keyboard = new KeyboardInfo();
        Mouse = new MouseInfo();
    }

    /// <summary>
    /// Updates the state information of keyboard and mouse input.
    /// </summary>
    public void Update()
    {
        Keyboard.Update();
        Mouse.Update();
    }
}
