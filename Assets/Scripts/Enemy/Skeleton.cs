using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy, IDamageable
{
    public int Health { get; set; }

    //For Initializing.
    public override void Init()
    {
        base.Init();
        Health = base.health;
    }

    public void Damage()
    {
        if (isDead == true)
            return;

        Debug.Log("Skeleton::Damage()");
        anim.SetTrigger("Hit");
        Health--;
        isHit = true;
        anim.SetBool("InCombat", true);

        if (Health <= 0)
        {
            isDead = true;
            anim.SetTrigger("Death");
            GameObject spawnedDiamond = Instantiate(diamondPrefab, transform.position, Quaternion.identity);
            spawnedDiamond.GetComponent<Diamond>().diamonWorth = gems;
        }
    }
}
