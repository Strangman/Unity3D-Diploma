using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public GameObject EndGameMenu;
    private int _enemyNumber = 13;
    public void Awake()
    {
        TargetScript.EndGame += EnemyDeath;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void EnemyDeath()
    {
        _enemyNumber -= 1;


        if(_enemyNumber <= 0)
        {
            PopUpEndGameMenu();
        }
    }

    private void PopUpEndGameMenu()
    {
        EndGameMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }
}
