using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAbilityCard : MonoBehaviour
{
    AttackAbility ability;
    private int dir;
    [SerializeField] private int phaseOneDist;
    [SerializeField] private float phaseOneMoveSpeed;
    [SerializeField] private float stopTime;

    public void Initialize(AttackAbility attackAbility)
    {
        ability = attackAbility;
        dir = ability.dir;
        StartCoroutine(PhaseOne());
    }

    private IEnumerator PhaseOne()
    {
        Vector3 startPosition = transform.position;
        Vector3 endPosition = new Vector3(transform.position.x + (dir * phaseOneDist),
            transform.position.y,
            transform.position.z);

        float elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            Vector3 currentPosition = Vector3.Lerp(startPosition, endPosition, elapsedTime);
            transform.position = currentPosition;

            elapsedTime += phaseOneMoveSpeed * Time.deltaTime;
            yield return null;
        }

        transform.position = endPosition;
        yield return new WaitForSeconds(stopTime);
        ability.StartAttack();
    }

    public void EnableAttackCollider()
    {
        GetComponent<BoxCollider2D>().enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
        {
            PlayerController player = FindObjectOfType<PlayerController>();
            Vector2 dir = new Vector2((player.gameObject.transform.position - gameObject.transform.position).normalized.x, 1);
            collision.GetComponent<IDamageable>()?.TakeDamage(ability.Damage, 0, dir);
        }
    }

    public void Destroy()
    {
        StopAllCoroutines();
        Destroy(gameObject);
    }
}
