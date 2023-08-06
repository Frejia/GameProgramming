using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldCollision : MonoBehaviour
{
   private Material _material;
   
    void Start()
   {
      _material = GetComponent<MeshRenderer>().material;
   }
   
    void OnCollisionEnter(Collision co)
   {
       Debug.Log("Sphere Pos change");
       _material.SetFloat("_Hardness", 0);
       _material.SetVector("_SpherePos", co.transform.position);
       // Give me code to wait for two seconds
       StartCoroutine(Wait());
   }
    
    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(2);
        _material.SetVector("_SpherePos", new Vector3(0, 0, 0));
        _material.SetFloat("_Hardness", 1);
    }

    private void Update()
    {
        /*if (Input.GetMouseButtonDown(0))
        {
            CastClickRay();
        }*/
    }
    
    private void CastClickRay()
    {
        var mousePosition = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(mousePosition.x, mousePosition.y,
            Camera.main.nearClipPlane));

        if (Physics.Raycast(ray, out var hit) && hit.collider.gameObject == gameObject)
        {
            Debug.Log("Click on obj");
            _material.SetFloat("_Hardness", 0);
            _material.SetVector("_SpherePos", hit.point);
            // Give me code to wait for two seconds
            StartCoroutine(Wait());
            this.gameObject.GetComponent<Dissolve>().isdissolving = true;
        }
    }
}
