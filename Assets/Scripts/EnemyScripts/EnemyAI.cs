using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour, IDamageable
{
    [SerializeField] private float health;
    [SerializeField] private float attackDamage;
    [SerializeField] private float knockbackForce;

    public float AttackDamage { get => attackDamage; }
    public float KnockbackForce { get => knockbackForce; }

    public void TakeDamage(float damage, float knockback, Vector2 dir)
    {
        if ((health - damage) <= 0) Unalive();
        else
        {
            health -= damage;
            Debug.Log("Enemy health left: " + health);
        }

        /// Knockback
        Debug.Log("Knocking back the enemy with force " + knockback + " in direction " + dir);
        gameObject.GetComponent<Rigidbody2D>()?.AddForce(knockback * dir, ForceMode2D.Impulse);
    }

    private void Unalive()
    {
        Debug.Log("Enemy has unalived.");
        Destroy(gameObject);
    }
}