namespace GLRenderer.Buffer
{
    /// <summary>
    /// Buffer class, creates managed representation
    /// TODO: Clean up
    /// </summary>
    public class Buffer : System.IDisposable
    {
        /*vao = vbo state
          vbo = vbo
          ebo/ibo = index*/

        public uint Address { get; set; }

        //private OpenTK.Graphics.OpenGL.BufferTarget type;
        public OpenTK.Graphics.OpenGL.BufferTarget Type;// => this.type; //for identification


        public Buffer(OpenTK.Graphics.OpenGL.BufferTarget bufferTarget)
        {
            this.Address = (System.UInt16)OpenTK.Graphics.OpenGL.GL.GenBuffer();
            this.Type = bufferTarget;
        }

        //public void BufferData<T>(T[] data)
        //{
        //    GL.BindBuffer(type, address);
        //    UInt16 size;
        //    var ass = data.GetType().Name;
        //    switch (data.GetType().Name)
        //    {
        //        case nameof(Int32): size = sizeof(Int32); break;
        //        case nameof(Single): size = sizeof(Single); break;
        //        default:
        //            break;
        //    }

        //    //GL.BufferData(type, data.Length * sizeof(float), data, BufferUsageHint.StaticDraw);
        //}


        //todo: generics maybe? overload does fine though
        public void BufferData(System.Int32[] data, OpenTK.Graphics.OpenGL.BufferUsageHint usageHint)
        {
            OpenTK.Graphics.OpenGL.GL.BindBuffer(this.Type, this.Address);
            OpenTK.Graphics.OpenGL.GL.BufferData(this.Type, data.Length * sizeof(System.Int32), data, usageHint);
        }

        public void BufferData(System.Single[] data, OpenTK.Graphics.OpenGL.BufferUsageHint usageHint)
        {
            OpenTK.Graphics.OpenGL.GL.BindBuffer(this.Type, this.Address);
            OpenTK.Graphics.OpenGL.GL.BufferData(this.Type, data.Length * sizeof(System.Single), data, usageHint);
        }
        //TODO: Generic argument method doesn't want to cooperate, work it out
        /*public void BufferData<T>(T[] data, OpenTK.Graphics.OpenGL.BufferUsageHint usageHint)
        {
            OpenTK.Graphics.OpenGL.GL.BindBuffer(this.type, this.Address);
            // OpenTK.Graphics.OpenGL.GL.BufferData<T>(this.type, data.Length * System.Runtime.InteropServices.Marshal.SizeOf<T>(), data, usageHint);
            OpenTK.Graphics.OpenGL.GL.BufferData<T>(this.type, data.Length * System.Runtime.InteropServices.Marshal.SizeOf<T>(), data, usageHint);
            //System.Runtime.InteropServices.Marshal.SizeOf<T>
        }*/

        public void Unbind()
        {
            OpenTK.Graphics.OpenGL.GL.BindBuffer(this.Type, 0);
        }


        /*private void Unbind()
         {
             GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
             GL.BindVertexArray(0);
             GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
         }*/
        /// <summary>
        /// 
        /// </summary>
        /// <param name="layout">Layout number of the shader.</param>
        /// <param name="size">Size of the data, must be in range of 1-4.</param>
        /// <param name="type"></param>
        /// <param name="stride">Amount of data belonging to single vertex.</param>
        /// <param name="offset"></param>
        public void SetAttribute(System.Byte layout, System.Int32 size, OpenTK.Graphics.OpenGL.VertexAttribPointerType type, System.Int32 stride, System.Int32 offset)
        {
            OpenTK.Graphics.OpenGL.GL.VertexAttribPointer(layout, size, type, false, stride * sizeof(System.Single), offset * sizeof(System.Single));
            OpenTK.Graphics.OpenGL.GL.EnableVertexAttribArray(layout);
        }

        public void Dispose()
        {
            this.Unbind();
            OpenTK.Graphics.OpenGL.GL.DeleteBuffer(this.Address);
            Common.Logger.Dispose(typeof(Buffer), new System.String[] { this.Address.ToString(), this.Type.ToString() });
        }

        ~Buffer()
        {
            Common.Logger.GC(typeof(Buffer), new System.String[] { this.Address.ToString(), this.Type.ToString() });
        }

    }
}
