using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    [SerializeField] private CanvasRenderer quitGamePopup;
    private Keyboard keyboard;
    private KeyboardState keyState1;
    private KeyboardState keyState2;

    public void ContinueButton()
    {
        keyboard = InputSystem.GetDevice<Keyboard>();
        keyState1 = new KeyboardState();
        keyState2 = new KeyboardState();
        keyState1.Press(Key.A);
        keyState2.Release(Key.A);
        InputSystem.QueueStateEvent(keyboard, keyState1);
        InputSystem.QueueStateEvent(keyboard, keyState2);
        gameObject.SetActive(false);
    }

    public void QuitGameConfirmButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGameCancel()
    {
        quitGamePopup.gameObject.SetActive(false);
    }

    public void QuitButton()
    {
        quitGamePopup.gameObject.SetActive(true);
    }
}
