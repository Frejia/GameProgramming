using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    public static Aim Instance { get; private set; }
    public GameObject player;
    public GameObject user { get; set; }
    private void Awake()
    {
        Instance = this;
        player = GameObject.FindGameObjectWithTag("Player");
    }
    
    public Vector3 Aiming()
    {
        //Calculate Angle for Direction of player from this gameobject position in a 3D Space, including the z axis
        Vector3 direction = player.transform.position - user.transform.position;
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
