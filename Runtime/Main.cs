using OpenTK;
using System;
using System.Xml;

//core
//examplegame
//game ? change name?
//renderer

//example calls core.putThing
//core sends to game something but what? so far it was game pulling things from core
//game adds Thing to the list, and calls renderer to draw it

//maybe game overlooks variables in game? the game itself would hold things like physics and stuff


//examplegame uses templates from core, actor etc
//core also does communication right
//so for shaders
/*
 example provides own shaders
 core has a function that passes it to main
 example during init calls SendShaders from shader class of core, that could be automated if there's such need
 SendShaders reads xml and returns shaders to public variable of class
 !!!make sure the order is right!!! renderer sends shaders to gl init
 magic happens and we're ready to go

 */

using static Game.ModuleLoader;
namespace Game
{
    /// <summary>
/// TODO: proper summary
/// TODO: Fix those namespaces plz
/// TODO: Clean up document blah blah
/// TODO: Framework vs not-framework type name debacle, time to settle it
/// </summary>
    class Program
    {
        public static Type coreModule;
        public static Type gameModule;

        public struct Resolution
        {
            public UInt16 width;
            public UInt16 height;
            public UInt16 depth;
            public UInt16 refresh;
        }
        static DisplayDevice displayDevice = null;

        static void Main(String[] args)
        {
            //AppDomain.CurrentDomain.UnhandledException += Common.ErrorHandler.CurrentDomain_UnhandledException;

            //load core and expose
            //but we don't need core
            // coreModule = LoadModuleAndExtractClass("Core.dll", "Main");





            // int width; int height;

            displayDevice = GetPrimaryDisplay();
            ParseArguments(args, out Resolution res, out Renderer.RenderAPI renderer, out Byte major, out Byte minor);
            //gameModule = ModuleLoader.LoadModuleAndExtractClass("ExampleGame", "Main");
            ModuleLoader.ExtractMethodAndExecute(gameModule, "Init", new string[] { }); //put game init after window init
            //ModuleLoader.ExtractClass()
            switch (renderer)
            {
                case Renderer.RenderAPI.OpenGL:
                    using (OpenTKWindow main = new OpenTKWindow(res.width, res.height, GameWindowFlags.Default, displayDevice, Renderer.RenderAPI.OpenGL, major, minor))
                    {
#error If you need to run this, fix namespaces first.
                        main.UpdateFrame += GameLoop.MainLoop; //NOTE: Not gonna work due to namespace messup
                        main.RenderFrame += Renderer.RenderFrame;
                        main.Run(200, res.refresh);
                    }
                    break;
                case Renderer.RenderAPI.Vulkan:
                    throw new NotImplementedException("Vulkan not implemented.");
                case Renderer.RenderAPI.DirectX:
                    throw new NotImplementedException("DirectX not implemented.");
                case Renderer.RenderAPI.Software:
                    throw new NotImplementedException("Software mode not implemented.");
                default:
                    break;
            }          
        }

        private static DisplayDevice GetPrimaryDisplay()
        {
            DisplayDevice currentDisplay = DisplayDevice.GetDisplay(DisplayIndex.Default);
            for (SByte display = -1; display < 8; display++)
            {
                currentDisplay = DisplayDevice.GetDisplay((DisplayIndex)display);
                if (currentDisplay == null)
                {
                    break;
                }

                if (currentDisplay.IsPrimary)
                {
                    break;
                }
            }
            return currentDisplay;
        }
        //TODO: Separate?
        public static void ParseArguments(String[] args, out Resolution res, out Renderer.RenderAPI renderer, out Byte major, out Byte minor)
        {
            renderer = Renderer.RenderAPI.Software;
            major = 0;
            minor = 0;
            res.width = 640;
            res.height = 480;
            res.depth = 16;
            res.refresh = 60;

            UInt16 currentArg = 0;

            foreach (String arg in args)
            {
                if (currentArg + 1 == args.Length)
                {
                    break;
                }

                String nextArg = args[currentArg + 1];
                if (arg[0].Equals('-'))
                {
                    switch (arg.TrimStart('-'))
                    {
                        case "game":
                            gameModule = LoadModuleAndExtractClass(nextArg, "Main");
                            break;
                        case "render":
                            /*switch (nextArg)
                            {
                                case var i when i.StartsWith("opengl"):
                                    break;
                            }*/
                            //hack: maybe change to switch?
                            if (nextArg.StartsWith("opengl"))
                            {
                                renderer = Renderer.RenderAPI.OpenGL;
                                String version = nextArg.Substring(6);
                                major = Byte.Parse(version[0].ToString()); minor = Byte.Parse(version[1].ToString());
                            }
                            else if (nextArg.StartsWith("vulkan"))
                            {
                                renderer = Renderer.RenderAPI.Vulkan;
                            }
                            else if (nextArg.StartsWith("dx"))
                            {
                                renderer = Renderer.RenderAPI.DirectX;
                                String version = nextArg.Substring(2);
                                major = Byte.Parse(version);
                            }
                            else if (nextArg.StartsWith("soft"))
                            {
                                renderer = Renderer.RenderAPI.Software;
                            }
                            else
                            {
                                throw new Exception("Invalid renderer.");
                            }

                            break;
                        case "res":
                            String[] resString = nextArg.Split(new Char[2] { 'x', '@' });
                            if (resString.Length != 4)
                            {
                                break;
                            }

                            Boolean validResolution = false;
                            Resolution requestedResolution = new Resolution
                            {
                                width = UInt16.Parse(resString[0]),
                                height = UInt16.Parse(resString[1]),
                                depth = UInt16.Parse(resString[2]),
                                refresh = UInt16.Parse(resString[3])
                            };
                            foreach (DisplayResolution testedResolution in displayDevice.AvailableResolutions)
                            {
                                bool w = false;
                                bool h = false;
                                bool d = false;
                                bool r = false;
                                bool rf = false;
                                UInt16 re = (UInt16)testedResolution.RefreshRate;

                                if (requestedResolution.width.Equals(Convert.ToUInt16(testedResolution.Width)) ||
                                 requestedResolution.height.Equals(Convert.ToUInt16(testedResolution.Height)) ||
                                 requestedResolution.depth.Equals(Convert.ToUInt16(testedResolution.BitsPerPixel)) ||
                                 requestedResolution.refresh.Equals(Convert.ToUInt16(testedResolution.RefreshRate)))
                                //if (requestedResolution.width.Equals(testedResolution.Width) && requestedResolution.height.Equals(testedResolution.Height) && requestedResolution.depth.Equals(testedResolution.BitsPerPixel) && requestedResolution.refresh.Equals((UInt16)testedResolution.RefreshRate))
                                {
                                    validResolution = true;
                                    res.width = requestedResolution.width;
                                    res.height = requestedResolution.height;
                                    res.depth = requestedResolution.depth;
                                    res.refresh = requestedResolution.refresh;
                                }
                                if (validResolution)
                                {
                                    break;
                                }
                            }
                            if (!validResolution)
                            {
                                throw new Exception("Invalid resolution.");
                            }

                            break;
                        default:
                            break;
                    }
                }
                currentArg++;
            }
        }
    }
}
