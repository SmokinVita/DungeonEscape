using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    //jump force
    [SerializeField]
    private float _jumpForce = 5f;
    [SerializeField]
    private float _raycastDistance = .5f;
    [SerializeField]
    private LayerMask _layermask;
    private bool _isGrounded = false;

    private Rigidbody2D _rb2D;
    private SpriteRenderer _playerSprite;
    private PlayerAnimation _playerAnim;

    private SpriteRenderer _swordArcSprite;

    void Start()
    {
        _rb2D = GetComponent<Rigidbody2D>();
        if (_rb2D == null)
        {
            Debug.LogError("Rigidbody 2D is not on Player! Is Null!!");
        }

        _playerSprite = GetComponentInChildren<SpriteRenderer>();
        if (_playerSprite == null)
        {
            Debug.LogError("Sprite Renderer is null on Child");
        }

        _playerAnim = GetComponent<PlayerAnimation>();
        if (_playerAnim == null)
        {
            Debug.LogError("Player Animation is null!");
        }

        _swordArcSprite = transform.GetChild(1).GetComponent<SpriteRenderer>();
        if (_swordArcSprite == null)
        {
            Debug.LogError("Sword Arc Sprite Renderer is null!");
        }
    }

    void Update()
    {
        Movement();
        Attack();

        Debug.DrawRay(transform.position, Vector2.down * _raycastDistance, Color.green);
    }

    private void Movement()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        _isGrounded = IsGrounded();

        _playerAnim.RunAnimation(horizontalInput);

        Flip(horizontalInput);



        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            _playerAnim.Jump(true);
            StartCoroutine(JumpResetRoutine());
            _rb2D.velocity = new Vector2(_rb2D.velocity.x, _jumpForce);
        }

        _rb2D.velocity = new Vector2(horizontalInput * _speed, _rb2D.velocity.y);
    }

    private void Attack()
    {
        if (Input.GetMouseButtonDown(0) && IsGrounded())
        {
            _playerAnim.RegAttack();
        }
    }

    private void Flip(float movement)
    {
        if (movement > 0)
        {
            _playerSprite.flipX = false;

            _swordArcSprite.flipX = false;
            _swordArcSprite.flipY = false;
            Vector2 newPos = _swordArcSprite.transform.localPosition;
            newPos.x = 1.01f;
            _swordArcSprite.transform.localPosition = newPos;
        }
        else if (movement < 0)
        {
            _playerSprite.flipX = true;

            _swordArcSprite.flipX = true;
            _swordArcSprite.flipY = true;
            Vector2 newPos = _swordArcSprite.transform.localPosition;
            newPos.x = -1.01f;
            _swordArcSprite.transform.localPosition = newPos;
        }
    }

    private bool IsGrounded()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, _raycastDistance, _layermask);
        return hitInfo.collider;
    }

    IEnumerator JumpResetRoutine()
    {
        yield return new WaitForSeconds(1f);
        _playerAnim.Jump(false);
    }
}
