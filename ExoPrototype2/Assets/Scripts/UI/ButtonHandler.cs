using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Scene = UnityEditor.SearchService.Scene;

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
}
