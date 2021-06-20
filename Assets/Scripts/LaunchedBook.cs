using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Serialization;

public class LaunchedBook : MonoBehaviour
{
    private float _tempX = 0;
    private float _tempY = 0;

    private float _force;
    
    [SerializeField] private float height = 2f;
    [SerializeField] private float gravity = 0.01f;

    [SerializeField] private float forceMax = 50;
    [SerializeField] private float forceMin = 40;

    private Rigidbody2D rb;

    private Transform childBook;

    [SerializeField] private GameObject bookShadow;
    
    // Start is called before the first frame update
    void Start()
    {
        //finds the index for the child of this object
        childBook = gameObject.transform.GetChild(0);
        
        //gets the Rigidbody2D component attached to this gameObject
        rb = GetComponent<Rigidbody2D>();
        
        //randomly selects a target x and y
        _tempX = Random.Range(-8, 8);
        _tempY = Random.Range(2, -4.5f);
        
        //creates a direction vector that points from the current position to the random position
        var pos = transform.position;
        var dir = new Vector2(_tempX - pos.x, _tempY - pos.y);
        
        //random force magnitude
        _force = Random.Range(forceMin, forceMax);
        
        //adds a force on the gameObject pointing towards the random position
        rb.AddForce(dir * _force);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (height <= 0)
        {
            var temp = Instantiate(bookShadow, transform);
            childBook.SetParent(temp.transform);
            childBook.transform.position = Vector2.zero;
            temp.transform.parent = null;
            Destroy(gameObject);
        }
        else
        {
            height -= gravity;
        }

        childBook.position = new Vector2(transform.position.x, transform.position.y + height);
    }
}
