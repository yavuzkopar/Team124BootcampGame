using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitMover : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField] GameObject sprite;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void MoveToPoint(Vector3 point)
    {
        agent.SetDestination(point);
    }
    public void Select()
    {
        sprite.SetActive(true);
    }
    public void Deselect()
    {
        sprite.SetActive(false);
    }
}
