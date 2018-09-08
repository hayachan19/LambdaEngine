using System;
using System.IO;
using OpenTK.Graphics.OpenGL;

namespace GLRenderer.Shader
{
    /// <summary>
    /// Shader class
    /// TODO: We're reading from hardcoded path and XML files offer in-line code, do something about it here and in ShaderParser
    /// TODO: Clean up
    /// </summary>
    public class Shader : IDisposable
    {
       // private UInt16 address;
        public UInt16 Address { get; }//=> this.address;

        private ShaderType type; //for identification
        public ShaderType Type => this.type;

        public Shader(ShaderType type, String codePath)
        {
            this.Address = Create(type);
            this.type = type;
            Compile(codePath);
        }

        private UInt16 Create(ShaderType type)
        {
            return (UInt16)GL.CreateShader(type);
        }

        private void Compile(String path)
        {
            GL.ShaderSource(this.Address, File.ReadAllText("Shaders\\" + path));
            GL.CompileShader(this.Address);
            Console.WriteLine(GL.GetShaderInfoLog(this.Address));
        }

        private void Delete()
        {
#pragma warning disable IDE0022 // Use expression body for methods
            GL.DeleteShader(this.Address);
#pragma warning restore IDE0022 // Use expression body for methods
        }

        public void Dispose()
        {
            Delete();
            Common.Logger.Dispose(typeof(Shader), new String[] { this.Address.ToString(), this.type.ToString() });
        }

        ~Shader()
        {
            Common.Logger.GC(typeof(Shader), new String[] { this.Address.ToString(), this.type.ToString() });
        }
    }
}