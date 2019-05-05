using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the movement of the boxes in the background of the main menu
/// </summary>
public class BackgroundBox : MonoBehaviour
{
    /// <summary>
    /// The direction how the cube should be rotating over time
    /// </summary>
    public Vector3 rotationDirection;
    
    // The direction the cube should be moving over time
    public Vector3 movementDirection;

    private void Update()
    {
        transform.localPosition += movementDirection * Time.deltaTime;
        transform.localEulerAngles += rotationDirection * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<BackgroundBox>())
        {
            movementDirection = -movementDirection;
            rotationDirection = -rotationDirection;
        }
    }
}
