using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  UnityEngine.UI;
using UnityEngine.SceneManagement;

public class menucodes : MonoBehaviour
{
    
    public void StartNewGame()
    {
        SceneManager.LoadScene("ilk sahnenin numaras� yaz�lacak");
        
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene("Kal�nan sahnenin numaras� yaz�lacak ");
        //Application.LoadLevel("kal�nan level yaz�lacak ");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
