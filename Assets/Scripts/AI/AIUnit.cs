using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIUnit : MonoBehaviour
{
    [SerializeField] float checkRadius = 5f;
  //  [SerializeField] LayerMask enemyLayer;
  [SerializeField] int _damage;

    NavMeshAgent agent;
  [SerializeField] Health target;
    Animator anim;
    [SerializeField] LayerMask layerMask;
    Health baseBuilding;
    Health hp;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        
        anim = GetComponent<Animator>();
        baseBuilding = GameObject.FindGameObjectWithTag("base").GetComponent<Health>();
        target = baseBuilding;
        hp = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] colls = Physics.OverlapSphere(transform.position, 20f, layerMask, QueryTriggerInteraction.Ignore);
        UpdateAnimator();
        if (hp.IsDead())
        {
            return;
        }
        
     /*   if(target.IsDead())
        {
            sensor.enemyUnits.Remove(target);
          //  anim.SetTrigger("die");
            
        }*/
        
       if (colls.Length > 0)
       {
           target = colls[0].GetComponent<Health>();
           
            if(Vector3.Distance(transform.position,target.transform.position)<= 6f  && !target.IsDead())
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
      void KesmeEvent()
    {
        if(target != null && !target.IsDead()){
            target.currentHealth -= _damage;
        }
        else 
            return;
            
    }
    void UpdateAnimator()
    {
        Vector3 v = agent.velocity;
        Vector3 localV = transform.InverseTransformDirection(v);
        float s = localV.z;
        anim.SetFloat("speed", s);
    }


}
