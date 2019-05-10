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

    public Rigidbody rigidBody;

    public void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }
    
    private void FixedUpdate()
    {
        transform.localPosition += movementDirection * Time.deltaTime;
        transform.Rotate(rotationDirection * Time.deltaTime);

        // Removes any jitter in the rigidbody movements
        rigidBody.velocity = Vector3.zero;
        rigidBody.rotation = Quaternion.identity;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<BackgroundBox>())
        {
            movementDirection = Random.onUnitSphere;
            rotationDirection = new Vector3(Random.Range(5, 90), Random.Range(5, 90),Random.Range(5, 90));
        }
    }
}
