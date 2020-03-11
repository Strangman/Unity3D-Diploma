using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDie : MonoBehaviour, IDie
{
    public void Die()
    {
        Destroy(gameObject);
    }
}
