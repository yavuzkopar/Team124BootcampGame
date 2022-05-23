using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIUnit : MonoBehaviour
{
    [SerializeField] float checkRadius = 5f;
  //  [SerializeField] LayerMask enemyLayer;
  [SerializeField] int _damage;
   Sensors sensor;
    NavMeshAgent agent;
  [SerializeField] Health target;
    Animator anim;

    Health baseBuilding;
    Health hp;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        sensor = GetComponentInChildren<Sensors>();
        anim = GetComponent<Animator>();
        baseBuilding = GameObject.FindGameObjectWithTag("base").GetComponent<Health>();
        target = baseBuilding;
        hp = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hp.IsDead())
        {
            return;
        }
        
        if(target.IsDead())
        {
            sensor.enemyUnits.Remove(target);
          //  anim.SetTrigger("die");
            
        }
        
       if (sensor.enemyUnits.Count > 0)
       {
           target = sensor.enemyUnits[0];
           
            if(Vector3.Distance(transform.position,target.transform.position)<= 4f  && !target.IsDead())
            {
                transform.LookAt(target.transform.position);
                anim.SetTrigger("att");
            }
            else
                agent.SetDestination(target.transform.position);

       }
       else
       {
            target = baseBuilding;
            if(Vector3.Distance(transform.position,target.transform.position)<= 10f && !target.IsDead())
            {
                transform.LookAt(target.transform.position);
                anim.SetTrigger("att");
            }
            else
                agent.SetDestination(target.transform.position);
       }
      
    }
      void Hittt()
    {
        if(target != null && !target.IsDead()){
            target.currentHealth -= _damage;
        }
        else 
            return;
            
    }
   
    
}
