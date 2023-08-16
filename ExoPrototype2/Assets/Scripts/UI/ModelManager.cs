using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelManager : MonoBehaviour
{
    [SerializeField] private GameObject _playerModelHolderTransform;
    
    public void SetModel(GameObject model)
    {
        // Delete old model
        foreach (Transform child in _playerModelHolderTransform.transform)
        {
            Destroy(child.gameObject);
        }
    
        

        // Instantiate the model
        GameObject newModelInstance = Instantiate(model, _playerModelHolderTransform.transform.position, _playerModelHolderTransform.transform.rotation, _playerModelHolderTransform.transform);

        // set correct scale
        newModelInstance.transform.localScale = new Vector3(100, 100, 100);
        
        switch (model.gameObject.name)
        {
            case "ShipOne":
                newModelInstance.transform.rotation *= Quaternion.Euler(90, 0, 0);
                break;
            case "ShipTwo":
                newModelInstance.transform.rotation *= Quaternion.Euler(-90, -90, 0);
                break;
            default:
                Debug.Log("Ship does not have correct rotation");
                break;
        }
        
    }
}
