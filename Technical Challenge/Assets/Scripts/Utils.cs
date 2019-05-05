using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Unity helper functions
/// </summary>
public static class Utils
{
    /// <summary>
    /// String find for children as a breadth first search
    /// </summary>
    /// <param name="aParent">Transform to search</param>
    /// <param name="aName">Name of object you wish to find</param>
    /// <returns>Transform if found, else null</returns>
    public static Transform FindDeepChild(this Transform aParent, string aName)
    {
        Queue<Transform> queue = new Queue<Transform>();
        queue.Enqueue(aParent);
        while (queue.Count > 0)
        {
            var c = queue.Dequeue();
            if (c.name == aName)
                return c;
            foreach(Transform t in c)
                queue.Enqueue(t);
        }
        return null;
    }    
}
