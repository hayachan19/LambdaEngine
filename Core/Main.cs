using System;
using System.Collections.Generic;

/// <summary>
/// This library will contain all engine logic that'll be referenced statically by the engine and game module, and acts somewhat as a communication layer between two.
/// TODO: Why did I mention "delegates" here? Find out.
/// </summary>
namespace Core
{
    public class Main
    {
        public static string GameName;
        public static bool initDone = false; //TODO: Replace with int
        public static Map currentMap;

        public static void GiveMeResult(string[] whatever, ref int[] result) //External lib param test
        {
            //result = new int[3];
            result[0] = 3;
        }

        //TODO: Rewrite together with Map class
        public static void LoadMap(float[] data)
        {
            Core.Main.initDone = false;
            currentMap = new Map(data);
            initDone = true;
        }
    }
}
