using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCloseWindow : MonoBehaviour, IButtonInteract
{
    public GameObject openWindow;
    public GameObject closeWindow;

    public void InteractOnClick()
    {
        closeWindow.SetActive(false);
        openWindow.SetActive(true);
    }
}
