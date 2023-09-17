using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableObject : MonoBehaviour, IDamageable
{
    int hitCount = 0;
    [SerializeField] int HP;
    [SerializeField] float knockbackForce;

    public void TakeDamage(float damage, float knockback, Vector2 dir)
    {
        if (hitCount > HP) { Unexist(); }
        else hitCount++;

        /// Knockback
        Debug.Log("Knockbacking the object with force " + knockbackForce + " in direction " + dir);
        gameObject.GetComponent<Rigidbody2D>()?.AddForce(knockbackForce * dir, ForceMode2D.Impulse);
    }

    private void Unexist()
    {
        Destroy(gameObject);
    }
}
