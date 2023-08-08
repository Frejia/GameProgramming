using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingController : MonoBehaviour
{
    AStarAgent _Agent;
    [SerializeField] Transform _MoveToPoint;
    [SerializeField] Animator _Anim;
    [SerializeField] AnimationCurve _SpeedCurve;
    [SerializeField] float _Speed;
    private void Start()
    {
        _Agent = GetComponent<AStarAgent>();
        StartCoroutine(Coroutine_MoveRandom());
        
        // When enemy sees player, leave curve
        EnemySeesPlayer.CanSee += StopMovement;
        // When enemy doesnt see player, leave curve
        EnemySeesPlayer.CantSee += StartMovement;
    }

    public void StopMovement()
    {
        _Agent.StopMoving();
        Debug.Log("We stop moving");
        StopCoroutine(Coroutine_MoveRandom());
    }
    
    public void StartMovement()
    {
        _Agent.StartMoving();
        Debug.Log("We start moving");
        StartCoroutine(Coroutine_MoveRandom());
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
    
    //New from Fanny Stop Move Random

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
