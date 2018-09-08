using System;
using System.Collections.Generic;
using System.Text;


namespace Core
{
    /// <summary>
    /// This class implements a camera system, which is based on LookAt functionality. That gives three variables to use - eye, center, and up.
    /// Four basic camera types are available by determining whenether the movement of the eye or the center is static or dynamic, in relation to each other (local space).
    /// 1. Static eye, static center - suitable for 2D view
    /// 2. Static eye, dynamic center - 1st person camera, follow camera
    /// 3. Dynamic eye, static center - 3rd person camera, orbitting camera
    /// 4. Dynamic eye, dynamic center - I have no clue right now
    /// Those should be enough to create all sorts of cameras.
    /// Up variable isn't considered at this point. 6DOF setup needs research. Also quaterions.
    /// TODO: Once you get this to work, consider adding predefinied settings for cameras where target/eye isn't exactly defined (like 1st person camera, get the absolute center of the view).
    /// TODO: Projection matrix wants some perspective love too.
    /// TODO: Also start doing it.
    /// </summary>
    class Camera
    {

    }
}
