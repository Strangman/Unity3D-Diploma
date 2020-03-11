using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PistolGunScript : MonoBehaviour
{
    public Transform bulletSpawnPoint;

    public int damage = 10;
    public float range = 100f;

    public ParticleSystem mazleFlash;
    public GameObject hitParticle;

    public int bulletInMug = 7;
    public int allBullets = 21;
    public int mugSize = 7;
    public float reloadSpeed = 3f;
    private bool isReloading = false;
    //
    private float nextTimeToFire = 0f;
    public float fireRate = 15f;
    public bool isAutomatic;
    //
    public float dropForse = 400f;

    public bool hardMode = false;

    private Animator _animator;

    public AudioSource shotSound;
    public AudioSource reloadSound;

    public Vector3 recoil;
    public Vector3 maxRecoil;
    private Tween recoilTween;

    public bool isPlayers;

    public LayerMask layerMask;
    private void OnEnable()
    {
        var gunSpot = transform.parent;
        bulletSpawnPoint = gunSpot.transform.parent.Find("BulletSpawner");


        _animator.enabled = true;
        isReloading = false;

        if(isPlayers)
        {
            SubscribeMethode();
        }
    }

    private void OnDisable()
    {
        UnsubscribeMethode();
    }
    private void Awake()
    {
        _animator = gameObject.GetComponent<Animator>();
        _animator.SetFloat("ReloadingSpeed", 1 / reloadSpeed);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DropWeapon()
    {
        DOTween.KillAll();

        var gunParent = transform.parent;
        var cameraParent = gunParent.transform.parent;
        var playerParent = cameraParent.parent;
        gameObject.transform.SetParent(playerParent.transform.parent);

        _animator.enabled = false;

        var gunRigid = gameObject.AddComponent<Rigidbody>();
        gunRigid.AddForce(transform.forward * dropForse);

        UnsubscribeMethode();

        var gunScript = gameObject.GetComponent<PistolGunScript>();
        gunScript.enabled = false;

        if (isPlayers)
            isPlayers = false;
    }

    public IEnumerator Reload()
    {
        if(isReloading)
        {
            yield return new WaitForSeconds(0f);
        }

        if (allBullets <= 0 || bulletInMug == mugSize)
        {
            yield return new WaitForSeconds(0f);
        }
        else if (allBullets >= mugSize)
        {
            if(!isReloading)
            {
                reloadSound.Play();
            }

            recoilTween.Kill();
            recoilTween = transform.DOLocalMove(Vector3.zero, 2f);

            isReloading = true;
            _animator.SetBool("Reloading", isReloading);
            
            yield return new WaitForSeconds(reloadSpeed);

            isReloading = false;
            _animator.SetBool("Reloading", isReloading);

            if (hardMode)
            {
                bulletInMug = 0;
                bulletInMug = mugSize;
                allBullets -= mugSize;
            }
            else if (!hardMode)
            {
                allBullets -= mugSize - bulletInMug;
                bulletInMug = mugSize;
            }
        }
        else if (allBullets < mugSize)
        {
            if (!isReloading)
            {
                reloadSound.Play();
            }

            recoilTween.Kill();
            recoilTween = transform.DOLocalMove(Vector3.zero, 2f);

            isReloading = true;
            _animator.SetBool("Reloading", isReloading);

            yield return new WaitForSeconds(reloadSpeed);

            isReloading = false;
            _animator.SetBool("Reloading", isReloading);

            var needBullet = mugSize - bulletInMug;

            if (allBullets > needBullet)
            {
                allBullets -= needBullet;
                bulletInMug = mugSize;
            }
            else if (allBullets == needBullet)
            {
                allBullets = 0;
                bulletInMug = mugSize;
            }
            else if (allBullets < needBullet)
            {
                bulletInMug += allBullets; 
                allBullets = 0;
            }
        }

    }

    public void Fire()
    {
        if (isAutomatic)
        {
           

            if (!isReloading && bulletInMug > 0 )
            {
                if (Time.time >= nextTimeToFire)
                {
                    nextTimeToFire = Time.time + 1f / fireRate;

                    shotSound.Play();

                    RecoilTween();

                    bulletInMug--;

                    mazleFlash.Play();

                    RaycastHit hit;

                    if (Physics.Raycast(bulletSpawnPoint.transform.position, transform.forward, out hit, range, layerMask))
                    {
                        if (hit.transform.TryGetComponent<TargetScript>(out TargetScript target))
                        {
                            target.TakeDamage(damage);
                        }
                    }

                    var particle = Instantiate(hitParticle, hit.point, Quaternion.LookRotation(hit.normal));
                    Destroy(particle, 1f);
                }
            }
        }
        else if(!isAutomatic)
        {
            if (!isReloading && bulletInMug > 0)
            {
                shotSound.Play();

                RecoilTween();

                bulletInMug--;

                mazleFlash.Play();

                RaycastHit hit;

                if (Physics.Raycast(bulletSpawnPoint.transform.position, transform.forward, out hit, range, layerMask))
                {
                    if (hit.transform.TryGetComponent<TargetScript>(out TargetScript target))
                    {
                        target.TakeDamage(damage);
                    }
                }
                var particle = Instantiate(hitParticle, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(particle, 1f);
            }
        }
    }

    public string SetBulletText()
    {
        string bulletText = $"{bulletInMug}/{allBullets}";
        return bulletText;
    }

    public float ReloadTime()
    {
        return reloadSpeed;
    }
    private bool GetAutomatic()
    {
        return isAutomatic;
    }
    private void RecoilTween()
    {
        if (transform.localPosition.z >= maxRecoil.z)
        {
            transform.DOPunchPosition(recoil, 0.5f, 1, 0f);
        }

        recoilTween.Kill();
        recoilTween = transform.DOLocalMove(Vector3.zero, 2f).SetDelay(3f);
    }
    public void SubscribeMethode()
    {

        EventManager.ChangeBulletText += SetBulletText;

        EventManager.Fire += Fire;
        EventManager.Reload += Reload;
        EventManager.ReloadTime += ReloadTime;
        EventManager.Automatic += GetAutomatic;
    }
    public void UnsubscribeMethode()
    {

        if (isPlayers)
        {
            bulletSpawnPoint = null;
            EventManager.ChangeBulletText -= SetBulletText;

            EventManager.Fire -= Fire;
            EventManager.Reload -= Reload;
            EventManager.ReloadTime -= ReloadTime;
            EventManager.Automatic -= GetAutomatic;
        }
    }
}
