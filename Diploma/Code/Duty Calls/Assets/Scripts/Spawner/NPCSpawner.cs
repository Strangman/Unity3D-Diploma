using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    public float spawnTime;
    public List<GameObject> npcList;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(NPCSpawn());
    }
    public IEnumerator NPCSpawn()
    {
        int rand = Random.Range(0, npcList.Count);
        Instantiate(npcList[rand], transform.position, Quaternion.identity);

        yield return new WaitForSeconds(spawnTime);

        StartCoroutine(NPCSpawn());
    }
}
