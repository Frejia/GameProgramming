using System.Collections;
using System.Collections.Generic;
using System.IO;
using Sound;
using TMPro;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Networking;

public class MusicLoader : MonoBehaviour
{
    [SerializeField] private GameObject ButtonPrefab;
    [SerializeField] private GameObject ScrollList;

    private void Start()
    {
        string musicFolderPath = Path.Combine(Application.streamingAssetsPath, "Music");
#if UNITY_EDITOR
        musicFolderPath = Path.Combine(Application.dataPath, "Music");
#endif
        string[] mp3Files = Directory.GetFiles(musicFolderPath, "*.mp3");

        foreach (string filePath in mp3Files)
        {
            StartCoroutine(LoadAudioClip(filePath));
        }
    }

    private IEnumerator LoadAudioClip(string filePath)
    {
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip("file://" + filePath, AudioType.MPEG))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.LogError("Error loading audio: " + www.error);
            }
            else
            {
                AudioClip clip = DownloadHandlerAudioClip.GetContent(www);
                LoadIntoList(clip);
                Debug.Log("Loaded clip: " + clip.name);
                // Use the AudioClip in your game
            }
        }
    }

    private void LoadIntoList(AudioClip clip)
    {
        int i = 0;
        // Init a new button for the given Audioclip
        GameObject button = Instantiate(ButtonPrefab, transform);
        button.transform.parent = ScrollList.transform;
        button.GetComponentInChildren<TextMeshProUGUI>().text = "Music Track " + i;
        button.GetComponentInChildren<MusicButton>().SetAudioClip(clip);
        
        i++;
    }
    
    
}