using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PopnSim.Scenes;
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
    /// The scene that is currently active and being updated/drawn.
    /// </summary>
    private static Scene s_activeScene;

    /// <summary>
    /// The next scene to switch to on a scene transition.
    /// </summary>
    private static Scene s_nextScene;

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

        // Start with the title scene
        ChangeScene(new TitleScene());
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

        if (s_nextScene != null) { TransitionScene(); }

        s_activeScene?.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        s_activeScene?.Draw(gameTime);

        base.Draw(gameTime);
    }

    public static void ChangeScene(Scene next)
    {
        if (s_activeScene != next) { s_nextScene = next; }
    }

    private static void TransitionScene()
    {
        if (s_activeScene != null) { s_activeScene.Dispose(); }

        GC.Collect();

        s_activeScene = s_nextScene;
        s_nextScene = null;

        if (s_activeScene != null) { s_activeScene.Initialize(); }
    }
}
