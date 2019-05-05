using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
    }

    /// <summary>
    /// List of all the menu panels
    /// </summary>
    public MenuPanel[] menuPanels;
    
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
                return;
            }
        }
    }
}
