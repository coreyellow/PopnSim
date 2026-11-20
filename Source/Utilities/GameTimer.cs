using System;
using Microsoft.Xna.Framework;

namespace PopnSim.Utilities;

public class GameTimer
{
    public double Duration { get; set; }
    public double Remaining { get; private set; }
    public bool IsRunning { get; private set; }
    public bool Loop { get; set; }
    public event Action Finished;

    public GameTimer(double duration, bool loop = false)
    {
        Duration = duration;
        Remaining = duration;
        Loop = loop;
    }

    public void Start() => IsRunning = true;
    public void Stop() => IsRunning = false;

    public void Reset(bool restart = false)
    {
        Remaining = Duration;
        IsRunning = restart;
    }

    /// <summary>
    /// Updates remaining time based on elapsed gameTime.
    /// </summary>
    /// <param name="gameTime"></param>
    /// <returns>True if the timer has finished, false otherwise.</returns>
    public bool Update(GameTime gameTime)
    {
        if (!IsRunning) return false;

        Remaining -= gameTime.ElapsedGameTime.TotalSeconds;
        if (Remaining <= 0)
        {
            Finished?.Invoke();
            if (Loop)
            {
                Remaining += Duration;
            }
            else
            {
                Remaining = 0;
                IsRunning = false;
            }
            return true;
        }
        return false;
    }
}
