using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackCollider : MonoBehaviour
{
    [SerializeField] EnemyAI enemy;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy"))
        {
            // dir
            Vector2 dir = new Vector2((collision.transform.position - gameObject.transform.position).normalized.x, 1);

            collision.GetComponent<IDamageable>()?.TakeDamage(enemy.AttackDamage, enemy.KnockbackForce , dir);
            gameObject.SetActive(false);
        }
    }
}
