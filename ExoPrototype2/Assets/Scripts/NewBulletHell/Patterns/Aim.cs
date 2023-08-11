using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.TextCore.Text;
using Random = UnityEngine.Random;

public abstract class Aim : MonoBehaviour
{
    //public static Aim Instance { get; private set; }
    public GameObject target { get; set; }
    public GameObject user { get; set; }
    
    public Vector3 Aiming()
    {
        Vector3 direction;
        //Calculate Angle for Direction of player from this gameobject position in a 3D Space, including the z axis
        if (target != null)
        {
            direction = target.transform.position - user.transform.position;
        }
        else
        {
            //This isnt right
            direction = RandomAim();
        }
       
        return direction.normalized;
    }

    public Vector3 RandomAim()
    {
        float angle = Random.Range(0, 360);
        
        float bulDirX = user.transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
        float bulDirY = user.transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);

        Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);
        
        Vector3 bulDir = (bulMoveVector - user.transform.position).normalized;
        return bulDir;
    }
}
