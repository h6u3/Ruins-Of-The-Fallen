using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathUIScript : MonoBehaviour
{
    public void ContinueButton()
    {
        SceneManager.LoadScene("MainTerrain");
    }

    public void QuitButton()
    {
        SceneManager.LoadScene("MainMenu"); 
    }
}
