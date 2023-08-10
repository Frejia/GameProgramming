using System;
using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AStarAgent))]
public class CharacterMoveAB : MonoBehaviour
{
    AStarAgent _Agent;
    [SerializeField] public Transform pointA;
    [SerializeField] public Transform pointB;
    private bool sawPlayer = false;
    [SerializeField] public bool debug;
    
    private void Start()
    {
        _Agent = GetComponent<AStarAgent>();
        //transform.position = pointA.position;
        //StartCoroutine(Coroutine_MoveAB());
        // When enemy sees player, leave curve
        EnemySeesPlayer.CanSee += StartMoving;
    }

    private void StartMoving(GameObject enemy)
    {
        //_Agent.StopMoving();
        sawPlayer = true;
        //GetComponent<CharacterMoveAB>().pointA = transform;
        //GetComponent<CharacterMoveAB>().pointB = GameObject.FindWithTag("Player").transform;
        
        StartCoroutine(enemy.GetComponent<CharacterMoveAB>().Coroutine_MoveAB());
    }
    
    public void StopMoving(GameObject enemy)
    {
        sawPlayer = false;
        //StopCoroutine(enemy.GetComponent<CharacterMoveAB>().Coroutine_MoveAB());
       _Agent.StartMoving();
    }

    private void Update()
    {
        if (debug)
        {
            _Agent.StopMoving();
            debug = false;
        }
    }

    public IEnumerator Coroutine_MoveAB()
    {
        yield return null;
        while (true)
        {
            _Agent.Pathfinding(pointB.position);
            while (_Agent.Status == AStarAgentStatus.Invalid)
            {
                Transform pom1 = pointA;
                pointA = pointB;
                pointB = pom1;
                transform.position = pointA.position;
                _Agent.Pathfinding(pointB.position);
                yield return new WaitForSeconds(0.2f);
            }
            while (_Agent.Status != AStarAgentStatus.Finished)
            {
                yield return null;
            }
            Transform pom = pointA;
            pointA = pointB;
            pointB = pom;
            yield return null;
        }
    }
}
