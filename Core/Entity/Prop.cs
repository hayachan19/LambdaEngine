using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entity
{
    /// <summary>
    /// This class represents non-playable objects, that fit in the "dynamics" part of the map.
    /// Added interfaces because they were here and seem appropriate. Probably not.
    /// </summary>
    class Prop : IDisposable, ICloneable
    {
        public Prop()
        {

        }

        public object Clone()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
