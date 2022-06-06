using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnaBina : MonoBehaviour
{
    Health hp;
    public Transform spawnPos;
    public Transform destPoint;
    static AnaBina singleton = null;
    public static AnaBina Singleton { get { return singleton; } }
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject DeadPanel;
    // Start is called before the first frame update
    private void Awake()
    {
        singleton = this;
        Time.timeScale = 1;
    }
    void Start()
    {
        hp = GetComponent<Health>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (hp.IsDead())
        {
            OverGame();
        }
        if(Input.GetKeyDown(KeyCode.Escape) && !pauseMenu.activeSelf)
        {
            PauseMenu();
        }
    }
    void PauseMenu()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }
    void OverGame()
    {
        DeadPanel.SetActive(true);
        Time.timeScale = 0;
    }
}
