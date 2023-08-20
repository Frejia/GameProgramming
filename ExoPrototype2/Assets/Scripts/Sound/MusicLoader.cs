using System.Collections;
using System.Collections.Generic;
using System.IO;
using Sound;
using TMPro;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// Loads all music files from the Music folder into the MusicList
///
/// Originally worked with only Unity Editor, changed to work in build Runtime
///
/// Folder: Build\Data\StreamingAssets\Music
/// </summary>
public class MusicLoader : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject ButtonPrefab;
    [SerializeField] private GameObject ScrollList;

    // Load all music files from the Music folder
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

    // Load specific Audio Clip
    private IEnumerator LoadAudioClip(string fileName)
    {
        string audioFilePath = Path.Combine(Application.streamingAssetsPath, "Music", fileName);

        // Convert the local file path to a URL format
        string audioFileURL = "file://" + audioFilePath;

        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(audioFileURL, AudioType.MPEG))
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
            }
        }
    }

    int i = 0;
    
    //Create a Button for given Clip
    private void LoadIntoList(AudioClip clip)
    {
        
        // Init a new button for the given Audioclip
        GameObject button = Instantiate(ButtonPrefab, transform);
        button.transform.parent = ScrollList.transform;
        button.GetComponentInChildren<TextMeshProUGUI>().text = "Music Track " + i;
        button.GetComponentInChildren<MusicButton>().SetAudioClip(clip);
        
        i++;
    }
    
    
}