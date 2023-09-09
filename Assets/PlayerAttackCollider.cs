using System.Collections;
using UnityEngine;

public class PlayerAttackCollider : MonoBehaviour
{
    [SerializeField] PlayerCombatSystem pcs;

    private void OnEnable()
    {
        StartCoroutine(ColliderActiveTime());
    }

    private IEnumerator ColliderActiveTime()
    {
        yield return new WaitForSeconds(pcs.BasicAttackDuration);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
        {
            // find direction from player to enemy (enemy - player)
            Vector2 dir = new Vector2((collision.transform.position - pcs.gameObject.transform.position).normalized.x, 1);

            // do the damage
            collision.GetComponent<IDamageable>()?.TakeDamage(pcs.BasicAttackDamage, pcs.BasicAttackKnockback, dir);

            // disable the collider
            gameObject.SetActive(false);
        }
    }
}
