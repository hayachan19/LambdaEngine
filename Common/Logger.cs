using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    /// <summary>
    /// Outputs whatever you tell it to the console.
    /// Dispose/GC messages are mostly for debugging.
    /// TODO: Add more types.
    /// TODO: Add generic type.
    /// </summary>

    class Logger
    {
        public static void Dispose<T>(T type, String[] paramArray)
        {
            String tempString = paramArray[0];
            for (Int32 i = 1; i < paramArray.Length; i++)
            {
                tempString += String.Format(", {0}", paramArray[i]);
            }
            Console.WriteLine($"{type.ToString()}: ({tempString}): object disposed.");
        }
        //TODO: Dispose is apparently wonky when it comes to GC, investigate the case.
        public static void GC<T>(T type, String[] paramArray)
        {
            String tempString = paramArray[0];
            for (Int32 i = 1; i < paramArray.Length; i++)
            {
                tempString += $", {paramArray[i]}";
            }
            Console.WriteLine($"{type.ToString()}: ({tempString}): object CG'd.");
        }
    }
}
