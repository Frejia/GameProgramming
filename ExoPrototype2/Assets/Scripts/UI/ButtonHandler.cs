using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles UI Interactions
/// </summary>
public class ButtonHandler : MonoBehaviour
{
    private SceneManager manager;
    
    public void Enable(GameObject panel){
        panel.SetActive(true);
    }

    public void Disable(GameObject panel)
    {
        panel.SetActive(false);
    }

    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
