using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiWeapon : MonoBehaviour
{
    [SerializeField] private List<GameObject> _weaponInventory;

    public int currentWeaponIndex = 0;
    private int _currentWeapon;
    private int maxWeapons = 2;
    private void Awake()
    {
        EventManager.SelectWeapon1 += SelectWeapon1;
        EventManager.SelectWeapon2 += SelectWeapon2;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            if(_weaponInventory.Count >0)
            {
                var weapon = _weaponInventory[currentWeaponIndex].GetComponent<PistolGunScript>();
                weapon.DropWeapon();
                RemoveWeapon();
            }
        }
    }
    public void AddWeapon(GameObject weapon)
    {
        _weaponInventory.Add(weapon);

        if(_weaponInventory.Count > 1)
        {
            _weaponInventory[currentWeaponIndex].SetActive(false);
            currentWeaponIndex = ReversWeapons();
        }
    }
    public void RemoveWeapon()
    {
        _weaponInventory.RemoveAt(currentWeaponIndex);
        _currentWeapon--;
    }
    public bool isCanAddWeapon()
    {
        return _weaponInventory.Count == maxWeapons ? false : true;
    }
    private int ReversWeapons()
    {
        return currentWeaponIndex == 0 ? 1 : 0;
    }

    private void SelectWeapon1()
    {
        if (_weaponInventory.Count < 1)
            return;

        _weaponInventory[0].SetActive(true);

        if (_weaponInventory.Count == 2)
            _weaponInventory[1].SetActive(false);

        currentWeaponIndex = 0;
    }
    private void SelectWeapon2()
    {
        if (_weaponInventory.Count < 2)
            return;
        _weaponInventory[0]?.SetActive(false);

        if (_weaponInventory.Count == 2)
            _weaponInventory[1].SetActive(true);

        currentWeaponIndex = 1;
    }
}
