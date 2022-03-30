using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour, IDamageable
{
    //variable for amount of diamonds
    [SerializeField]
    private int _diamondAmount;

    [SerializeField]
    private float _speed;
    [SerializeField]
    public int Health { get; set; }

    //jump force
    [SerializeField]
    private float _jumpForce = 5f;
    [SerializeField]
    private float _raycastDistance = .5f;
    [SerializeField]
    private LayerMask _layermask;
    private bool isJumping = false;
    private bool isDead = false;

    private Rigidbody2D _rb2D;
    private SpriteRenderer _playerSprite;
    private PlayerAnimation _playerAnim;

    private SpriteRenderer _swordArcSprite;

    void Start()
    {
        UIManager.Instance.UpdateGemCount(_diamondAmount);

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

        Health = 4;
    }

    void Update()
    {
        if (isDead == true)
            return;

        Movement();
        Attack();

        Debug.DrawRay(transform.position, Vector2.down * _raycastDistance, Color.green);
    }

    private void Movement()
    {
        float horizontalInput = CrossPlatformInputManager.GetAxisRaw("Horizontal");// Input.GetAxisRaw("Horizontal");

        if (isJumping == true)
            IsGrounded();

        _playerAnim.RunAnimation(horizontalInput);

        Flip(horizontalInput);



        if ((Input.GetKeyDown(KeyCode.Space) || CrossPlatformInputManager.GetButtonDown("B_Button")) && IsGrounded())
        {
            _playerAnim.Jump(true);
            _rb2D.velocity = new Vector2(_rb2D.velocity.x, _jumpForce);
            isJumping = true;
        }

        _rb2D.velocity = new Vector2(horizontalInput * _speed, _rb2D.velocity.y);
    }

    private void Attack()
    {
        if ((Input.GetMouseButtonDown(0) || CrossPlatformInputManager.GetButtonDown("A_Button")) && IsGrounded())
        {
            _playerAnim.RegAttack();
        }
    }

    public void Damage()
    {
        if (isDead == true)
            return;

        Debug.Log("Player Damage()");
        Health--;
        UIManager.Instance.UpdateLives(Health);

        if(Health <= 0)
        {
            _playerAnim.PlayerDeath();
            isDead = true;
        }

        //remove 1 health
        //update UI Display
        //check for dead
        //play death animation


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

    public void AddDiamonds(int diamonds)
    {
        _diamondAmount += diamonds;
        UIManager.Instance.UpdateGemCount(_diamondAmount);
    }

    public int CheckDiamonAmount()
    {
        return _diamondAmount;
    }

    public void SubtractDiamonds(int diamondsLost)
    {
        _diamondAmount -= diamondsLost;
        UIManager.Instance.UpdateGemCount(_diamondAmount);
    }

    private bool IsGrounded()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, _raycastDistance, _layermask);
        if (hitInfo.collider == true)
        {
            isJumping = false;
            _playerAnim.Jump(false);
        }

        return hitInfo.collider;
    }
}
