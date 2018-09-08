using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    /// <summary>
    /// This class represents playable objects, that could fit in the "dynamics" part of the map.
    /// Added interfaces because they were here and seem appropriate. Probably not.
    /// TODO: Both this and Prop needs rethinking.
    /// </summary>
    class Actor : IDisposable, ICloneable
    {
        public Actor()
        {

        }
        public static void SpawnActor(Actor actor)
        {
// entity class for holding anything on map? with position and maybe hidden unhidden etc
            //spawns entity
            //information about model to render needs to be sent somehow
        }

        public Object Clone()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
