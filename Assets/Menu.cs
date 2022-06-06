using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    [SerializeField] GameObject gecisEkran;
    public static bool load = false;
    [SerializeField] Slider slider;
    public void LoadScene(int index)
    {
        // SceneManager.LoadScene(1);
        //   SceneManager.LoadSceneAsync(index);
        //  gecisEkran.SetActive(true);
        StartCoroutine(LoadAsc(index));
        load = false;
    }
    public void Continue()
    {
        Time.timeScale = 1;
    }
    public void LoadLastScene(int index)
    {
        // SceneManager.LoadSceneAsync(index);
        StartCoroutine(LoadAsc(index));
        load = true;
    }
    IEnumerator LoadAsc(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        gecisEkran.SetActive(true);

        while(!operation.isDone)
        {
            float progres = Mathf.Clamp01(operation.progress / .9f);

            slider.value = progres;
            yield return null;
        }
    }


    public void Exit()
    {
        Application.Quit();
    }
}
        
    

