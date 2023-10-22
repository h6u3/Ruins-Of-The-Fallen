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
        Application.Quit(); //temp until i make a main menu
        //open main menu
    }
}
