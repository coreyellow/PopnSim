using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PopnSim.Utilities;
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

    private SpriteFont _textFont;
    private int _fpsCounter;
    private GameTimer _fpsCounterTimer;

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
    }

    protected override void Initialize()
    {
        _fpsCounterTimer = new GameTimer(0.25, true);

        base.Initialize();
        GraphicsDevice = base.GraphicsDevice;
        SpriteBatch = new SpriteBatch(GraphicsDevice);
    }

    protected override void LoadContent()
    {
        // TODO: use this.Content to load your game content here
        _textFont = Content.Load<SpriteFont>("Fonts/Roboto");
    }

    protected override void Update(GameTime gameTime)
    {
#if !__IOS__
        // Exit the game if the Back button (GamePad) or Escape key (Keyboard) is pressed.
        // (iOS does not allow programmatically closing an app.)
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
#endif

        _fpsCounterTimer.Start();
        if (_fpsCounterTimer.Update(gameTime))
        {
            _fpsCounter = (int)(1.0 / gameTime.ElapsedGameTime.TotalSeconds);
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        SpriteBatch.Begin();

        SpriteBatch.DrawString(_textFont, $"FPS: {_fpsCounter}", new Vector2(10, 10), Color.White);

        SpriteBatch.End();

        base.Draw(gameTime);
    }
}
