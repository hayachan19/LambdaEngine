using System;
using System.Collections.Generic;
using System.Text;
using OpenTK.Graphics.OpenGL;

namespace GLRenderer
{
    /// <summary>
    /// This class offers methods for listing extensions supported by graphics adapter.
    /// </summary>
    class Extensions
    {
        //TODO: Dynamic enum generation, might be useful
        /// <summary>
        /// Gets available extensions.
        /// </summary>
        /// <returns>List of extension names.</returns>
        public static List<String> GetList()
        {
            List<String> extensions = new List<String>();
            Int32 amount = GetAmount();
            for (Int32 i = 0; i < amount; i++)
            {
                extensions.Add(GL.GetString(StringNameIndexed.Extensions, i));
            }
            return extensions;
        }
        /// <summary>
        /// Gets the count of available extensions.
        /// </summary>
        /// <returns>The amount of extensions.</returns>
        public static Int32 GetAmount() => GL.GetInteger((GetPName)All.NumExtensions);
        /// <summary>
        /// Checks if the extension is present.
        /// </summary>
        /// <param name="extension">Name of the extension.</param>
        /// <returns>True if it is available.</returns>
        //TODO: Replace bools with ints? For error codes.
        public static System.Boolean IsPresent(String extension) => GetList().Contains(extension);
    }
}
