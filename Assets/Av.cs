using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Av : MonoBehaviour
{
    NavMeshAgent agent;
    Health h;
    [SerializeField] GameObject deadarea;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        h = GetComponent<Health>();
    }
    private void Update()
    {
        if (h.IsDead() && !deadarea.activeSelf)
        {
            deadarea.SetActive(true);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (h.IsDead())
        {
            agent.ResetPath();
            
            return;
        }
        if (other.gameObject.CompareTag("Aslan") || other.gameObject.CompareTag("Unit"))
        {
            Vector3 direction = other.transform.position - transform.position;

            agent.SetDestination(transform.position - direction);

        }
        else
            return;
    }
}
