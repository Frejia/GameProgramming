using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Generates a number of random values in ranges for the Perlin Noise Generator to use
/// </summary>
public class RandomLevel : MonoBehaviour
{
    [Header("RandomValues")]
    private int chunkSize, chunkSizeZ, offset;

    private List<GameObject> wayPoints;
    private bool raceMode, withCurve, sphere, meshSmoothing;
    
    
    public void GenerateRandomLevel()
    {
        PerlinNoiseGen perlin = PerlinNoiseGen.Instance;
        GenerateChunkSize();
        GenerateRandomWayPoints();
        
        perlin.PerlinSetter(chunkSize,chunkSizeZ, offset, raceMode,
            withCurve, sphere, meshSmoothing, wayPoints);
    }

    public void SetRaceMode(bool raceModeOn)
    {
        raceMode = raceModeOn;
        withCurve = raceModeOn;
        chunkSizeZ = 200;
    }

    private void GenerateChunkSize()
    {
        chunkSize = Random.Range(1, 200);
        chunkSizeZ = Random.Range(1, 200);
        offset = Random.Range(0, 30);
    }

    private void GenerateRandomWayPoints()
    {
        //Generate 6 Random Waypoints in the grid area
        for (int i = 0; i < 6; i++)
        {
            int x = Random.Range(0, chunkSize);
            int y = Random.Range(0, chunkSize);
            int z = Random.Range(0, chunkSizeZ);
            Vector3 pos = new Vector3(x, y, z);
            GameObject point = new GameObject("Waypoint" + i);
            point.transform.position = pos;
            wayPoints.Add(point);
        }
    }
    
}
