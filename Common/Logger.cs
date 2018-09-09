using System;

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
        public static void Dispose<T>(T type, string[] paramArray)
        {
            string tempString = paramArray[0];
            for (int i = 1; i < paramArray.Length; i++)
            {
                tempString += string.Format(", {0}", paramArray[i]);
            }
            Console.WriteLine($"{type.ToString()}: ({tempString}): object disposed.");
        }
        //TODO: Dispose is apparently wonky when it comes to GC, investigate the case.
        public static void GC<T>(T type, string[] paramArray)
        {
            string tempString = paramArray[0];
            for (int i = 1; i < paramArray.Length; i++)
            {
                tempString += $", {paramArray[i]}";
            }
            Console.WriteLine($"{type.ToString()}: ({tempString}): object CG'd.");
        }
        //TODO: Non-fatal exceptions?
        public static void Exception(object ex)
        {
            Console.WriteLine("An exception has occured: ");
            Console.WriteLine(ex.ToString());
        }
    }
}
