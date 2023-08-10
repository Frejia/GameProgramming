using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FlyingController : MonoBehaviour
{
    AStarAgent _Agent;
    [SerializeField] Transform _MoveToPoint;
    [SerializeField] Animator _Anim;
    [SerializeField] AnimationCurve _SpeedCurve;
    [SerializeField] float _Speed;
    private bool sawPlayer = false;
    
    private void Start()
    {
        _Agent = GetComponent<AStarAgent>();
        StartCoroutine(Coroutine_MoveRandom());
        // When enemy sees player, leave curve
        EnemySeesPlayer.CanSee += StopMoving;
        //EnemySeesPlayer.CantSee += StartMoving;
    }

    private void StopMoving(GameObject enemy)
    {
        StopCoroutine(enemy.GetComponent<FlyingController>().Coroutine_MoveRandom());
    }

    private void StartMoving(GameObject enemy)
    {
        StartCoroutine(enemy.GetComponent<FlyingController>().Coroutine_MoveRandom());
    }
    
    IEnumerator Coroutine_MoveRandom()
    {
        List<Point> freePoints = WorldManager.Instance.GetFreePoints();
        Point start = freePoints[Random.Range(0, freePoints.Count)];
        transform.position = start.WorldPosition;
        while (true)
        {
            Point p = freePoints[Random.Range(0, freePoints.Count)];

            _Agent.Pathfinding(p.WorldPosition);
            while (_Agent.Status != AStarAgentStatus.Finished)
            {
                yield return null;
            }
        }
    }
    
    IEnumerator Coroutine_Animation()
    {
        _Anim.SetBool("Flying", true);
        while (_Agent.Status != AStarAgentStatus.Finished)
        {
            yield return null;
        }
        _Anim.SetBool("Flying", false);
    }
}
