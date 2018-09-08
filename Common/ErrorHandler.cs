using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    /// <summary>
    /// Simple error handler.
    /// TODO: Expand.
    /// </summary>
        
    class ErrorHandler
    {
        public static void CurrentDomain_UnhandledException(Object sender, UnhandledExceptionEventArgs e) => Report(e.ExceptionObject);

        public static void Report(Object ex)
        {
            Console.WriteLine("An exception has occured: ");
            Console.WriteLine(ex.ToString());
            Console.WriteLine("Press Enter to terminate.");
            Console.ReadLine();
            Console.WriteLine("Exiting.");
            Environment.Exit(-1);
        }
    }
}
