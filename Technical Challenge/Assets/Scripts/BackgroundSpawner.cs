using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

/// <summary>
/// Spawns background objects which float around and interact with each other
/// </summary>
public class BackgroundSpawner : MonoBehaviour
{
    /// <summary>
    /// How many objects should spawn
    /// </summary>
    public int objectsToSpawn = 50;

    /// <summary>
    /// Area the objects should be contained in
    /// </summary>
    public Bounds objectsBounds = new Bounds(Vector3.zero, Vector3.one * 25);

    /// <summary>
    /// Random scale to be applied to the object
    /// </summary>
    public Vector2 objectScaleMinMax = new Vector2(0.8f, 1.2f);
    
    /// <summary>
    /// Random scale to be applied to the object
    /// </summary>
    public Vector2 objectSpeedMinMax = new Vector2(0.8f, 1.2f);

    /// <summary>
    /// List of all spawned objects
    /// </summary>
    private List<BackgroundBox> spawnedObjects = new List<BackgroundBox>();

    private void Start()
    {
        for(int i = 0; i < objectsToSpawn; i++)
        {
            // Spawn the box and parent it to the gameobject
            GameObject spawned = GameObject.CreatePrimitive(PrimitiveType.Cube);
            spawned.transform.SetParent(transform);
            
            // Set a random scale rotation and position
            spawned.transform.localScale = Vector3.one * Random.Range(objectScaleMinMax.x, objectScaleMinMax.y);
            spawned.transform.rotation = Quaternion.Euler(Random.Range(0, 359), Random.Range(0, 359), Random.Range(0, 359));
            spawned.transform.position = new Vector3(
                Random.RandomRange(objectsBounds.min.x, objectsBounds.max.x),
                Random.RandomRange(objectsBounds.min.y, objectsBounds.max.y),
                Random.RandomRange(objectsBounds.min.z, objectsBounds.max.z));

            spawned.AddComponent<Rigidbody>().useGravity = false;
            spawned.GetComponent<MeshRenderer>().material.color = Random.ColorHSV();
            
            BackgroundBox backgroundBox = spawned.AddComponent<BackgroundBox>();

            backgroundBox.movementDirection = Random.onUnitSphere;
            backgroundBox.rotationDirection = new Vector3(Random.Range(30, 90), Random.Range(30, 90),Random.Range(30, 90));
            
            spawnedObjects.Add(backgroundBox);
        }
    }

    private void LateUpdate()
    {
        // Check if box is outside of bounds, if so move it to the opposite side
        foreach (var box in spawnedObjects)
        {
            if (objectsBounds.Contains(box.transform.position) == false)
            {
                Vector3 boxPos = box.transform.position;
                
                if (boxPos.x < objectsBounds.min.x)
                {
                    boxPos.x = objectsBounds.max.x;
                }
                if (boxPos.x > objectsBounds.max.x)
                {
                    boxPos.x = objectsBounds.min.x;
                }
                if (boxPos.y < objectsBounds.min.y)
                {
                    boxPos.y = objectsBounds.max.y;
                }
                if (boxPos.y > objectsBounds.max.y)
                {
                    boxPos.y = objectsBounds.min.y;
                }
                if (boxPos.z < objectsBounds.min.z)
                {
                    boxPos.z = objectsBounds.max.z;
                }
                if (boxPos.z > objectsBounds.max.z)
                {
                    boxPos.z = objectsBounds.min.z;
                }

                box.transform.position = boxPos;
            }
        }
    }
}
