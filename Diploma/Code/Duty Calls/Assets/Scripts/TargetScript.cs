using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour
{
    public bool isPlayer;

    public delegate void EndGameDelegate();
    public static event EndGameDelegate EndGame;

    public int hp = 50;

    private IDie _die;

    public delegate void HPText(int hp);
    public static event HPText hpText;
    private void Awake()
    {
        _die = GetComponent<IDie>();

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;

        if(isPlayer)
        {
            hpText?.Invoke(hp);
        }

        if(hp <= 0)
        {
            _die.Die();
        }
    }

}
