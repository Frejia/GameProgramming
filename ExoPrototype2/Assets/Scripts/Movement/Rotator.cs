using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [Header("Rotator Settings")]
    [SerializeField] private float rotationSpeed = 90.0f;
    public bool debug;
    [SerializeField] private bool isAimPoint = false;
    [SerializeField] private Transform target;
    
    [SerializeField] private bool isRotating = false;
     private Quaternion startRotation { get; set; }
    private Quaternion endRotation { get; set; }

    
    /* TODO: Rotations-Vektor für links und rechts als Variable speichern
     * Neutralen Rotationsvektor speichern
     * Mittels Switch-Case die Rotationen durchführen
     * Sonderfälle: nicht rotieren wenn:
     *      Spieler drückt und maximaler Winkel erreicht ist
     *      Spieler drückt nicht und neutraler Winkel erreicht ist
     */

    private void OnEnable()
    {
        EnemySeesPlayer.CanSee += SpecialTurn;
    }
    
    private void OnDisable()
    {
        EnemySeesPlayer.CanSee -= SpecialTurn;
    }

    private void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
    }

    public void StartRotating(Quaternion startRot, Quaternion endRot)
    {
        if (!isRotating)
        {
            startRotation = startRot;
            endRotation = endRot;
            isRotating = true;
        }
    }
    
    private void SpecialTurn()
    {
        target = GameObject.FindWithTag("Player").transform;
        if (this.gameObject.tag == "Enemy")
        {
            directionToTarget = target.position - transform.position;
            if (isAimPoint)
            {
                directionToTarget = target.position - transform.parent.transform.position;
            }
            endRotation = Quaternion.LookRotation(-directionToTarget);
            StartRotating(this.transform.rotation, endRotation);
        }
    }
    
    public void StopRotating()
    {
        endRotation = startRotation;
        isRotating = false;
    }
    
    private void FixedUpdate()
    {
        if (debug)
        {
            Quaternion endRotation = Quaternion.Euler(90f, 0f, 0f);
            StartRotating(transform.rotation, endRotation);
        }
        

        if (isRotating)
        {
            float step = rotationSpeed * Time.fixedDeltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, endRotation, step);
            // Check if the rotation has reached the endRotation
            /*if (transform.rotation == endRotation)
            {
                isRotating = false;
            }*/
        }
    }

    private Vector3 directionToTarget;
    
    private void Update()
    {
        if (isAimPoint)
        {
            SpecialTurn();
        }
    }

}
