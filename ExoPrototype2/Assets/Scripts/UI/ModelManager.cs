using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// Used to change the models on the player when using Customization in Main Menu
/// </summary>
public class ModelManager : MonoBehaviour
{
    [FormerlySerializedAs("_playerModelHolderTransform")] [SerializeField] private GameObject playerModelHolderTransform;
    
    public void SetModel(GameObject model)
    {
        // Delete old model
        foreach (Transform child in playerModelHolderTransform.transform)
        {
            Destroy(child.gameObject);
        }
        
        // Instantiate the model
        GameObject newModelInstance = Instantiate(model, playerModelHolderTransform.transform.position, playerModelHolderTransform.transform.rotation, playerModelHolderTransform.transform);

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
