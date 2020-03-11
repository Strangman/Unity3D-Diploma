using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public List<GameObject> itemList;

    // Start is called before the first frame update
    void Start()
    {
        int rand = Random.Range(0, itemList.Count);
        Instantiate(itemList[rand], transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
