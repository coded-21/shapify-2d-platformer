using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour, IDamageable
{
    [SerializeField] private float health;
    [SerializeField] private float attackDamage;
    public float AttackDamage { get => attackDamage; }

    public void TakeDamage(float damage, float knockback, Vector2 dir)
    {
        if ((health - damage) <= 0)
        {
            Unalive();
        }
        else
        {
            health -= damage;
            Debug.Log("Enemy health left: " + health);
        }
    }

    private void Unalive()
    {
        Debug.Log("Enemy has unalived.");
        Destroy(gameObject);
    }
}