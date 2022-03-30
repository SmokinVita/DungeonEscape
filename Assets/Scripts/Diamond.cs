using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{
    //ontriggerenter to collect
    //check for the player
    //add the value of the diamond to the player
    public int diamonWorth = 1;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if(player != null)
            {
                player.AddDiamonds(diamonWorth);
                Destroy(gameObject);
            }
        }
    }

}
