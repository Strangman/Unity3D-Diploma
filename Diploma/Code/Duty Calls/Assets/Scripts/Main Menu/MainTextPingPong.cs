using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MainTextPingPong : MonoBehaviour
{
    public Vector3 scaleSize;
    public float pingpongDuration;
    // Start is called before the first frame update
    void Start()
    {
        transform.DOScale(scaleSize, pingpongDuration).SetLoops(-1, LoopType.Yoyo);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
