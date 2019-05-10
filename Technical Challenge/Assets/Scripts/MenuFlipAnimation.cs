using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

/// <summary>
/// Handles animating child elements of UI
/// </summary>
public class MenuFlipAnimation : MonoBehaviour
{
    /// <summary>
    /// Overrides the elements pivot of the children
    /// </summary>
    public Vector2 elementPivot = new Vector2(0.5f,0.5f);

    /// <summary>
    /// If the first element is to be delayed or play immediately
    /// </summary>
    public bool delayFirstElement = false;
    
    /// <summary>
    /// Delay in between elements animation being triggered
    /// </summary>
    public float elementDelay = 0.1f;
    
    /// <summary>
    /// Time in which the animation should be played
    /// </summary>
    public float animationTime = 0.1f;

    /// <summary>
    /// Rotation which should be placed on object in the beginning of the animation
    /// </summary>
    public Vector3 startingRotation = Vector3.zero;

    /// <summary>
    /// Final position of the animation
    /// </summary>
    public Vector3 endingRotation = Vector3.zero;

    private void OnEnable()
    {
        // Set the elements initial position as per the script
        foreach (RectTransform child in transform)
        {
            child.pivot = elementPivot;
            child.localEulerAngles = startingRotation;
        }

        StartCoroutine(_RotateChildren());
    }


    /// <summary>
    /// Iterates through each element and starts the animation when the delay time is reached
    /// </summary>
    private IEnumerator _RotateChildren()
    {
        // If the first element is to be delayed put the timer to 0, else play immediately
        float timer = delayFirstElement ? 0 : elementDelay;

        // Loop through each child waiting for the delay, followed by the rotation
        foreach (Transform child in transform)
        {
            while (timer <= elementDelay)
            {
                timer += Time.deltaTime;

                yield return null;
            }

            StartCoroutine(_RotateElement((RectTransform)child));
                
            timer = 0;
        }
        
        yield return null;
    }
    
    /// <summary>
    /// Rotates a element based on values
    /// </summary>
    /// <param name="aElement">RectTransform which is to be animated</param>
    private IEnumerator _RotateElement(RectTransform aElement)
    {
        float timer = 0;

        // Rotates the element while the animation time iterates
        while (timer <= animationTime)
        {
            aElement.localEulerAngles = Vector3.Slerp(startingRotation, endingRotation, timer / animationTime);
            
            timer += Time.deltaTime;

            yield return null;
        }

        // When the timer is finished ensure the element is at it's proper destination
        aElement.localEulerAngles = endingRotation;
    }

    private void OnDisable()
    {
        // Stop any unfinished animations
        StopAllCoroutines();
    }
}
