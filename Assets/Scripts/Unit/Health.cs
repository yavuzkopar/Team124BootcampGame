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
    void Things()
        {
            GameObject obj = Resources.Load<GameObject>(this.gameObject.name);
        }

    // Update is called once per frame
    void Update()
    {
        if (IsDead())
        {
            gameObject.layer = LayerMask.NameToLayer("DeadLayer");
            GetComponent<Animator>().SetTrigger("die"); // die
            Economy.singleton.UpdatePopulation();
            DestroyImmediate(gameObject);
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
   public Dictionary<string,object> data = new Dictionary<string, object>();
    public object CaptureState()
    {
        
        data["Health"] = currentHealth;
        data["instance"] = GetComponent<SaveableEntity>().GetUniqueIdentifier();
        gameObject.name =(string) data["instance"];
        return data;
    }

    public void RestoreState(object state)
    {
        Dictionary<string,object> dat = ( Dictionary<string,object>) state;
        currentHealth = (float) dat["Health"];
        
    }
}
