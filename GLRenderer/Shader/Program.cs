using System;
using System.Collections.Generic;
using System.Text;
using OpenTK.Graphics.OpenGL;

namespace GLRenderer.Shader
{
    /// <summary>
    /// Shader program function.
    /// TODO: Are we transparent enough here?
    /// </summary>
    public class Program : IDisposable
    {
        private UInt16 address;

        public Program(Dictionary<ShaderType, Shader> shaderList)
        {
            Compile(shaderList);
            CleanUp(shaderList);
        }
        private void Compile(Dictionary<ShaderType, Shader> shaderList)
        {
            this.address = (UInt16)GL.CreateProgram();
            foreach (Shader shader in shaderList.Values)
            {
                GL.AttachShader(this.address, shader.Address); //multiple needed
            }
            GL.LinkProgram(this.address);
            Console.WriteLine(GL.GetProgramInfoLog(this.address));
        }
        private void CleanUp(Dictionary<ShaderType, Shader> shaderList)
        {
            foreach (Shader shader in shaderList.Values)
            {
                shader.Dispose();
            }
        }

        public void Use()
        {
            GL.UseProgram(this.address);
        }

        private void Delete()
        {
            GL.DeleteProgram(this.address);
        }

        //TODO: Maybe rewrite and document?
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="input"></param>
        public void SetAttribute<T>(String name, T input)
        {
            Int32 program = GL.GetUniformLocation(this.address, name);
            switch (typeof(T).Name) //hmm
            {
                case nameof(Int16): { Int16 output = (Int16)(Object)input; GL.Uniform1(program, output); break; }
                case nameof(Int32): { Int32 output = (Int32)(Object)input; GL.Uniform1(program, output); break; }
                case nameof(Int64): { Int64 output = (Int64)(Object)input; GL.Uniform1(program, output); break; }
                case nameof(Single): { Single output = (Single)(Object)input; GL.Uniform1(program, output); break; }
                case nameof(Double): { Double output = (Double)(Object)input; GL.Uniform1(program, output); break; }
                case nameof(OpenTK.Vector2): { OpenTK.Vector2 output = (OpenTK.Vector2)(Object)input; GL.Uniform2(program, ref output); break; }
                case nameof(OpenTK.Vector3): { OpenTK.Vector3 output = (OpenTK.Vector3)(Object)input; GL.Uniform3(program, ref output); break; }
                case nameof(OpenTK.Vector4): { OpenTK.Vector4 output = (OpenTK.Vector4)(Object)input; GL.Uniform4(program, ref output); break; }
                case nameof(OpenTK.Matrix2): { OpenTK.Matrix2 output = (OpenTK.Matrix2)(Object)input; GL.UniformMatrix2(program, false, ref output); break; }
                case nameof(OpenTK.Matrix3): { OpenTK.Matrix3 output = (OpenTK.Matrix3)(Object)input; GL.UniformMatrix3(program, false, ref output); break; }
                case nameof(OpenTK.Matrix4): { OpenTK.Matrix4 output = (OpenTK.Matrix4)(Object)input; GL.UniformMatrix4(program, false, ref output); break; }
                default: throw new Exception("Bad type providen");
            }
        }

        public void Dispose()
        {
            Delete();
            //todo name
            Common.Logger.Dispose(typeof(Program), new String[] { this.address.ToString() });
        }

        ~Program()
        {
            Common.Logger.GC(typeof(Program), new String[] { this.address.ToString() });
        }
    }
}
