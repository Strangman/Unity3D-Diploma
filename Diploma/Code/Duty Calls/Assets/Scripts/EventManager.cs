using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    [SerializeField] private Text _bulletText;
    [SerializeField] private Text _HPText;

    public delegate void VoidDelegate();
    public static event VoidDelegate Fire;
    public static event VoidDelegate Interact;
    public static event VoidDelegate SelectWeapon1;
    public static event VoidDelegate SelectWeapon2;

    public delegate IEnumerator ReloadDelegate();
    public static event ReloadDelegate Reload;

    public delegate float GetFloat();
    public static event GetFloat ReloadTime;

    public delegate string OnBulletChange();
    public static event OnBulletChange ChangeBulletText;

    public delegate bool GetBool();
    public static event GetBool Automatic;

    public bool automatic;

    private void Awake()
    {
        TargetScript.hpText += SetHPText;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!automatic)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Fire?.Invoke();
                SetBulletText();
            }
        }
        else if (automatic)
        {
            if (Input.GetButton("Fire1"))
            {
                Fire?.Invoke();
                SetBulletText();
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if(Reload!= null)
            {
                StartCoroutine(Reload?.Invoke());
                StartCoroutine(WaitingTime(ReloadTime() + 0.25f, SetBulletText));
            }
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            Interact?.Invoke();
            if(Automatic != null)
            {
                automatic = Automatic();
            }
            SetBulletText();
        }

        if(Input.GetKeyDown(KeyCode.G))
        {
            SetDropBulletText();
        }

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectWeapon1?.Invoke();
            if (Automatic != null)
            {
                automatic = Automatic();
            }
            SetBulletText();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectWeapon2?.Invoke();
            if (Automatic != null)
            {
                automatic = Automatic();
            }
            SetBulletText();
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MaineMenu");
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void SetBulletText()
    {
        _bulletText.text = ChangeBulletText?.Invoke();
    }
    public void SetDropBulletText()
    {
        _bulletText.text = $"0/0";
    }
    public void SetHPText(int hp)
    {
        _HPText.text = $"HP:{hp}";
    }
    public IEnumerator WaitingTime(float time, Action methode)
    {
        yield return new WaitForSeconds(time);
        methode?.Invoke();
    }
}
