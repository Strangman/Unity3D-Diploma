using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

public class MainMenuButtonScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Vector3 onMouseEnterScale;
    private Vector3 onMouseExitScale;


    public float animationDyration;

    private IButtonInteract buttonInteract;

    public AudioSource buttonClickSound;

    private void Awake()
    {
        onMouseExitScale = transform.localScale;
        buttonInteract = GetComponent<IButtonInteract>();
    }

    private void OnEnable()
    {
        transform.localScale = onMouseExitScale;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(onMouseEnterScale, animationDyration);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(onMouseExitScale, animationDyration);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        buttonClickSound.Play();

        if (buttonInteract != null)
        {
            buttonInteract.InteractOnClick();
        }
    }
}
