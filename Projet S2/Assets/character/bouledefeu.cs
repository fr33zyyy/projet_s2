using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bouledefeu : MonoBehaviour
{
    
    public GameObject impactVFX;
    private bool collided;

    public float damage = 3;
    void OnCollisionEnter(Collision co)
    {
        if (co.gameObject.tag != "Bullet" && co.gameObject.tag != "Player" && !collided)
        {
            collided = true;

            var impact = Instantiate(impactVFX, co.contacts[0].point, Quaternion.identity) as GameObject;
            
            Destroy(impact, 2);
            Destroy (gameObject);
            Enemy enemy = co.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
        
    }

    public static implicit operator float(bouledefeu v)
    {
        throw new NotImplementedException();
    }
}
