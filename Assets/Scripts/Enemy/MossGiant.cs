using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MossGiant : Enemy, IDamageable
{
    
    public int Health { get; set; }

    //For initializing.
    public override void Init()
    {
        base.Init();
        Health = base.health;
    }

    public void Damage()
    {
        if (isDead == true)
            return;

        Debug.Log("MossGiant::Damage()");
        anim.SetTrigger("Hit");
        Health--;
        isHit = true;
        anim.SetBool("InCombat", true);

        if(Health <= 0)
        {
            isDead = true;
            anim.SetTrigger("Death");
            //spawn a diamond
            //Change value of diamond to w/e my gem count is
            GameObject spawnedDiamond = Instantiate(diamondPrefab, transform.position, Quaternion.identity);
            spawnedDiamond.GetComponent<Diamond>().diamonWorth = gems;
        }
    }

}
