using UnityEngine;

public interface IDamageable
{
    void TakeDamage(float damage, float knockback, Vector2 dir);
}
