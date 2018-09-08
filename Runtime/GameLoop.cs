using OpenTK;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Runtime
{
    /// <summary>
    /// GameLoop
    /// TODO: Is it possible to put basics here (input, console stuff) and then override/append? Also apply that logic to game provided elements to replace base
    /// TODO: Add general stuff
    /// </summary>
    class GameLoop
    {
        //NOTE: Does the UpdateFrame event get replaced with module version?
        //NOTE: Is it possible to have basic controls here and then expand?
        public static Stopwatch stopwatch = new Stopwatch();
        public static void MainLoop(Object sender, FrameEventArgs e)
        {
            if (Core.Main.initDone)
            {
                if(!stopwatch.IsRunning)stopwatch.Start();
                //camera here?
                //scan for camera stuff
                //so input facility
                //core or runtime comes with default 3d camera
                //we need something to manipulate view matrix
                var keyboardState = OpenTK.Input.Keyboard.GetState();
                if (keyboardState.IsKeyDown(OpenTK.Input.Key.W)) { }
                if (keyboardState.IsKeyDown(OpenTK.Input.Key.S)) { }
                if (keyboardState.IsKeyDown(OpenTK.Input.Key.A)) { }
                if (keyboardState.IsKeyDown(OpenTK.Input.Key.D)) { }
                if (keyboardState.IsKeyDown(OpenTK.Input.Key.Q)) { }
                if (keyboardState.IsKeyDown(OpenTK.Input.Key.E)) { }
                
            }

        }
    }
}
