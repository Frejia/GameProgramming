using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsInView : MonoBehaviour
{
    private Camera camera;
    private MeshRenderer renderer;
    Plane[] cameraFrustum;
    private Collider collider;
    [SerializeField] private int range;
    
    public delegate void EnemyInScreen(GameObject enemy);
    public static event EnemyInScreen OnEnemyInScreen;
    
    private void Start()
    {
        camera = Camera.main;
        renderer = GetComponent<MeshRenderer>();
        collider = GetComponent<Collider>();
    }

    void OnEnemyVisible()
    {
        var bounds = collider.bounds;
        cameraFrustum = GeometryUtility.CalculateFrustumPlanes(camera);
        if (GeometryUtility.TestPlanesAABB(cameraFrustum, bounds))
        {
            // Enemy is visible
            Debug.Log("enemy is visible");
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (Vector3.Distance(player.transform.position, this.gameObject.transform.position) < range)
            {
                Debug.Log("Enemy not enough");
                OnEnemyInScreen(this.gameObject);
            }
            else
            {
                Debug.Log("Enemy not close enough");
                OnEnemyInScreen(null);
            }
        }
        else
        {
            Debug.Log("enemy is not visible");
            OnEnemyInScreen(null);
        }
    }

    private void Update()
    {
        OnEnemyVisible();
        
        var bounds = collider.bounds;
        cameraFrustum = GeometryUtility.CalculateFrustumPlanes(camera);
        if (GeometryUtility.TestPlanesAABB(cameraFrustum, bounds))
        {
            renderer.sharedMaterial.color = Color.green;
            // Enemy is visible
        }
        else
        {
            renderer.sharedMaterial.color = Color.red;
        }
    }

}
