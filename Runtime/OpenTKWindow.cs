using System;
using OpenTK;
using OpenTK.Graphics;

namespace Runtime
{
    /// <summary>
    /// Makes OpenTK window for OpenGL
    /// TODO: Equivalents for other renderers, or recycle
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
                throw new Exception("Requested version of OpenGL is not supported, minimum is 3.3."); //We don't deal with legacy for now
            }
            else
            {
                this.Load += Renderer.Initialize_OpenGL;
                Renderer.Initialize(this, Renderer.RenderAPI.OpenGL);
            }
        }
    }
}
