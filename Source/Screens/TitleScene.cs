using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using Gum.Forms.Controls;
using Gum.GueDeriving;

using PopnSim;
using PopnSim.Utilities;
using Gum.Wireframe;

namespace PopnSim.Screens;

public class TitleScreen : Screen
{
    private const string TITLE_TEXT = "Pop'n Sim";

    private SpriteFont _textFont;
    private SpriteFont _titleFont;
    private Vector2 _titleTextSize => _titleFont.MeasureString(TITLE_TEXT);
    private Panel _titlePanel;

    private FramesPerSecondCounter _fpsCounter;

    public override void Initialize()
    {
        base.Initialize();

        Game1.ExitOnEscape = true;

        _fpsCounter = new FramesPerSecondCounter();

        InitializeUI();
    }

    private void InitializeUI()
    {
        // Clear out any previous UI in case we came here from
        // a different screen:
        Gum.GumService.Default.Root.Children.Clear();

        CreateTitlePanel();
    }

    public override void LoadContent()
    {
        _textFont = Content.Load<SpriteFont>("Fonts/Roboto");
        _titleFont = Content.Load<SpriteFont>("Fonts/RobotoLarge");
    }

    public override void Update(GameTime gameTime)
    {
        _fpsCounter.Update(gameTime);
        Gum.GumService.Default.Update(gameTime);
    }

    public override void Draw(GameTime gameTime)
    {
        _fpsCounter.Draw(gameTime);
        Game1.GraphicsDevice.Clear(Color.Black);

        Game1.SpriteBatch.Begin();

        Game1.SpriteBatch.DrawString(
            _textFont, 
            $"FPS: {_fpsCounter.FramesPerSecond}", 
            new Vector2(10, 10), 
            Color.White
        );

        Game1.SpriteBatch.DrawString(
            _titleFont,
            TITLE_TEXT,
            new Vector2(Game1.GraphicsDevice.Viewport.Width / 2 - _titleTextSize.X / 2, 
                        Game1.GraphicsDevice.Viewport.Height / 4 - _titleTextSize.Y / 2),
            Color.White
        );

        Game1.SpriteBatch.End();

        Gum.GumService.Default.Draw();
    }

    private void CreateTitlePanel()
    {
        _titlePanel = new Panel();
        _titlePanel.Dock(Gum.Wireframe.Dock.Fill);
        _titlePanel.AddToRoot();

        Button playButton = new Button
        {
            Width = 100,
            Height = 10,
            Text = "Play",
        };
        playButton.Anchor(Gum.Wireframe.Anchor.Bottom);
        playButton.X = 0;
        playButton.Y = -60;
        playButton.Click += HandlePlayClicked;
        _titlePanel.AddChild(playButton);

        Button optionsButton = new Button
        {
            Width = 100,
            Height = 10,
            Text = "Options",
        };
        optionsButton.Anchor(Gum.Wireframe.Anchor.Bottom);
        optionsButton.X = 0;
        optionsButton.Y = -20;
        optionsButton.Click += HandleOptionsClicked;
        _titlePanel.AddChild(optionsButton);
    }

    private void HandlePlayClicked(object sender, EventArgs e)
    {
        
    }

    private void HandleOptionsClicked(object sender, EventArgs e)
    {

    }
}
