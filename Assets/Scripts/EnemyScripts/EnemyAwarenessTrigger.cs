using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAwarenessTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GetComponentInParent<EnemyStateMachine>()?.SetState(new EnemyAwareState());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GetComponentInParent<EnemyStateMachine>()?.SetState(new EnemySuspicionState());
        }
    }
}
