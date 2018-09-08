using System;
using Core;

/// <summary>
/// This is/will be an (horribly unorganised) example of a game module, compiled against core library. Something akin to how Quake works I guess...
/// </summary>
namespace ExampleGame
{
    public class Main
    {
        static String GameName = "Example";

        public static void Init(string[] test)
        {
            //NOTE: It's all communication tests so far
            var param = test; //Test for calling external libs with arguments.
            //Simple test if Game->Core communication works.
            Core.Main.GameName = GameName;
            WriteStuffOnConsole(); //Hey it might not work.
            Core.IO.ShaderParser.ParseStuff(); //not sure if shaders are the same between dx and gl
            Core.Main.currentMap = new Core.Map(map);

            Core.Main.initDone = true; //make it return int, unless delegates hate that
        }
        public static void WriteStuffOnConsole()
        {
            Console.WriteLine("yessssssssssss"); //TODO: Now try with Logger class
        }

        //for testing
        //load a map using LoadMap or so function from Core
        //for now we'll improvise and make it a triangle, just like the example, it just has to work
        //also raw list of vertices instead of xml or whatever
        public static float[] map = {-0.5f, -0.5f, -0.5f,
     0.5f, -0.5f, -0.5f,
     0.0f,  0.5f, -0.5f};


    }
}
