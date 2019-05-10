using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

/// <summary>
/// Handles the menu panel interactions
/// </summary>
public class MainMenuUI : MonoBehaviour
{

    /// <summary>
    /// Holds all the panels and their corresponding side menu buttons
    /// </summary>
    [System.Serializable]
    public struct MenuPanel
    {
        /// <summary>
        /// Button which turns on the corresponding panel
        /// </summary>
        public Button panelButton;
        
        /// <summary>
        /// Panel linked to the button
        /// </summary>
        public GameObject panelGameObject;

        /// <summary>
        /// Position of the camera when the panel is selected
        /// </summary>
        public Vector3 cameraPosition;

        /// <summary>
        /// Rotation of the camera when the panel is selected
        /// </summary>
        public Vector3 cameraRotation;
        
        /// <summary>
        /// How long the camera should take to get to the final position
        /// </summary>
        public float cameraTransitionTime;
    }

    /// <summary>
    /// List of all the menu panels
    /// </summary>
    public MenuPanel[] menuPanels;

    /// <summary>
    /// Camera which is showing background animation, this will be moved on panel changes
    /// </summary>
    public Camera backgroundCamera;
    
    private void Start()
    {
        _TurnOffAllPanels();

        foreach (var panel in menuPanels)
        {
            panel.panelButton.onClick.AddListener(() =>
            {
                _TurnOffAllPanels();
                _TurnOnPanel(panel);
                
                panel.panelButton.GetComponent<Outline>().enabled = true;
            });
        }
        
        if (menuPanels.Length >= 1)
        {
            menuPanels[0].panelButton.onClick.Invoke();
        }
    }

    /// <summary>
    /// Turns off all the panel gameObjects
    /// </summary>
    private void _TurnOffAllPanels()
    {
        foreach (var panel in menuPanels)
        {
            panel.panelGameObject.SetActive(false);
            
            Outline outline = panel.panelButton.GetComponent<Outline>();
            if (outline == null)
            {
                outline = panel.panelButton.gameObject.AddComponent<Outline>();
                outline.effectDistance = new Vector2(10,10);
                outline.effectColor = Color.black;
            }

            outline.enabled = false;
        }
    }

    /// <summary>
    /// Turns on a menu panel
    /// </summary>
    /// <param name="aMenuPanel">Menu panel to turn on</param>
    private void _TurnOnPanel(MenuPanel aMenuPanel)
    {
        foreach (var panel in menuPanels)
        {
            if (panel.panelGameObject == aMenuPanel.panelGameObject)
            {
                panel.panelGameObject.SetActive(true);

                StopAllCoroutines();
                StartCoroutine(_TransitionCamera(panel.cameraPosition, panel.cameraRotation, panel.cameraTransitionTime));
                
                return;
            }
        }
    }

    /// <summary>
    /// Moves the camera to the desired location over a given time
    /// </summary>
    /// <param name="aPosition">Position the camera should move to</param>
    /// <param name="aRotation">Rotation the camera should move to</param>
    /// <param name="aTransitionTime">How long the transition should take</param>
    /// <returns></returns>
    IEnumerator _TransitionCamera(Vector3 aPosition, Vector3 aRotation, float aTransitionTime)
    {
        float timer = 0;

        // Save position for transition calculations
        Vector3 startingPosition = backgroundCamera.transform.position;
        Vector3 startingRotation = backgroundCamera.transform.eulerAngles;
     
        while (timer <= aTransitionTime)
        {
            backgroundCamera.transform.position = Vector3.Slerp(startingPosition, aPosition, timer / aTransitionTime);
            backgroundCamera.transform.eulerAngles = Vector3.Slerp(startingRotation, aRotation, timer / aTransitionTime);
                 
            timer += Time.deltaTime;
     
            yield return null;
        }

        backgroundCamera.transform.position = aPosition;
        backgroundCamera.transform.eulerAngles = aRotation;
    }
}
