using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PopnSim.Utilities;

namespace PopnSim.Scenes;

public class TitleScene : Scene
{
    private const string TITLE_TEXT = "Pop'n Sim";

    private SpriteFont _textFont;
    private SpriteFont _titleFont;
    private int _fpsCounter;
    private GameTimer _fpsCounterTimer;

    public override void Initialize()
    {
        base.Initialize();

        Game1.ExitOnEscape = true;

        _fpsCounterTimer = new GameTimer(0.25, true);
    }

    public override void LoadContent()
    {
        _textFont = Content.Load<SpriteFont>("Fonts/Roboto");
    }

    public override void Update(GameTime gameTime)
    {
        _fpsCounterTimer.Start();
        if (_fpsCounterTimer.Update(gameTime))
        {
            _fpsCounter = (int)(1.0 / gameTime.ElapsedGameTime.TotalSeconds);
        }
    }

    public override void Draw(GameTime gameTime)
    {
        Game1.GraphicsDevice.Clear(Color.Black);

        Game1.SpriteBatch.Begin();

        Game1.SpriteBatch.DrawString(_textFont, $"FPS: {_fpsCounter}", new Vector2(10, 10), Color.White);

        Game1.SpriteBatch.End();
    }
}
