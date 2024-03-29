﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy, IDamageable
{

    [SerializeField]
    private GameObject _acidEffectPrefab;
    [SerializeField]
    private Transform _acidFireSpot;

    public int Health { get; set; }

    //For initializing.
    public override void Init()
    {
        base.Init();
        Health = base.health;
    }

    public override void Update()
    {
        
    }

    public void Damage()
    {
        if (isDead == true)
            return;

        Health--;
        if(Health <= 0)
        {
            isDead = true;
            anim.SetTrigger("Death");
            GameObject spawnedDiamond = Instantiate(diamondPrefab, transform.position, Quaternion.identity);
            spawnedDiamond.GetComponent<Diamond>().diamonWorth = gems;
        }
    }

    public override void Movement()
    {
        //sit still!
    }

    public void Attack()
    {
        Instantiate(_acidEffectPrefab, _acidFireSpot.position, Quaternion.identity);
    }

}
