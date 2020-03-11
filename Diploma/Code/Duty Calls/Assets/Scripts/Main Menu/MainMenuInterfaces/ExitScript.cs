using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitScript : MonoBehaviour, IButtonInteract
{
    public void InteractOnClick()
    {
        Application.Quit();
    }
}
