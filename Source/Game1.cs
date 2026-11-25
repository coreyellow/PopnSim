using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PopnSim.Screens;
using System;

namespace PopnSim;

public class Game1 : Microsoft.Xna.Framework.Game
{
    internal static Game1 s_instance;

    /// <summary>
    /// Gets a reference to the Game1 instance.
    /// </summary>
    public static Game1 Instance => s_instance;

    /// <summary>
    /// The screen that is currently active and being updated/drawn.
    /// </summary>
    private static Screen s_activeScreen;

    /// <summary>
    /// The next screen to switch to on a screen transition.
    /// </summary>
    private static Screen s_nextScreen;

    /// <summary>
    /// Gets the graphics device manager to control the presentation of graphics.
    /// </summary>
    public static GraphicsDeviceManager Graphics { get; private set; }

    /// <summary>
    /// Gets the graphics device used to create graphical resources and perform primitive rendering.
    /// </summary>
    public static new GraphicsDevice GraphicsDevice { get; private set; }

    /// <summary>
    /// Gets the sprite batch used for all 2D rendering.
    /// </summary>
    public static SpriteBatch SpriteBatch { get; private set; }

    /// <summary>
    /// Gets the content manager used to load global assets.
    /// </summary>
    public static new ContentManager Content { get; private set; }

    /// <summary>
    /// Gets or Sets a value that indicates if the game should exit when the esc key on the keyboard is pressed.
    /// </summary>
    public static bool ExitOnEscape { get; set; }

    public Game1()
    {
        // Ensure that multiple cores are not created.
        if (s_instance != null)
        {
            throw new InvalidOperationException($"Only a single Game instance can be created");
        }

        // Store reference to engine for global member access.
        s_instance = this;

        Graphics = new GraphicsDeviceManager(this);

        // Window settings
        var displayMode = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode;
        Graphics.PreferredBackBufferWidth = displayMode.Width;
        Graphics.PreferredBackBufferHeight = displayMode.Height;
        Graphics.HardwareModeSwitch = false;
        Graphics.IsFullScreen = false;
        Window.IsBorderless = true;

        // Frame rate settings:
        // Disable VSync
        Graphics.SynchronizeWithVerticalRetrace = false;

        // Caps frames
        var FPSCap = 1000;
        IsFixedTimeStep = false;
        TargetElapsedTime = TimeSpan.FromSeconds(1.0 / FPSCap);

        Content = base.Content;
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        Graphics.ApplyChanges();

        ExitOnEscape = true;
    }

    protected override void Initialize()
    {
        base.Initialize();
        GraphicsDevice = base.GraphicsDevice;
        SpriteBatch = new SpriteBatch(GraphicsDevice);

        // Start with the title screen
        ChangeScreen(new TitleScreen());
    }

    protected override void Update(GameTime gameTime)
    {
#if !__IOS__
        // Exit the game if the Back button (GamePad) or Escape key (Keyboard) is pressed.
        // (iOS does not allow programmatically closing an app.)
        if (ExitOnEscape &&
            (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape)))
        {
            Exit();
        }
#endif

        if (s_nextScreen != null) { TransitionScreen(); }

        s_activeScreen?.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        s_activeScreen?.Draw(gameTime);

        base.Draw(gameTime);
    }

    public static void ChangeScreen(Screen next)
    {
        if (s_activeScreen != next) { s_nextScreen = next; }
    }

    private static void TransitionScreen()
    {
        if (s_activeScreen != null) { s_activeScreen.Dispose(); }

        GC.Collect();

        s_activeScreen = s_nextScreen;
        s_nextScreen = null;

        if (s_activeScreen != null) { s_activeScreen.Initialize(); }
    }
}
