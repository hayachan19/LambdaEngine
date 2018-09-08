using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Text;

namespace GLRenderer.Buffer
{
    /// <summary>
    /// Provides VAO managed representation
    /// TODO: Write proper summary for everything
    /// TODO: Clean up those pragmas
    /// TODO: General clean up and maybe something extra
    /// </summary>
    class VertexArray : IDisposable
    {
        private UInt16 address;

        public VertexArray()
        {
#pragma warning disable IDE0021 // Use expression body for constructors
            this.address = (UInt16)GL.GenVertexArray();
#pragma warning restore IDE0021 // Use expression body for constructors
        }

        public void Bind()
        {
#pragma warning disable IDE0022 // Use expression body for methods
            GL.BindVertexArray(this.address);
#pragma warning restore IDE0022 // Use expression body for methods
        }

        public void Unbind()
        {
#pragma warning disable IDE0022 // Use expression body for methods
            GL.BindVertexArray(0);
#pragma warning restore IDE0022 // Use expression body for methods
        }

        public void Dispose()
        {
            Unbind();
            GL.DeleteVertexArray(this.address);
            Common.Logger.Dispose(typeof(VertexArray), new String[] { this.address.ToString() });
        }

        ~VertexArray()
        {
            Common.Logger.GC(typeof(VertexArray), new String[] { this.address.ToString() });
        }
    }
}
