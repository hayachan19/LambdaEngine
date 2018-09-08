using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Text;
//using Assimp;

namespace GLRenderer
{
    /// <summary>
    /// Singleton class to represent OpenGL context.
    /// The goal is to translate(almost) everything happening inside state machine into managed objects.
    /// NOTE: The naming is confusing, since there can be more than one context.
    /// TODO: Consider refactoring, or making a Context class so the singleton can work with Context objects.
    /// </summary>
    class MasterClass
    {
        public sealed class Context
        {
            private static readonly Lazy<Context> lazy =
                new Lazy<Context>(() => new Context());
            
            public static Context Instance { get { return lazy.Value; } }

            //public Dictionary<ShaderType, Shader.Shader> ShaderList { get; set; } = new Dictionary<ShaderType, Shader.Shader>(); //Reminder: Shaders on their own are useless.
            public List<Buffer.Buffer> BufferList { get; set; } = new List<Buffer.Buffer>();
            public Dictionary<String, Shader.Program> ProgramList { get; set; } = new Dictionary<string, Shader.Program>();
            private Context()
            {
                //NOTE: Do we need that constructor?
            }

            //TODO: Complete this class on the go.
            public static class ShaderProgram
            {
                public static void Create(string programName, Dictionary<ShaderType, Shader.Shader> shaders)
                {
                    Instance.ProgramList.Add(programName, new Shader.Program(shaders));
                }
            }

            public static class VertexBufferObject
            {
                public static void Create()
                {

                }
            }
        }
    }
}
