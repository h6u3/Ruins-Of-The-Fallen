using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemPopUp : MonoBehaviour
{
    public CanvasRenderer PopUpCanvasToggle;
    public TextMeshProUGUI PopUpText;

    private Coroutine currentCoroutine;

    public void ShowPopUp(string itemText)
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }

        PopUpText.text = "You picked up\n" + itemText;
        PopUpCanvasToggle.gameObject.SetActive(true);
        currentCoroutine = StartCoroutine(HidePopUp());
    }

    private IEnumerator HidePopUp()
    {
        yield return new WaitForSeconds(3f);
        PopUpCanvasToggle.gameObject.SetActive(false);
    }
}
