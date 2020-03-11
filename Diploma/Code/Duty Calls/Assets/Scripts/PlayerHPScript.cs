using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHPScript : MonoBehaviour
{
    [SerializeField] private int _hp;
    [SerializeField] private int _maxHP;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Decreace(int damage)
    {
        _hp -= damage;
        _hp = Mathf.Clamp(_hp, 0, _maxHP);

        if(_hp == 0)
        {

        }
    }

    public int GetHP()
    {
        return _hp;
    }

    public void Heal(int heal)
    {
        _hp += heal;
        _hp = Mathf.Clamp(_hp, 0, _maxHP);
    }
    
}
