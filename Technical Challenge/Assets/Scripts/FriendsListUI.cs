using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Handles friends list UI spawning
/// This is to be placed on the parent of the list elements
/// </summary>
public class FriendsListUI : MonoBehaviour
{

    /// <summary>
    /// Holds the friends UI details to modify the details
    /// </summary>
    [System.Serializable]
    public struct FriendUIDetails
    {
        /// <summary>
        /// Name of the user
        /// </summary>
        public string name;
        
        /// <summary>
        /// Image for the user
        /// NOTE: If null default will be used
        /// </summary>
        public Sprite image;
    }

    /// <summary>
    /// Holds the UI element for which the friends list will be populated with
    /// </summary>
    public GameObject friendElementPrefab;

    /// <summary>
    /// Manual editing of the friends list, this would be done from a server request in live production
    /// </summary>
    public FriendUIDetails[] friends;
    
    void Start()
    {
        // Check if there is a friend element, if not log an error
        if (friendElementPrefab != null)
        {
            // Iterate through each friend, spawn the UI element and populate the appropriate data
            foreach (var friend in friends)
            {
                // Spawn the and parent the friend element
                GameObject friendElement = Instantiate(friendElementPrefab);
                friendElement.transform.SetParent(transform);

                Transform friendName = friendElement.transform.Find("Name");

                // Set the friend name
                if (friendName != null)
                {
                    Text nameText = friendName.GetComponent<Text>();

                    if (nameText != null)
                    {
                        nameText.text = friend.name;
                    }
                    else
                    {
                        Debug.LogError("No text component on friend name on object" + friendName);
                    }
                }
                else
                {
                    Debug.LogError("Incorrect naming of friend name text object on object " + friendElement);
                }
                
                // Set the friend image if there is one provided
                if (friend.image != null)
                {
                    // Transform.Find() does not work recursively so you have to use this as a workaround
                    // Reference Utils.cs for function
                    Transform friendImage = friendElement.transform.FindDeepChild("User Image");
                    
                    if (friendImage != null)
                    {
                        Image image = friendImage.GetComponent<Image>();

                        if (image != null)
                        {
                            image.sprite = friend.image;
                        }
                        else
                        {
                            Debug.LogError("No Image component on friend image on object" + friendImage);
                        }
                    }
                    else
                    {
                        Debug.LogError("Incorrect naming of friend image object on object " + friendElement);
                    }
                }
                
            }
        }
        else
        {
            Debug.LogError("There is no prefab attached to " + this.gameObject);
        }
    }

}
