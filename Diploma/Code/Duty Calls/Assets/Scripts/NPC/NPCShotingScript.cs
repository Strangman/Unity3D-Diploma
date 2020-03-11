using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCShotingScript : MonoBehaviour
{
    public PistolGunScript gunScript;

    public bool isAuto;
    public float fireRate;
    private float nextTimeToFire = 0f;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gunScript.bulletInMug <= 0)
        {
            gunScript.StartCoroutine(gunScript.Reload());
        }

        RaycastHit hit;
        if(isAuto)
        {       
            if (Physics.Raycast(gunScript.bulletSpawnPoint.transform.position, transform.forward, out hit, gunScript.range))
            {
                if (hit.transform.TryGetComponent<TargetScript>(out TargetScript target))
                {
                    gunScript.Fire();
                }
            }
        }
        else if(!isAuto)
        {

        }
        if(Physics.Raycast(gunScript.bulletSpawnPoint.transform.position, transform.forward, out hit, gunScript.range))
        {
            if (Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1f / fireRate;

                if (hit.transform.TryGetComponent<TargetScript>(out TargetScript target))
                {
                    gunScript.Fire();
                }
            }  
        } 
    }
}
