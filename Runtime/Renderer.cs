namespace Runtime
{
    /// <summary>
    /// This class handles renderers. There's some OpenTK stuff that should be somewhere else, like that repetitive shader loader.
    /// TODO: Analyse and clean up
    /// NOTE: Unsure if it's possible to keep this class renderer agnostic, since each wrapper has it's own way of creating and controlling the window.
    /// NOTE: It isn't 1990s anymore, we only have two (three) major players and one fancy stuff. This program won't even run on anything that uses something else so why bother.
    /// Library based selection needs native window to hook to and create context, something for later.
    /// </summary>
    class Renderer
    {
        public enum RenderAPI { Software, OpenGL, Vulkan, DirectX } //obamaCmon
        public static System.Reflection.Assembly renderPlugin;
        public static System.Type renderClass;
        public static ModuleLoader.TemporaryDelegate renderingLoop;
        public static System.Xml.XmlDocument[] shaderList;

        //lists of things to render, passed from core

//todo: init and initialize? try to merge, init only does the log, actual stuff must be done after context is created, so in whatever is attached to onload event
        public static void Initialize(System.Object sender, RenderAPI renderer)
        {
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            //doc.Load(XmlReader)
            doc.Load(@"Shaders/ShaderList.xml");
            GetShaders(doc);

            switch (renderer)
            {
                case RenderAPI.OpenGL:
                    renderPlugin = ModuleLoader.LoadModule("GLRenderer.dll");
                    renderClass = ModuleLoader.ExtractClass(renderPlugin, "Main");

//todo: candidate for merging
                    ModuleLoader.ExtractMethod(renderClass, "Init")();
                    //ModuleLoader.ExtractMethodAndExecute(renderClass, "")
                    renderingLoop = ModuleLoader.ExtractMethod(renderClass, "Draw");
                    break;
                case RenderAPI.Vulkan:
                    break;
                case RenderAPI.DirectX:
                    break;
                case RenderAPI.Software:
                default:
                    throw new System.Exception("Failure while initializing the renderer"); //huh?
            }
        }
        //TODO: Move to Core? Dev calls LoadShaders from Core, it analyzes shaders and tells runtime there's shaders to do
        //So this should be scanning for shaders and sending them to appropriate facility
//todo: this side of the renderer should have separate classes for each renderer containing things like that, keep away from plugin since why would it have a xml parser. actually it's about the shadertype type, overrides should be the soluttion
        public static System.Collections.Generic.Dictionary<System.String, System.Collections.Generic.Dictionary<OpenTK.Graphics.OpenGL.ShaderType, System.String>> GetShaders(System.Xml.XmlDocument definitionFile)
        {

            if (definitionFile.GetElementsByTagName("shaderPackage").Count.Equals(0))
            {
                throw new System.Exception("Invalid shader definition file.");
            }

            System.Collections.Generic.Dictionary<OpenTK.Graphics.OpenGL.ShaderType, System.String> shaderGroup = new System.Collections.Generic.Dictionary<OpenTK.Graphics.OpenGL.ShaderType, System.String>(7);
            System.Collections.Generic.Dictionary<System.String, System.Collections.Generic.Dictionary<OpenTK.Graphics.OpenGL.ShaderType, System.String>> shaderPackage = new System.Collections.Generic.Dictionary<System.String, System.Collections.Generic.Dictionary<OpenTK.Graphics.OpenGL.ShaderType, System.String>>();
            System.Xml.XmlNodeList groups = definitionFile.GetElementsByTagName("shaderGroup");
            foreach (System.Xml.XmlNode group in groups)
            {
                System.String namen = group.Attributes["name"].Value;
                foreach (System.Xml.XmlNode shader in group)
                {
                    System.String type = shader.Attributes["type"].Value;
                    OpenTK.Graphics.OpenGL.ShaderType shaderType = OpenTK.Graphics.OpenGL.ShaderType.VertexShader; //init to keep vs silent
                    switch (type)
                    {
                        case "vertex": shaderType = OpenTK.Graphics.OpenGL.ShaderType.VertexShader; break;
                        case "fragment": shaderType = OpenTK.Graphics.OpenGL.ShaderType.FragmentShader; break;
                        case "geometry": shaderType = OpenTK.Graphics.OpenGL.ShaderType.GeometryShader; break;
                        case "compute": shaderType = OpenTK.Graphics.OpenGL.ShaderType.ComputeShader; break;
                        case "tessControl": shaderType = OpenTK.Graphics.OpenGL.ShaderType.TessControlShader; break;
                        case "tessEval": shaderType = OpenTK.Graphics.OpenGL.ShaderType.TessEvaluationShader; break;
                        default: throw new System.Exception("Invalid shader type.");
                    }
                    switch (shader.Name)
                    {
                        case "shaderCode": shaderGroup.Add(shaderType, shader.InnerText); break;
                        case "shaderFile": shaderGroup.Add(shaderType, System.IO.File.ReadAllText("Shaders\\" + shader.InnerText)); break;
                        default: throw new System.Exception("Invalid shader source designator.");
                    }
                }
                shaderPackage.Add(namen, shaderGroup);
                shaderGroup.Clear();
            }
            return shaderPackage;
        }
        // private static Dictionary<string, Shader> shaders;

        #region Renderer initializers
        public static void Initialize_OpenGL(System.Object sender, System.EventArgs e)
        {
            ModuleLoader.ExtractMethod(renderClass, "Initialize")();
        } //late init, after context is established
        public static void Initialize_Vulkan() { }
        public static void Initialize_DirectX() { }
        public static void Initialize_Software() { }
        #endregion
        //OpenTK window
        public static void RenderFrame(System.Object sender, OpenTK.FrameEventArgs e)
        {
            if (Core.Main.initDone) {
                OpenTKWindow main = sender as OpenTKWindow;
                main.Title = $"T-Delta: {(e.Time * 1000).ToString("n0")}ms | Time Elapsed: {GameLoop.stopwatch.Elapsed.ToString(@"mm\:ss")}";
                renderingLoop();
                main.SwapBuffers();
            }          
        }
        //core loads map
        //sends all data somewhere
        //renderer on each turn reads that data and renders it
        //for now let's make it prefabs static and actors dynamic, split into two lists
        //something would have to prep actors for rendering

    }
}
