using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InvalidLevel", menuName = "InvalidLevel", order = 1)]
public class ScriptableInvalidLevel : ScriptableObject
{
    public int chunkSize, chunkSizeZ, seed;
}
