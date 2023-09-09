using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySuspicionTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GetComponentInParent<EnemyStateMachine>()?.SetState(new EnemySuspicionState());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GetComponentInParent<EnemyStateMachine>()?.SetState(new EnemyIdleState());
        }
    }
}
