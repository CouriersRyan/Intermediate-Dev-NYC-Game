using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float moveSpd = 5f;

    [SerializeField] private ScoreHandler _crashScore;

    private Animator _anim;
    private Rigidbody2D _rb;

    private string _currentAnim;
    private float _prevSpd;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
    }
    
    
    void FixedUpdate()
    {
        _currentAnim = "SharkIdle";
        
        if (Input.GetKey(KeyCode.UpArrow))
        {
            _rb.velocity = new Vector2(_rb.velocity.x, moveSpd);
            _currentAnim = "SharkMoveUp";
        } 
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            _rb.velocity = new Vector2(_rb.velocity.x, -moveSpd);
            _currentAnim = "SharkMoveDown";
        }
        
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            _rb.velocity = new Vector2(-moveSpd, _rb.velocity.y);
            _currentAnim = "SharkMoveLeft";
        }
        else if(Input.GetKey(KeyCode.RightArrow))
        {
            _rb.velocity = new Vector2(moveSpd, _rb.velocity.y);
            _currentAnim = "SharkMoveRight";
        }

        _prevSpd = _rb.velocity.magnitude;
        _anim.Play(_currentAnim);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (_prevSpd >= 1 * moveSpd)
        {
            _crashScore.UpdateScore(1);
        }
    }
}
