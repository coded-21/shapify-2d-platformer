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
            collision.GetComponent<IDamageable>()?.TakeDamage(enemy.AttackDamage, 0 , Vector2.zero);
            gameObject.SetActive(false);
        }
    }
}
