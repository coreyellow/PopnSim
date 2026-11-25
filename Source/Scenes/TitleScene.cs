using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PopnSim.Utilities;
using MonoGame.Extended;

namespace PopnSim.Scenes;

public class TitleScene : Scene
{
    private const string TITLE_TEXT = "Pop'n Sim";

    private SpriteFont _textFont;
    private SpriteFont _titleFont;
    private FramesPerSecondCounter _fpsCounter;

    public override void Initialize()
    {
        base.Initialize();

        Game1.ExitOnEscape = true;

        _fpsCounter = new FramesPerSecondCounter();
    }

    public override void LoadContent()
    {
        _textFont = Content.Load<SpriteFont>("Fonts/Roboto");
    }

    public override void Update(GameTime gameTime)
    {
        _fpsCounter.Update(gameTime);
    }

    public override void Draw(GameTime gameTime)
    {
        _fpsCounter.Draw(gameTime);
        Game1.GraphicsDevice.Clear(Color.Black);

        Game1.SpriteBatch.Begin();
        Game1.SpriteBatch.DrawString(_textFont, $"FPS: {_fpsCounter.FramesPerSecond}", new Vector2(10, 10), Color.White);
        Game1.SpriteBatch.End();
    }
}
