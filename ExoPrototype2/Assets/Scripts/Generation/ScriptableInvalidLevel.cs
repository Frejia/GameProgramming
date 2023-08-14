using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Saves the levels marked as invalid data
/// </summary>
[CreateAssetMenu(fileName = "InvalidLevel", menuName = "InvalidLevel", order = 1)]
public class ScriptableInvalidLevel : ScriptableObject
{
    public int chunkSize, chunkSizeZ, seed;
}
