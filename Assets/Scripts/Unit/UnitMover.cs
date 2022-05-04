using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitMover : MonoBehaviour
{
    NavMeshAgent agent;
    Animator _animator;
    [SerializeField] GameObject sprite;
    public Transform target; // daha sonra hedefle ilgili yapılacaklar icin dusman ya da bina gibi objeler

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
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
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        UpdateAnimator();
        if(!agent.hasPath) return;
        if(agent.remainingDistance > agent.stoppingDistance) return;

        agent.ResetPath();

        
    }
    void UpdateAnimator()
    {
        Vector3 v =agent.velocity;
        Vector3 localV = transform.InverseTransformDirection(v);
        float s = localV.z;
        _animator.SetFloat("speed",s);
    }
}
