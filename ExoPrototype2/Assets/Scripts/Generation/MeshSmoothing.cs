using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class MeshSmoothing : MonoBehaviour
{
    public  float noiseScale = 0.5f;
    public float noiseStrength = 1f;

    private MeshFilter meshFilter;
    private MeshRenderer _meshRenderer;
    private Mesh originalMesh;
    Vector3[] vertPositions;
    
    public float scaleX = 1f;
    public float scaleY = 1f;
    
    private GameObject visage;
    private Mesh mesh;
    private Vector3[] originalVertices;
    
    void Start()
    {

        /*meshFilter = GetComponent<MeshFilter>();
        _meshRenderer = GetComponent<MeshRenderer>();
        if (meshFilter == null)
        {
            Debug.LogError("MeshFilter component not found!");
            return;
        }

       
        if (originalMesh == null)
        {
            Debug.LogError("No mesh found in MeshFilter component!");
            return;
        }

        SmoothMesh();*/
       
        //ApplyPerlin_Hard(this.gameObject);

    }

    void ApplyPerlin()
    {
        Vector3[] vertices = originalMesh.vertices;

        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 originalVertex = originalVertices[i];
            Vector3 offset = new Vector3(
                Mathf.PerlinNoise((originalVertex.x + transform.position.x) * noiseScale, 0),
                Mathf.PerlinNoise(0, (originalVertex.y + transform.position.y) * noiseScale),
                Mathf.PerlinNoise((originalVertex.z + transform.position.z) * noiseScale, 0)
            );
            
                vertices[i] = originalVertex + offset * noiseStrength;
        }

        originalMesh.vertices = vertices;
        originalMesh.RecalculateNormals();
    }
    
    public void ApplyPerlin_Hard(GameObject meshObj)
    {
        meshFilter = meshObj.GetComponent<MeshFilter>();
        originalMesh = meshFilter.mesh;
        originalVertices = originalMesh.vertices;
        Vector3[] vertices = originalMesh.vertices;

        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 originalVertex = originalVertices[i];
            float noiseValue = Mathf.PerlinNoise(
                (originalVertex.x + transform.position.x) * noiseScale,
                (originalVertex.y + transform.position.y) * noiseScale
            );

            // Determine a threshold value for surface generation
            float threshold = 0.5f;

            // Apply noise strength based on whether noise value is above threshold
            if (noiseValue > threshold)
            {
                Vector3 offset = new Vector3(
                    Mathf.PerlinNoise((originalVertex.x + transform.position.x) * noiseScale, 100),
                    Mathf.PerlinNoise(100, (originalVertex.y + transform.position.y) * noiseScale),
                    Mathf.PerlinNoise((originalVertex.z + transform.position.z) * noiseScale, 200)
                );

                vertices[i] = originalVertex + offset * noiseStrength;
            }
            else
            {
                // Apply noise only on the Y-axis to create blocky appearance
                Vector3 offset = new Vector3(0, 
                    Mathf.PerlinNoise(0, (originalVertex.y + transform.position.y) * noiseScale),
                    0
                );

                vertices[i] = originalVertex + offset * noiseStrength;
            }
        }

        originalMesh.vertices = vertices;
        originalMesh.RecalculateNormals();
    }
    
}