using PopnSim;
using System.Diagnostics;

// Makes logging methods like Trace.WriteLine() and Debug.WriteLine() work in Debug mode.
Trace.Listeners.Add(new ConsoleTraceListener());

using var game = new Game1();
game.Run();
