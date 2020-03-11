using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDie : MonoBehaviour, IDie
{
    public string sceneName;
    public void Die()
    {
        SceneManager.LoadScene(sceneName);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
