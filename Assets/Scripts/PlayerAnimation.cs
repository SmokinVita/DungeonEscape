using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _anim;

    private Animator _swordAnim;

    void Start()
    {
        _anim = GetComponentInChildren<Animator>();
        if (_anim == null)
        {
            Debug.LogError("Animator is null on children!");
        }

        _swordAnim = transform.GetChild(1).GetComponent<Animator>();
        if(_swordAnim == null)
        {
            Debug.LogError("Sword Arc animator is null!");
        }
    }
    
    public void RunAnimation(float speed)
    {
        _anim.SetFloat("Move_Speed", Mathf.Abs(speed));
    }

    public void Jump(bool isJumping)
    {
        _anim.SetBool("IsJumping", isJumping);
    }

    public void RegAttack()
    {
        _anim.SetTrigger("Attack");
        _swordAnim.SetTrigger("SwordAnimation");
    }
}
