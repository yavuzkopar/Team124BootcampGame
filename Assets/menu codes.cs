using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  UnityEngine.UI;
using UnityEngine.SceneManagement;

public class menucodes : MonoBehaviour
{
    
    public void StartNewGame()
    {
        SceneManager.LoadScene("ilk sahnenin numarasý yazýlacak");
        
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene("Kalýnan sahnenin numarasý yazýlacak ");
        //Application.LoadLevel("kalýnan level yazýlacak ");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
