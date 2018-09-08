using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using OpenTK.Audio.OpenAL;
using OpenTK.Audio;

namespace Game
{
    /// <summary>
    /// Makes OpenTK window for OpenGL
    /// TODO: Equivalents for other renderers
    /// </summary>
    
    class OpenTKWindow : OpenTK.GameWindow
    {
        public OpenTKWindow(UInt16 width, UInt16 height, GameWindowFlags window, DisplayDevice display, Renderer.RenderAPI render) : this(width, height, window, display, render, 3, 3)
        {
            if (render.Equals(Renderer.RenderAPI.OpenGL))
            {
                Console.WriteLine("OpenGL renderer chosen but no version providen, assuming version 3.3."); //NOTE: DX10, programmable pipeline, might look into fixed since that seems fun as well
                this.Load += Renderer.Initialize_OpenGL;
            }
            Renderer.Initialize(this, render);
        }
        public OpenTKWindow(UInt16 width, UInt16 height, GameWindowFlags window, DisplayDevice display, Renderer.RenderAPI render, UInt16 major, UInt16 minor) : base(width, height, GraphicsMode.Default, "Engine", window, display, major, minor, GraphicsContextFlags.ForwardCompatible)
        {

            if (!render.Equals(Renderer.RenderAPI.OpenGL))
            {
                throw new Exception("Failure while requesting renderer.");
            }
            else if (major < 3 || (major == 3 && minor < 3))
            {
                throw new Exception("Requested version of OpenGL is not supported, minimum is 3.3.");
            }
            else
            {
                this.Load += Renderer.Initialize_OpenGL;
                Renderer.Initialize(this, Renderer.RenderAPI.OpenGL);
            }
        }
    }
}
