using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    //variable to determine if the dmg function can be called.
    private bool _canDamage = true;

    void OnTriggerEnter2D(Collider2D other)
    {
        IDamageable hit = other.GetComponent<IDamageable>();
        if(hit != null)
        {
            if (_canDamage == true)
            {
                hit.Damage();
                _canDamage = false;
                StartCoroutine(AttackDamageRoutine());
            }
        }
    }

    IEnumerator AttackDamageRoutine()
    {
        yield return new WaitForSeconds(.8f);
        _canDamage = true;
    }
}
