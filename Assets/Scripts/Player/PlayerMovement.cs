using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform _wallCheck;
    [SerializeField] private float _wallSlideSpeed;
    [SerializeField] private Vector2 _jumpVelocity;

    private Rigidbody2D _rigidbody2d;
    private float _wallCheckRadious = 0.1f;
    private int _jumpDirectionX = 1;
    private int _maxJumpCount = 2;
    private int _currentJumpCount;
    private float _currentJumpTime;
    private float _maxJumpTime = 0.5f;

    private void OnEnable()
    {
        _rigidbody2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Jump();
    }

    private void FixedUpdate()
    {
        if (Physics2D.OverlapCircle(_wallCheck.position, _wallCheckRadious)?.GetComponent<Wall>())
        {
            _currentJumpCount = 0;
            _rigidbody2d.velocity = new Vector2(_rigidbody2d.velocity.x, Mathf.Clamp(_rigidbody2d.velocity.y, -_wallSlideSpeed, float.MaxValue));
        }
    }

    private void Jump()
    {
        Vector2 velocity;
        
        if (Input.GetMouseButton(0) && _currentJumpTime < _maxJumpTime && _currentJumpCount < _maxJumpCount)
        {
            _currentJumpTime += Time.deltaTime;
            velocity = new Vector2(_jumpDirectionX * _jumpVelocity.x, _jumpVelocity.y);
            _rigidbody2d.velocity = velocity;
           
        }

        if (Input.GetMouseButtonUp(0) && _currentJumpCount < _maxJumpCount)
        {
            _currentJumpTime = 0;
            _currentJumpCount++;
            Flip();
        }
    }

    private void Flip()
    {
        var scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        _jumpDirectionX *= -1;
    }
}


