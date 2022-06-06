using System.Collections;
using System.Collections.Generic;
using RPG.Saving;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour,ISaveable
{
    public float maxHealth;
    public float currentHealth;

    [SerializeField] Slider slider;
    [SerializeField] Transform canvas;
    [SerializeField] bool decreaseHealth;

    void Start()
    {
        currentHealth = maxHealth;    
    }
   
    // Update is called once per frame
    void Update()
    {
        if (IsDead())
        {
            gameObject.layer = LayerMask.NameToLayer("DeadLayer");
            gameObject.tag = "Dead";
            if (!gameObject.CompareTag("Agac"))
                GetComponent<Animator>().SetTrigger("die"); // die
            Economy.singleton.UpdatePopulation();
            return;
        }
        canvas.transform.forward = Camera.main.transform.forward;
        if (decreaseHealth)
        {
            currentHealth -= 0.5f * Time.deltaTime;
        }
        
       currentHealth = Mathf.Clamp(currentHealth,0,maxHealth);
        slider.value = currentHealth / maxHealth;
    }
     public bool IsDead()
    {
   
       // _animator.SetBool("attack",false);
        return currentHealth <= 0;
    }
    public void Gerisay()
    {
        Invoke("Die", 2f);
    }
    void Die()
    {
        gameObject.SetActive(false);
    }
   public Dictionary<string,object> data = new Dictionary<string, object>();
    public object CaptureState()
    {
        
        
        return currentHealth;
    }

    public void RestoreState(object state)
    {
       
        currentHealth = (float) state;
        
    }
}
