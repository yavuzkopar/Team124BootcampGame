using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;

    [SerializeField] Slider slider;
    [SerializeField] Transform canvas;

    void Start()
    {
        currentHealth = maxHealth;    
    }

    // Update is called once per frame
    void Update()
    {
        canvas.transform.forward = Camera.main.transform.forward;
        currentHealth -= 0.5f * Time.deltaTime;
       currentHealth = Mathf.Clamp(currentHealth,0,currentHealth);
        slider.value = currentHealth / maxHealth;
    }
}
