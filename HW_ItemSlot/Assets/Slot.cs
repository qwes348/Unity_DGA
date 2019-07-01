using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public int slotId;
    public ParticleSystem potionEffect;

    public void DropItem()
    {
        if(transform.childCount > 0)
        {
            Destroy(transform.GetChild(0).gameObject);
        }
    }

    public void DrinkPotion()
    {
        if (transform.childCount > 0)
        {
            Transform Player = GameObject.FindGameObjectWithTag("Player").transform;
            var potionFx = Instantiate(potionEffect, Player.position, Quaternion.identity);

            Destroy(transform.GetChild(0).gameObject);
            Destroy(potionFx, 3f);
        }
    }
}
