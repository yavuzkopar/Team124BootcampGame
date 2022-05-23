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
    [SerializeField] int _damage;
    public Transform target = null; // daha sonra hedefle ilgili yapılacaklar icin dusman ya da bina gibi objeler
    public Health enemy;

    //hedeflenen nesne
    public UnitMover currentTarget;
    public bool isDead = false;

    //bu nesnenin tarafı enemy yada düşman
    public Side side;

    //Nesnenin durumu
    public State currentState;

    //can
   // public int health = 100;
   public bool canAttack = false;
   Health hp;
   public List<Transform> enemiesInRange = new List<Transform>();

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Ggg").transform;
        agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        hp = GetComponent<Health>();
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
  //  public void Hit(int damage){
  //      health -= damage;
  //  }

    //atağı yakala
 //  public void HandleAttack(UnitMover enemy){
 //      StartCoroutine(AttackRoutine());
//
 //      IEnumerator AttackRoutine() {
 //          //Her yarım saniyede kendi sağlığım olduğu ve düşmanın ölmediği sürece ona saldır
 //          while (enemy.health > 0 && health > 0) {
 //              yield return new WaitForSeconds(.5f);
 //              _animator.SetBool("attack", true);
 //           //   enemy.Hit(5);
 //          }
 //      }
 //  }
    void Update()
    {

        //  Battle();

        Physics.Raycast(raybas.position,
            Vector3.down, out RaycastHit hit, 15f, groundLayer);

        trForRotation.LookAt(hit.point);



        UpdateAnimator();
        if(hp.IsDead()){

            if(UnitSelectionController.Singleton.myAllUnits.Contains(this))
            {
                UnitSelectionController.Singleton.myAllUnits.Remove(this);
            }
            return;
        } 
     //   if(target == null) return;

        //    if (!agent.hasPath) return;
        //    if (agent.remainingDistance > agent.stoppingDistance) return;
        if (gameObject.CompareTag("Unit"))
        {
            AttackBehaviour();

        }
        else if (gameObject.CompareTag("Kunduz"))
        {
            LumberJacking();
        }
        //  agent.ResetPath();




    }

    void LumberJacking()
    {
         if ((target.CompareTag("Agac")))
        {
        //    enemy = target.GetComponent<Health>();
            transform.LookAt(target.position);
            if (Vector3.Distance(transform.position, target.position) <= 4f)
            {
                canAttack = true;
                //   agent.isStopped = true;
                _animator.SetTrigger("agackes"); // agac kesme anim
                Debug.Log("Saldırı animasyonu");
            }
            else
            {
                _animator.SetTrigger("idle");
                canAttack = false;
            }
        }
        else
        {
            _animator.SetTrigger("idle");
                canAttack = false;
        }
    }
    private void AttackBehaviour()
    {
        if(target == null)
        {
            if (enemiesInRange.Count>0)
            {
                Debug.Log("Ben Malim");
                foreach (var item in enemiesInRange)
                {
                    if (item == null)
                    {
                        enemiesInRange.Remove(item);
                    }
                }
                target = GetClosestEnemy(enemiesInRange);
            }
            else
                return;
        }
        else 
        {
            
        
        if ((target.CompareTag("Enemy") || target.CompareTag("Av")))
        {
            enemy = target.GetComponent<Health>();
            transform.LookAt(target.position);
            if (Vector3.Distance(transform.position, target.position) <= 4f && !enemy.IsDead())
            {
                canAttack = true;
                //   agent.isStopped = true;
                _animator.ResetTrigger("idle");
                _animator.SetTrigger("att");
                Debug.Log("Saldırı animasyonu");
            }
            else if (enemy.IsDead())
            {
                enemiesInRange.Remove(target);
                if (enemiesInRange.Count >0)
                {
                    target = GetClosestEnemy(enemiesInRange);
                 //   enemy = target.GetComponent<Health>();
                }
                else
                {
                    target = null;
                    enemy = null;
                }
                
            }
            else
            {
                _animator.SetTrigger("idle");
                canAttack = false;
            }
        }

        else
        {
            canAttack = false;
            _animator.SetTrigger("idle");

        }
        }
        
    }
    Transform GetClosestEnemy(List<Transform> enemies)
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        if (enemies.Count > 0)
        {
             foreach (Transform t in enemies)
        {
            float dist = Vector3.Distance(t.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        }
       
        return tMin;
    }

  //  private void Battle()
  //  {
       // if(currentTarget == null || currentTarget.health <= 0) return;
        // Saldırı yapıyorsam kontrol yapma
        //Bir hedefe kitlenmişsem ve bana yakınsa saldır
    //    if (currentTarget && Vector3.Distance(currentTarget.transform.position, transform.position) < 4f)
    //    {
    //        // savaş
    //        if(currentTarget.health > 0 && health > 0)
    //        {
    //            Debug.Log("savaş başladı");
    //        currentState = State.attacking;
    //        agent.ResetPath();
    //        _animator.SetBool("attack", true);
//
    //      //  HandleAttack(currentTarget);
    //        }
    //        
    //        
    //      //  _animator.SetBool("attack", true);
    //    }
    //    //Bir hedefe kitlenmemişsem
    //    else
    //    {
            //Etrafımda olan birimleri göster
            
    /*        Collider[] hitColliders = Physics.OverlapSphere(raybas.position, 10);

            var isEnemyFound = false;
            //Her birimi kontrol et
            foreach (var hitCollider in hitColliders)
            {
                UnitMover unit = hitCollider.GetComponent<UnitMover>();
                //Etrafımdaki birim unit moversa ve benim tarafımda değilse direk düşmanımsa ona kilitlen
                if (unit && unit.side != side)
                {
                    isEnemyFound = true;
                    currentTarget = unit;
                    currentState = State.lock2target;
                    if(gameObject.CompareTag("Enemy")){
                    MoveToPoint(currentTarget.transform.position);
                    }
                }
            }
                if (currentTarget && Vector3.Distance(currentTarget.transform.position, transform.position) < 4f)
                {
        // savaş
                     if(!currentTarget.IsDead())
                     {
                         if(gameObject.CompareTag("Enemy"))
                            {Debug.Log("savaş başladı");}
                     currentState = State.attacking;
                     agent.ResetPath();
                     _animator.SetBool("attack", true);
                     
                    // HandleAttack(currentTarget);
                     }
                     else
                     {
                        _animator.SetBool("attack",false);
                     //   isDead = true;
                     }
                }
            //etrafında düşman yoksa beklemeye devam et
            if (!isEnemyFound)
            {
                // idle
                currentState = State.idle;
                _animator.SetBool("attack", false);
                
            }*/
    //    }
//    }
 //   public bool IsDead()
 //   {
 //  
 //      // _animator.SetBool("attack",false);
 //       return health <= 0;
 //   }

    void UpdateAnimator()
    {
        Vector3 v =agent.velocity;
        Vector3 localV = transform.InverseTransformDirection(v);
        float s = localV.z;
        _animator.SetFloat("speed",s);
    }

    void KesmeEvent()
    {
        Economy.singleton.wood += 10;
        Economy.singleton.woodMiktar.text = Economy.singleton.wood.ToString();
        Debug.Log("kestim");
    }

//animation event
    void Hittt()
    {
        if(enemy != null && !enemy.IsDead() && canAttack){
            enemy.currentHealth -= _damage;
        }
        else 
            return;
            
    }
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (!enemiesInRange.Contains(other.transform))
            {
                enemiesInRange.Add(other.transform);
            }
            else 
                return;
            
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (enemiesInRange.Contains(other.transform))
            {
                enemiesInRange.Remove(other.transform);
            }
            else 
                return;
            
        }
    }
   
}
