using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectObject : MonoBehaviour
{
    [SerializeField] private float _interactDistance;
    [SerializeField] private string _compareTag = "Selectable";
    [SerializeField] private GameObject _gunSpot;

    private MultiWeapon _multiWeapon;

    private void Awake()
    {
        _multiWeapon = gameObject.GetComponent<MultiWeapon>();
        EventManager.Interact += Interact;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        
    }
    private void Interact()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, _interactDistance))
        {
            var selected = hit.transform;
            if (selected.CompareTag(_compareTag))
            {
                if (selected.TryGetComponent<PistolGunScript>(out PistolGunScript gunScript))
                {
                    if (!_multiWeapon.isCanAddWeapon())
                        return;

                    if(_gunSpot != null)
                    {
                        selected.SetParent(_gunSpot.transform);

                    }
                    
                    selected.localPosition = Vector3.zero;
                    selected.localRotation = Quaternion.Euler(Vector3.zero);
                    var ridBody = selected.GetComponent<Rigidbody>();
                    Destroy(ridBody);
                    gunScript.isPlayers = true;
                    gunScript.enabled = true;

                    _multiWeapon.AddWeapon(selected.gameObject);
                }
            }
        }
    }
}
