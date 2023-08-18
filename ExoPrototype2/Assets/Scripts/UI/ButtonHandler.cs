using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles UI Interactions and Game References
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
    
    public void Toggle(GameObject panel)
    {
        panel.SetActive(!panel.activeSelf);
    }
    
    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
    public void StartGameShooter()
    {
        GameManager.Instance.SetShooter();
    }
    public void ContinueGame()
    {
        GameManager.Instance.ContinueGame();
    }

    public void SaveLevel()
    {
        MeshCreator.Instance.EditLevelSave();
    }

    public void InvalidLevelSave()
    {
        InvalidLevelSafe.Instance.EditInvalidLevelSave();
    }

    public void GoToMainMenu()
    {
        GameManager.Instance.GoToMainMenu();
        GameManager.Instance.SetNone();
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
