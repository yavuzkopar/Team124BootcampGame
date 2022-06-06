using RPG.Saving;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitMover : MonoBehaviour, ISaveable
{
    public NavMeshAgent agent;
    Animator _animator;
    [SerializeField] GameObject sprite;
    [SerializeField] Transform trForRotation;
    [SerializeField] Transform raybas;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask enemyLayer;
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
    void OnEnable()
    {
        if (!UnitSelectionController.Singleton.myAllUnits.Contains(this))
            UnitSelectionController.Singleton.myAllUnits.Add(this);
    }
    void OnDisable()
    {
        if (UnitSelectionController.Singleton.myAllUnits.Contains(this))
            UnitSelectionController.Singleton.myAllUnits.Remove(this);
    }
    void Update()
    {


        Physics.Raycast(raybas.position,
            Vector3.down, out RaycastHit hit, 15f, groundLayer);

        trForRotation.LookAt(hit.point);



        UpdateAnimator();


        if (hp.IsDead()) {

            if (UnitSelectionController.Singleton.myAllUnits.Contains(this))
            {
                UnitSelectionController.Singleton.myAllUnits.Remove(this);
            }
            return;
        }

        if (gameObject.CompareTag("Unit") || gameObject.CompareTag("Aslan"))
        {
            AttackBehaviour();

        }
        else if (gameObject.CompareTag("Kunduz"))
        {
            LumberJacking();
        }





    }

    public void LumberJacking()
    {
        /*  if (target == null)
          {
              target = GameObject.FindGameObjectWithTag("Ggg").transform;
              _animator.SetTrigger("idle");
              canAttack = false;
          }*/
        if ((target.CompareTag("Agac")) && target != null)
        {
            //    enemy = target.GetComponent<Health>();
            transform.LookAt(target.position);
            if (Vector3.Distance(transform.position, target.position) <= 6f)
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
                //   target = GameObject.FindGameObjectWithTag("Ggg").transform;
            }
        }
        else
        {
            _animator.SetTrigger("idle");
            canAttack = false;
            //  target = GameObject.FindGameObjectWithTag("Ggg").transform;
        }
    }
    public void AttackBehaviour()
    {

        //  Collider[] enemies = Physics.OverlapSphere(transform.position, 15f,groundLayer);

        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Ggg").transform;
            _animator.SetTrigger("idle");
            canAttack = false;

            /*    Debug.Log("Ben Malim");
                foreach (var item in enemiesInRange)
                {
                    if (item == null || enemy.IsDead())
                    {
                        enemiesInRange.Remove(item);
                    }
                }*/

        }
        if (enemiesInRange.Count > 0)
        {
            target = enemiesInRange[0];
            enemy = target.GetComponent<Health>();
            if (enemy.IsDead())
            {
                enemy.Gerisay();
                enemiesInRange.Remove(target);
            }

        }
        else
            return;

    
        


        if ((target.CompareTag("Enemy") || target.CompareTag("Av")))
        {
            if (target.CompareTag("Enemy"))
            {
                target = enemiesInRange[0];
                enemy = target.GetComponent<Health>();
            }
            else if(target.CompareTag("Av"))
            {
                enemy = target.GetComponent<Health>();
            }
            transform.LookAt(target.position);
            if (Vector3.Distance(transform.position, target.position) <= 10f && !enemy.IsDead())
            {
                canAttack = true;
                //   agent.isStopped = true;
                //   _animator.ResetTrigger("idle");
                _animator.SetTrigger("att");
                Debug.Log("Saldırı animasyonu");
                return;
            }
            else if (enemy.IsDead())
            {
                enemy.Gerisay();
                enemiesInRange.Remove(target);
                if (enemiesInRange.Count > 0)
                {
                    target = GetClosestEnemy(enemiesInRange);
                    //   enemy = target.GetComponent<Health>();
                }
                else
                {
                    target = null;
                    enemy = null;
                }
                return;
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

  
    void UpdateAnimator()
    {
        Vector3 v =agent.velocity;
        Vector3 localV = transform.InverseTransformDirection(v);
        float s = localV.z;
        _animator.SetFloat("speed",s);
    }

    void KesmeEvent()
    {
        if (gameObject.CompareTag("Kunduz") && target != null)
        {
            if (target.gameObject.CompareTag("Agac"))
            {
                Health h = target.GetComponent<Health>();
                if (!h.IsDead())
                {
                    h.currentHealth -= _damage;
                    Economy.singleton.wood += 10;
                    Economy.singleton.woodMiktar.text = Economy.singleton.wood.ToString();
                    Debug.Log("kestim");
                }
                else
                    h.Gerisay();
                
            }
            else
            {
                target = GameObject.FindGameObjectWithTag("Ggg").transform;
            }
        }
        else
            return;
        
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
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Av"))
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
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Av"))
        {
            if (enemiesInRange.Contains(other.transform))
            {
                enemiesInRange.Remove(other.transform);
            }
            else 
                return;
            
        }
    }

    public object CaptureState()
    {
        return new SerializableVector3(transform.position);
    }

    public void RestoreState(object state)
    {
        SerializableVector3 pos = (SerializableVector3)state;
        transform.position = pos.ToVector();
        
    }
}
