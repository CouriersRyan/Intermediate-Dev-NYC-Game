using TMPro.Examples;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float moveSpd = 5f;

    [SerializeField] private ScoreHandler crashScore;
    [SerializeField] private ScoreHandler pointScore;

    [SerializeField] private GameObject book;

    private Animator _anim;
    private Rigidbody2D _rb;

    private string _currentAnim;
    private Vector2 _prevVel;
    private float _prevVelLargestAxis = 0;


    [SerializeField] private int baseToBoost = 45;
    private int _toBoost;

    private float _spdMultiplier = 1;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();

        _toBoost = baseToBoost;
    }
    
    
    void FixedUpdate()
    {
        _currentAnim = "SharkIdle";
        
        if (Input.GetKey(KeyCode.UpArrow))
        {
            _rb.velocity = new Vector2(_rb.velocity.x, moveSpd * _spdMultiplier);
            _currentAnim = "SharkMoveUp";
        } 
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            _rb.velocity = new Vector2(_rb.velocity.x, -moveSpd * _spdMultiplier);
            _currentAnim = "SharkMoveDown";
        }
        
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            _rb.velocity = new Vector2(-moveSpd * _spdMultiplier, _rb.velocity.y);
            _currentAnim = "SharkMoveLeft";
        }
        else if(Input.GetKey(KeyCode.RightArrow))
        {
            _rb.velocity = new Vector2(moveSpd * _spdMultiplier, _rb.velocity.y);
            _currentAnim = "SharkMoveRight";
        }
        
        if (Mathf.Sign(_prevVel.y) != Mathf.Sign(_rb.velocity.y) || Mathf.Sign(_prevVel.x) != Mathf.Sign(_rb.velocity.x))
        {
            _toBoost = baseToBoost;
        }
        _prevVel = _rb.velocity;
        _prevVelLargestAxis = Mathf.Abs(_prevVel.x) >= Mathf.Abs(_prevVel.y)
            ? Mathf.Abs(_prevVel.x)
            : Mathf.Abs(_prevVel.y);

        if (_prevVelLargestAxis >= moveSpd)
        {
            _toBoost--;
        }

        if (_prevVelLargestAxis <= 0.5 * moveSpd)
        {
            _toBoost = baseToBoost;
            _spdMultiplier = 1;
        }

        if (_toBoost <= 0)
        {
            _spdMultiplier = 1.5f;
        }
        
        _anim.Play(_currentAnim);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        _toBoost = baseToBoost;
        _spdMultiplier = 0.5f;
        if (_prevVelLargestAxis >= 1.5f * moveSpd && other.collider.CompareTag("Obstacle"))
        {
            crashScore.UpdateScore(1);
            ReleaseBooks(3, transform);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Scoring"))
        {
            pointScore.UpdateScore(1);
            Destroy(other.gameObject);
        }
    }

    public void ReleaseBooks(int n, Transform transformTemp)
    {
        for (int i = 0; i < n; i++)
        {
            Instantiate(book, transformTemp.position, Quaternion.identity);
        }
    }
}