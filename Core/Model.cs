using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    /// <summary>
    /// Holds information about models. Might move it to Runtime since it's more fitting, and write some kind of interface.
    /// </summary>
    public class Model
    {
        public static float[] verts;
        public Model(float[] vertices)
        {
            verts = vertices;
        }
    }
}
