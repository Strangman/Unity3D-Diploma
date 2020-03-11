using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour, IButtonInteract
{
    public string sceneName;
    public void InteractOnClick()
    {
        SceneManager.LoadScene(sceneName);
    }

}
