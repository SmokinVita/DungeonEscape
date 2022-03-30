using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField]
    protected GameObject diamondPrefab;

    [SerializeField]
    protected int health;
    [SerializeField]
    protected float speed;
    [SerializeField]
    protected int gems;

    [SerializeField]
    protected Transform pointA, pointB, currentTarget;

    protected Animator anim;
    protected SpriteRenderer sprite;

    protected bool isHit = false;
    protected bool isDead = false;
    
    protected Player player;

    public virtual void Init()
    {
        anim = GetComponentInChildren<Animator>();
        if (anim == null)
        {
            Debug.LogError($"{this.name} is missing Animator");
        }

        sprite = GetComponentInChildren<SpriteRenderer>();
        if (sprite == null)
        {
            Debug.LogError($"{this.name} is missing Sprite Renderer");
        }

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if(player == null)
        {
            Debug.LogError("Player is null!");
        }

        currentTarget = pointA;
    }


    private void Start()
    {
        Init();
    }

    public virtual void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") && anim.GetBool("InCombat") == false)
            return;

        if (isDead == true)
            return;

        Movement();
    }

    public virtual void Movement()
    {
        SpriteFlip();

        if (transform.position == pointA.position)
        {
            anim.SetTrigger("Idle");
            currentTarget = pointB;
        }
        else if (transform.position == pointB.position)
        {
            anim.SetTrigger("Idle");
            currentTarget = pointA;
        }

        if (isHit == false)
        {
            transform.position = Vector2.MoveTowards(transform.position, currentTarget.position, speed * Time.deltaTime);
        }
        else if (isHit == true)
        {
            
            float distance = Vector2.Distance(transform.position, player.transform.position);
            if (distance >= 2f)
            {
                isHit = false;
                anim.SetBool("InCombat", false);
            }
            Vector2 direction = player.transform.localPosition - transform.localPosition;
            if (direction.x > 0f && anim.GetBool("InCombat") == true)
            {
                sprite.flipX = false;
            }
            else if (direction.x < 0f && anim.GetBool("InCombat") == true)
            {
                sprite.flipX = true;
            }
        }
    }

    private void SpriteFlip()
    {
        if (currentTarget == pointA)
        {
            sprite.flipX = true;
        }
        else if (currentTarget == pointB)
        {
            sprite.flipX = false;
        }
    }
}
