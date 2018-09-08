using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;
using GLRenderer.Shader;
using System;
using GLRenderer.Buffer;

namespace GLRenderer
{
    /// <summary>
    /// 
    /// </summary>
    public class Main
    {
        /*
            Entry point for OpenGL rendering plugin.
            TODO: Document some kind of structure, maybe when working on another renderer.
            TODO: Implement resize/viewport, resize fires on the start so overloading that would do (?)
        */
        static MasterClass.Context context = MasterClass.Context.Instance; //NOTE: See MasterClass.cs for context note.
        
        public static void Init()
        {
            Log();
            //Initialize();
        }

        private static void Log()
        {
            Console.WriteLine("Initializing OpenGL.");
            Console.WriteLine(String.Format("Found {0} ({1}, {2})",
                GL.GetString(StringName.Renderer),
                GL.GetString(StringName.Vendor),
                GL.GetString(StringName.Version)));
            Console.WriteLine(String.Format("Maxmimum GLSL version supported: {0}", GL.GetString(StringName.ShadingLanguageVersion)));
            Console.WriteLine(String.Format("Found {0} extensions", Extensions.GetList().Count));
        }

        static Program currentProgram = null;
        static VertexArray currentVAO = null;
        //static Dictionary<String, Program> programList = new Dictionary<String, Program>();
        //public static Buffer.Buffer wrestling = new Buffer.Buffer(BufferTarget.ArrayBuffer);
        //TODO: Check if combining Init and Initialize is worthy, if so do it, if not rename.
        public static void Initialize()
        {
            //todo: everything, clean up
            //NOTE: This method should only create shader programs, buffer generation needs some rethinking.
            //NOTE: One buffer per model, rendered using instancing. Preload everything that appears in the scene, or just load everything for testing. Buffering method would have to be called on each map change.
            //TODO: Separate buffer creation, and shader creation as well.

            //make shaders

            //make buffers
            //inside vaos, buffer settings and texturing? //text should be done on the fly

            //default rendering values and shader activation to set stuff

            //capabilities

            /*
             so far this does shader generation, and buffering
             real life scenario would load all needed shaders here, not sure about additional generation on the fly
             buffering... iirc every type of object has its own buffer, not sure if that's the case everywhere
             this would prompt for the array containing grouped buffers for each type
             so object loading/buffering would have to be done from a method, which would be called on demand e.g. map change
             prefabs/objects used are enumerated, and each unique thing gets it's own set of buffers

            placement and stuff comes later
             */


            //buffers
            //we need to generate programs, like general shader, skybox shader etc

            //shaderList;// = new Dictionary<ShaderType, Shader.Shader>(7); //shader list for one program

            //load all required shaders, let's start with gathering one group
            //read shader code from somewhere, xml with either file location or content itself should do
            //but for now let's stick to debug way and let's make that default by the way
            var vs = new Shader.Shader(ShaderType.VertexShader, "vertex.glsl");
            var fs = new Shader.Shader(ShaderType.FragmentShader, "fragment.glsl");
            //pack them into list, and we have a shader package ready to be made into a program
            //Dictionary<ShaderType, Shader.Shader> shaderList = new Dictionary<ShaderType, Shader.Shader>(7);
            Dictionary<ShaderType, Shader.Shader> shaderList = new Dictionary<ShaderType, Shader.Shader>(7);
            shaderList.Add(vs.Type, vs); //TODO: Should be override the Add method to avoid specifying shader type?
            shaderList.Add(fs.Type, fs);
            //compile
            //pants = GenerateProgram(shaderList);

            //let's make the list that holds all the programs and add our fresh one to it
            //  Dictionary<string, Program> programList = new Dictionary<string, Program>();
           // context.ProgramList.Add("default", pants);
            //programList.Add("default", pants);
            
            MasterClass.Context.ShaderProgram.Create("default", shaderList);
            
            //list of buffers, or list of groups of buffers
            currentVAO = new VertexArray();

            
            Buffer.Buffer vbo = new Buffer.Buffer(BufferTarget.ArrayBuffer);
            context.BufferList.Add(vbo);
            vbo.BufferData(new Single[] { -0.5f, -0.5f, -0.5f, //buffer could add values for reference if needed but unlikely
     0.5f, -0.5f, -0.5f,
     0.0f,  0.5f, -0.5f },
     BufferUsageHint.StaticDraw);
            //wrestling.BufferData(new int[] { 3, 4 }, BufferUsageHint.StaticDraw);
            // glVertexAttribPointer(0, 3, GL_FLOAT, GL_FALSE, 3 * sizeof(float), (void*)0);
            //  glEnableVertexAttribArray(0);
            currentVAO.Bind();
            //GL.VertexAttribPointer(index, size, type, normalized, stride, offset);
            vbo.SetAttribute(0, 3, VertexAttribPointerType.Float, 3, 0);
            //GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            //GL.EnableVertexAttribArray(0);
            currentVAO.Unbind();


           // context.BufferList.Add(new Buffer.Buffer(BufferTarget.AtomicCounterBuffer));

        }
        /*
        public static Program GenerateProgram(Dictionary<ShaderType, Shader.Shader> shaderList)
        {
            Program program = new Program(shaderList);
            shaderList.Clear();
            return program;
        } //for each group of shaders, put that into a loop within some xml loading function, xml passed from Main
        */

        public static void Draw()
        {
            //todo: everything, clean up
            //this is going to consist of several other methods, skybox pass, normal pass, hud pass, post process pass etc
            //TODO: Generalize, right now this function is used for testing.
            GL.ClearColor(OpenTK.Color.Aqua);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            //currentProgram.Use();
            //foreach vao
            //something to segregate shaders and buffers
            context.ProgramList["default"].Use();
            OpenTK.Matrix4 model, view, projection;
            model = OpenTK.Matrix4.Identity;//OpenTK.Matrix4.CreateTranslation(15.0f, 0.0f, 0.0f);
            view = OpenTK.Matrix4.Identity;
            projection = OpenTK.Matrix4.CreatePerspectiveFieldOfView(1.7f, 1.67f, 0.1f, 100.0f);

            context.ProgramList["default"].SetAttribute("model", model);
            context.ProgramList["default"].SetAttribute("view", view);

            context.ProgramList["default"].SetAttribute("projection", projection);

            currentVAO.Bind();
            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
            currentVAO.Unbind();
        }
    }
}
