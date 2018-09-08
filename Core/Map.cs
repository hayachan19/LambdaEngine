using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    //map instance
    //constructor loads stuff based on input
    /// <summary>
    /// This class contains information about static objects (prefabs) and dynamic objects (props) on the scene.
    /// While static objects are static, dynamic objects would have to be either updated within map object, or changed into actor which actually sounds pretty good.
    /// </summary>
    public class Map
    {
        //TODO: Rewrite this
        public static bool reloadPending = false; //TODO: Find out what I wanted here.
        public static Dictionary<int, Model> statics = new Dictionary<int, Model>();
        public static Dictionary<int, Model> dynamics = new Dictionary<int, Model>();

        public Map(float[] vertices)//string mapName)
        {
            //loads file with information on type and location of the prefab
            //Actor actor = new Actor();

            //pass the map info / prefab info to Main
            Model obj = new Model(vertices);
            statics.Add(1, obj);

            reloadPending = true;
            
        }
    }
}
