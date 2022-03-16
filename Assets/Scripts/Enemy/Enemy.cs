using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
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

    public virtual void Init()
    {
        anim = GetComponentInChildren<Animator>();
        if (anim == null)
        {
            Debug.Log($"{this.name} is missing Animator");
        }

        sprite = GetComponentInChildren<SpriteRenderer>();
        if (sprite == null)
        {
            Debug.Log($"{this.name} is missing Sprite Renderer");
        }
    }

    private void Start()
    {
        Init();
    }

    public virtual void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
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

        transform.position = Vector2.MoveTowards(transform.position, currentTarget.position, speed * Time.deltaTime);
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
