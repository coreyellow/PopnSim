using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace PopnSim
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont _textFont;
        private int fpsCounter;
        private double fpsTimer;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);

            // Window settings
            var displayMode = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode;
            _graphics.PreferredBackBufferWidth = displayMode.Width;
            _graphics.PreferredBackBufferHeight = displayMode.Height;
            _graphics.HardwareModeSwitch = false;
            _graphics.IsFullScreen = false;
            Window.IsBorderless = true;

            // Frame rate settings:
            // Disable VSync
            _graphics.SynchronizeWithVerticalRetrace = false;

            // Caps frames
            var FPSCap = 1000;
            IsFixedTimeStep = true;
            TargetElapsedTime = TimeSpan.FromSeconds(1.0 / FPSCap);

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

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

            // Update the FPS counter
            fpsTimer += gameTime.ElapsedGameTime.TotalSeconds;
            if (fpsTimer >= 1.0)
            {
                fpsCounter = (int)(1.0 / gameTime.ElapsedGameTime.TotalSeconds);
                fpsTimer = 0.0;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();

            _spriteBatch.DrawString(_textFont, $"FPS: {fpsCounter}", new Vector2(10, 10), Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
