using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Necesarry for cross-scene objects
/// </summary>
public class DontDestroy : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
