using System;
using OpenTK; //NOTE: We need that for window creation, later we could try creating a window via native methods and then hook to it. Or just adapt OpenTK window later.

namespace Runtime
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
            public ushort Width, Height, Depth, RefreshRate;
        }
        static DisplayDevice displayDevice = null;

        static void Main(string[] args) //TODO: Parameter count?
        {
            //AppDomain.CurrentDomain.UnhandledException += Common.ErrorHandler.CurrentDomain_UnhandledException; //NOTE: Something's not right with it.

            //load core and expose
            //but we don't need core
            // coreModule = LoadModuleAndExtractClass("Core.dll", "Main");

            displayDevice = GetPrimaryDisplay();
            ParseArguments(args, out Resolution res, out Renderer.RenderAPI renderer, out byte major, out byte minor);
            //gameModule = ModuleLoader.LoadModuleAndExtractClass("ExampleGame", "Main");
            //ModuleLoader.ExtractMethodAndExecute(gameModule, "Init", new string[] { }); //put game init after window init
            ModuleLoader.ExtractMethod<string>(gameModule, "Init")(new string[] { }); //put game init after window init
            switch (renderer) //This exists mostly for making window and event subscription.
            {
                case Renderer.RenderAPI.OpenGL:
                    using (OpenTKWindow main = new OpenTKWindow(res.Width, res.Height, GameWindowFlags.Default, displayDevice, Renderer.RenderAPI.OpenGL, major, minor))
                    {
                        main.UpdateFrame += GameLoop.MainLoop;
                        main.RenderFrame += Renderer.RenderFrame;
                        main.Run(200, res.RefreshRate);
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
            for (sbyte display = -1; display < 8; display++)
            {
                currentDisplay = DisplayDevice.GetDisplay((DisplayIndex)display);
                if (currentDisplay == null) break;
                if (currentDisplay.IsPrimary) break;
            }
            return currentDisplay;
        }
        //TODO: Separate?
        public static void ParseArguments(String[] args, out Resolution res, out Renderer.RenderAPI renderer, out Byte major, out Byte minor)
        {
            //Defaults
            renderer = Renderer.RenderAPI.Software;
            major = 0;
            minor = 0;
            res.Width = 640;
            res.Height = 480;
            res.Depth = 16;
            res.RefreshRate = 60;

            ushort currentArg = 0;

            foreach (string arg in args)
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
                            gameModule = ModuleLoader.LoadModuleAndExtractClass(nextArg, "Main");
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

                            bool validResolution = false;
                            Resolution requestedResolution = new Resolution
                            {
                                Width = ushort.Parse(resString[0]),
                                Height = ushort.Parse(resString[1]),
                                Depth = ushort.Parse(resString[2]),
                                RefreshRate = ushort.Parse(resString[3])
                            };
                            foreach (DisplayResolution testedResolution in displayDevice.AvailableResolutions)
                            {
                                /*bool w = false;
                                bool h = false;
                                bool d = false;
                                bool r = false;
                                bool rf = false;
                                UInt16 re = (UInt16)testedResolution.RefreshRate;*/

                                if (requestedResolution.Width.Equals(Convert.ToUInt16(testedResolution.Width)) ||
                                 requestedResolution.Height.Equals(Convert.ToUInt16(testedResolution.Height)) ||
                                 requestedResolution.Depth.Equals(Convert.ToUInt16(testedResolution.BitsPerPixel)) ||
                                 requestedResolution.RefreshRate.Equals(Convert.ToUInt16(testedResolution.RefreshRate)))
                                //if (requestedResolution.width.Equals(testedResolution.Width) && requestedResolution.height.Equals(testedResolution.Height) && requestedResolution.depth.Equals(testedResolution.BitsPerPixel) && requestedResolution.refresh.Equals((UInt16)testedResolution.RefreshRate))
                                {
                                    validResolution = true;
                                    res.Width = requestedResolution.Width;
                                    res.Height = requestedResolution.Height;
                                    res.Depth = requestedResolution.Depth;
                                    res.RefreshRate = requestedResolution.RefreshRate;
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
