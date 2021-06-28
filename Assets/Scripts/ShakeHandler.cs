using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeHandler : MonoBehaviour
{
    private Animator _camAnim;

    private int shakeID;
    
    // Start is called before the first frame update
    void Start()
    {
        _camAnim = Camera.main.GetComponent<Animator>();

        shakeID = Animator.StringToHash("shake");
    }

    public void Shake()
    {
        _camAnim.SetTrigger(shakeID);
    }
}
