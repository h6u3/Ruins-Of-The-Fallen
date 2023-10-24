using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] private CanvasRenderer newGamePopup;

    public void ContinueButton()
    {
        PlayerPrefs.SetInt("new", 0); //0 for false to new game meaning it will continue from last save
        SceneManager.LoadScene("MainTerrain");
    }

    public void NewGameButton()
    {
        newGamePopup.gameObject.SetActive(true);
    }

    public void NewGameConfirmButton()
    {
        PlayerPrefs.SetInt("new", 1); //0 for true to new game meaning it will start new game
        SceneManager.LoadScene("MainTerrain");
    }

    public void NewGameCancel()
    {
        newGamePopup.gameObject.SetActive(false);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
