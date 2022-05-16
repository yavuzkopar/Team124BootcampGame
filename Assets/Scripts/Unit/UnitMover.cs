using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitMover : MonoBehaviour
{
    public NavMeshAgent agent;
    Animator _animator;
    [SerializeField] GameObject sprite;
    [SerializeField] Transform trForRotation;
    [SerializeField] Transform raybas;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float _damage;
    public Transform target = null; // daha sonra hedefle ilgili yapılacaklar icin dusman ya da bina gibi objeler

    //hedeflenen nesne
    public UnitMover currentTarget;

    //bu nesnenin tarafı enemy yada düşman
    public Side side;

    //Nesnenin durumu
    public State currentState;

    //can
    public int health = 100;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Ggg").transform;
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
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>

    //hasar alma durumu
    public void Hit(int damage){
        health -= damage;
    }

    //atağı yakala
    public void HandleAttack(UnitMover enemy){
        StartCoroutine(AttackRoutine());

        IEnumerator AttackRoutine() {
            //Her yarım saniyede kendi sağlığım olduğu ve düşmanın ölmediği sürece ona saldır
            while (enemy.health > 0 && health > 0) {
                yield return new WaitForSeconds(.5f);
                enemy.Hit(5);
            }
        }
    }
    void Update()
    {
        // Saldırı yapıyorsam kontrol yapma
            //Bir hedefe kitlenmişsem ve bana yakınsa saldır
            if (currentTarget && Vector3.Distance(currentTarget.transform.position, transform.position) < 4f) {
                // savaş
                Debug.Log("savaş başladı");
                currentState = State.attacking;
                agent.ResetPath();
                HandleAttack(currentTarget);
            }
            //Bir hedefe kitlenmemişsem
            else {
                //Etrafımda olan birimleri göster
                Collider[] hitColliders = Physics.OverlapSphere(raybas.position, 10);

                var isEnemyFound = false;
                //Her birimi kontrol et
                foreach (var hitCollider in hitColliders)
                {
                    UnitMover unit = hitCollider.GetComponent<UnitMover>();
                    //Etrafımdaki birim unit moversa ve benim tarafımda değilse direk düşmanımsa ona kilitlen
                    if (unit && unit.side != side) {
                        isEnemyFound = true;
                        currentTarget = unit;
                        currentState = State.lock2target;
                        MoveToPoint(currentTarget.transform.position);
                    }
                }
                //etrafında düşman yoksa beklemeye devam et
                if (!isEnemyFound) {
                    // idle
                    currentState = State.idle;
                }
            }
               
        
        Physics.Raycast(raybas.position,
            Vector3.down,out RaycastHit hit,15f,groundLayer);
      
     trForRotation.LookAt(hit.point);
        
   
        
        UpdateAnimator();
         
        if(!agent.hasPath) return;
        if(agent.remainingDistance > agent.stoppingDistance) return;
        
        if ((target.CompareTag("Enemy") || target.CompareTag("Av") )&& Vector3.Distance(transform.position,target.position)<= 3f)
        {
            transform.LookAt(target.position);
         //   agent.isStopped = true;
            _animator.SetBool("attack",true);
            Debug.Log("Saldırı animasyonu");
            
        }
        
        else if(target.CompareTag("Ggg"))
        {
            _animator.SetBool("attack",false);
            
        }
        agent.ResetPath();


       
        
    }
    

    void UpdateAnimator()
    {
        Vector3 v =agent.velocity;
        Vector3 localV = transform.InverseTransformDirection(v);
        float s = localV.z;
        _animator.SetFloat("speed",s);
    }

//animation event
    void Hittt()
    {
        if(target.GetComponent<Health>() != null)
            target.GetComponent<Health>().currentHealth -= _damage;
    }
   
}
